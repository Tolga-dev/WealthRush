using System;
using Google.Play.Review;
using UnityEngine;

namespace Managers
{
    public class GameReviewManager : MonoBehaviour
    {
        private ReviewManager _gameReviewManager;

        public void Start()
        {
            _gameReviewManager = new ReviewManager();
            
        }
        
        public void RewardUser()
        {
            
        }
        
    }
}