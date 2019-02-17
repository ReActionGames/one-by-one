using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Continuous
{
    public class IAPUtility : MonoBehaviourSingleton<IAPUtility>/*, IStoreListener*/
    {
        [Serializable]
        public struct Product
        {
            [SerializeField] private string _ID;
            [SerializeField] private ProductType type;
            [SerializeField] private float price;

            public string ID => _ID;
            public ProductType Type => type;
        }

        [SerializeField] private Product[] products;

        private IStoreController controller;
        private IExtensionProvider extensions;

        private void Awake()
        {
            //ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            //foreach (Product product in products)
            //{
            //    builder.AddProduct(product.ID, product.Type);
            //}

            //UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            throw new System.NotImplementedException();
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            throw new System.NotImplementedException();
        }

        public void OnPurchaseFailed(UnityEngine.Purchasing.Product i, PurchaseFailureReason p)
        {
            throw new System.NotImplementedException();
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }

    
}