using System;
using System.Collections;
using Player;
using Save.GameObjects.Base;
using TMPro;
using UnityEngine;

namespace Save.GameObjects.Prizes
{
    public class Prize : GameObjectBase
    {
        public ParticleSystem emoji;
        public Canvas prizeCanvas;

        public int prizeAmount;
        public bool onUse = false;

        protected override void PlayAdditionalEffects(PlayerController playerController)
        {
            if (emoji != null)
                SetParticlePosition(emoji, playerController.prizeEffectSpawnPoint.transform);

            if(prizeCanvas != null)
                    ShowCanvas(playerController.canvasSpawnPoint.transform);
        }

        public  void ShowCanvas(Transform playerPos)
        {
            var canvasTransform = prizeCanvas.transform;
    
            // Set the canvas parent and initial position
            canvasTransform.SetParent(playerPos);
            canvasTransform.localPosition = Vector3.zero;

            var text = prizeCanvas.GetComponentInChildren<TextMeshProUGUI>();
            text.text = prizeAmount.ToString();
    
            canvasTransform.gameObject.SetActive(true);
            StartCoroutine(AnimateCanvas(canvasTransform));
        }

   
        public virtual IEnumerator AnimateCanvas(Transform canvasTransform)
        {
            float duration = 1f; // Total duration of the animation
            float elapsed = 0f;

            Vector3 startPos = canvasTransform.localPosition; // Starting position (Vector3.zero)
            Vector3 endPos = startPos + new Vector3(0, 10, 0); // Move up by 1 unit on the Y axis
    
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                canvasTransform.localPosition = Vector3.Lerp(startPos, endPos, elapsed / duration);
        
                yield return null;
            }
            Destroy(prizeCanvas.gameObject);
        }

        public void OnDestroy()
        {
            if(prizeCanvas != null )
                Destroy(prizeCanvas.gameObject);
        }
    }
}