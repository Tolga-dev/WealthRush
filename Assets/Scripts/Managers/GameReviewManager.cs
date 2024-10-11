using System;
using System.Collections;
using Google.Play.Review;
using UnityEngine;

namespace Managers
{
    public class GameReviewManager : MonoBehaviour
    {
        private ReviewManager _reviewManager;
        private PlayReviewInfo _playReviewInfo;

        private void Start()
        {
            _reviewManager = new ReviewManager();
        }

        public void PopUp()
        {
            StartCoroutine(InitiateReviewFlow());
        }

        private IEnumerator InitiateReviewFlow()
        {
            var requestFlowOperation = _reviewManager.RequestReviewFlow();
            yield return requestFlowOperation;

            if (requestFlowOperation.Error != ReviewErrorCode.NoError)
            {
                Debug.LogError("Failed to request review flow: " + requestFlowOperation.Error.ToString());
                yield break; // Exit if there's an error requesting the review flow.
            }

            _playReviewInfo = requestFlowOperation.GetResult();

            var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
            yield return launchFlowOperation;

            if (launchFlowOperation.Error != ReviewErrorCode.NoError)
            {
                Debug.LogError("Failed to launch review flow: " + launchFlowOperation.Error.ToString());
                yield break; // Exit if there's an error launching the review flow.
            }

            Debug.Log("Review flow completed successfully.");
            _playReviewInfo = null; // Clear the PlayReviewInfo object after the review flow completes.
        }
    }
}