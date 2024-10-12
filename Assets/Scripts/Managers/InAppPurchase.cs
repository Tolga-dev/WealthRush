using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managers.Controllers.InAppPurchase;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Serialization;

namespace Managers
{
    public class InAppPurchase : MonoBehaviour, IDetailedStoreListener
    {
        private IStoreController _storeController;
        public IExtensionProvider ExtensionProvider;
        private UnityAction<string> _onPurchaseSuccess;
        private UnityAction<string> _onPurchaseFailed;

        public List<ConsumableItems> consumableItems = new List<ConsumableItems>();

        public async Task<Task> StartInAppServer()
        {
            await InitializeUnityServices();
            return Task.FromResult(Task.CompletedTask);
        }

        private async Task InitializeUnityServices()
        {
            var options = new InitializationOptions().SetEnvironmentName("development"); // Set your environment
            await UnityServices.InitializeAsync(options);

            InitializeIAP();
        }

        private void InitializeIAP()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            var products = new List<ProductDefinition>();
            products.
                AddRange(consumableItems.Select(
                        consumableItem => new ProductDefinition(consumableItem.id, ProductType.Consumable)));

            builder.AddProducts(products);
            
            UnityPurchasing.Initialize(this, builder);
        }
        
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            ExtensionProvider = extensions;
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
            var product = _storeController.products.WithID(id);
            return product;
        }


    }
}