using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPManager : MonoBehaviour, IDetailedStoreListener
{

    IStoreController m_StoreController;

    //Adding Product ID
    public string goldProductId = "com.Aurora.InAppPurchasing.Gold50";
    public string diamondProductId = "com.Aurora.InAppPurchasing.Diamond50";

    private void Start()
    {
        InitilizePurchase(); 
    }

    //Initializing Purchase
    public void InitilizePurchase()
    {
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(goldProductId, ProductType.Consumable);
        builder.AddProduct(diamondProductId, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    void AddGold()
    {
        Debug.Log("Gold");
    }

    void AddDiamond()
    {
        Debug.Log("Diamond");
    }

    public void PurchaseGold()
    {
        m_StoreController.InitiatePurchase(goldProductId);
    }

    public void PurchaseDiamond()
    {
        m_StoreController.InitiatePurchase(diamondProductId);
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("Initialized Sucessfull");
        m_StoreController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Initialized Failed!");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("Initialized Failed!");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
       OnPurchaseFailed(product, PurchaseFailureReason.UserCancelled);
        Debug.Log("Failure Description");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchased Failure Reason");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        //Retrive the purchased product
        Product product = purchaseEvent.purchasedProduct;

        //Add the purchased product to the players inventory
        if (product.definition.id == goldProductId) 
        {
            AddGold();
        }
        else if(product.definition.id == diamondProductId)
        {
            AddDiamond();
        }
        //We return Complete, informing IAP that the processing on our side is done and
        return PurchaseProcessingResult.Complete;
    }
}
