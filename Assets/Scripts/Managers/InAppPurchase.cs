using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers
{
    public class InAppPurchase : MonoBehaviour, IDetailedStoreListener
    {
        private IStoreController _storeController;
        private IExtensionProvider _extensionProvider;
        private UnityAction<string> _onPurchaseSuccess;
        private UnityAction<string> _onPurchaseFailed;

        public GameManager gameManager;
        public bool isInitialized;
        public TextMeshProUGUI errorMessage;
        
        public IEnumerator StartInAppServer()
        {
            yield return InitializeUnityServices();

            float timeout = 10f; // Set your desired timeout duration in seconds
            float elapsedTime = 0f;

            while (!isInitialized && elapsedTime < timeout)
            {
                elapsedTime += Time.deltaTime; // Accumulate elapsed time
                yield return null; // Wait for the next frame
            }

            if (!isInitialized)
            {
                Debug.LogWarning("IAP initialization timed out.");
                errorMessage.text= "Time Out. Please try again later.";
                
                yield return new WaitForSeconds(1);
            }
            
            yield return null; // Wait for the next frame
        }
        private IEnumerator InitializeUnityServices()
        {
            var options = new InitializationOptions().SetEnvironmentName("development"); // Set your environment
            yield return  UnityServices.InitializeAsync(options);
            Debug.Log("Unity Services initialized.");
            
            InitializeIAP();
        }
        
        private void InitializeIAP()
        {
            Debug.Log("Initializing IAP...");

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.Configure<IGooglePlayConfiguration>().SetDeferredPurchaseListener(OnDeferredPurchase);
            builder.Configure<IGooglePlayConfiguration>().SetQueryProductDetailsFailedListener(MyFunction);

            builder.AddProduct(gameManager.gamePropertiesInSave.noAdsProductId, ProductType.NonConsumable);
            
            UnityPurchasing.Initialize(this, builder);

            Debug.Log("UnityPurchasing.Initialize called.");
        }
        void OnDeferredPurchase(Product product)
        {
            Debug.Log($"Purchase of {product.definition.id} is deferred");
        }
        void MyFunction(int myInt)
        {
            Debug.Log("Listener = " + myInt.ToString());
        }
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            _extensionProvider = extensions;
            isInitialized = true;
            foreach (var products in _storeController.products.all)
            {
                Debug.Log($"Product available: {products.definition.id}");
            }
        }
      
        public void OnInitializeFailed(InitializationFailureReason error)
        {
            _onPurchaseFailed.Invoke("Failed to initialize IAP: " + error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            _onPurchaseFailed.Invoke("Failed to initialize IAP: " + error + " " + message);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            _onPurchaseSuccess.Invoke("Purchased ");
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            _onPurchaseFailed.Invoke("purchase failed " + failureReason);
        }

  

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            _onPurchaseFailed.Invoke("purchase failed " + failureDescription);
        }

        public void BuyItem(string id, UnityAction<string> success, UnityAction<string> failed)
        {
            if (!isInitialized)
            {
                StartCoroutine(StartInAppServer());
            }
            
            var noAdsProduct = GetItem(id);

            _onPurchaseSuccess = success;
            _onPurchaseFailed = failed;
            if (noAdsProduct != null)
            {
                Debug.Log($"Buying {id} product");
                _storeController.InitiatePurchase(noAdsProduct);
                
            }
            else
            {
                _onPurchaseFailed.Invoke("Product not available");
            }

        }

        public string ReturnLocalizedPrice(string id)
        {
            var product = GetItem(id);
            if (product != null)
            {
                decimal price = product.metadata.localizedPrice;
                string code = product.metadata.isoCurrencyCode;
                return price + " " + code;
            }
            return "N/A";
        }

        private Product GetItem(string id)
        {
            
            if (_storeController == null)
            {
                Debug.LogError("StoreController is not initialized.");
                return null;
            }
            foreach (var products in _storeController.products.all)
            {
                Debug.Log($"Product available: {products.definition.id}");
            }
            
            var product = _storeController.products.WithID(id);
            if (product == null)
            {
                Debug.LogError($"Product with id {id} not found.");
            }
            return product;
        }
        private bool IsInitialized()
        {
            return _storeController != null && _extensionProvider != null;
        }
    }
}