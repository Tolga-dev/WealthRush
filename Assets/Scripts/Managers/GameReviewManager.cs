using System.Collections;
using Google.Play.Review;
using UnityEngine;

namespace Managers
{
    public class GameReviewManager : MonoBehaviour
    {
        private ReviewManager _reviewManager;
        private PlayReviewInfo _playReviewInfo;
        public GameManager gameManager;
    
        public void PopUp()
        {
            var save = gameManager.gamePropertiesInSave;
            if (save.isReviewed)
            {
                Debug.Log("Review flow already completed or max reviews reached.");
            }
            else
            {
                if (save.reviewCount >= save.maxReviewCount)
                {
                    StartCoroutine(InitiateReviewFlow());
                    Debug.Log("Review flow initiated.");                
                }
                else
                {
                    save.reviewCount++;
                }
            }
        }

        private IEnumerator InitiateReviewFlow()
        {
            // Request a review flow from the Play Store
            var requestFlowOperation = _reviewManager.RequestReviewFlow();
            yield return requestFlowOperation;

            if (requestFlowOperation.Error != ReviewErrorCode.NoError)
            {
                Debug.LogError("Failed to request review flow: " + requestFlowOperation.Error.ToString());
                yield break;
            }

            _playReviewInfo = requestFlowOperation.GetResult();

            // Launch the review flow
            var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
            yield return launchFlowOperation;

            if (launchFlowOperation.Error != ReviewErrorCode.NoError)
            {
                Debug.LogError("Failed to launch review flow: " + launchFlowOperation.Error.ToString());
                yield break;
            }
            var save = gameManager.gamePropertiesInSave;

            Debug.Log("Review flow completed successfully.");
            _playReviewInfo = null; // Clear PlayReviewInfo after completion.

            save.isReviewed = true;
        }
    }
}