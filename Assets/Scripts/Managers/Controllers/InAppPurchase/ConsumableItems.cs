using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace Managers.Controllers.InAppPurchase
{
    [Serializable]
    public class ConsumableItems
    {
        public Button shopButton; // give function early
        public string id;
    }
}