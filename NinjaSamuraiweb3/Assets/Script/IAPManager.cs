using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OnePF; //namespace OnePF is used to access functionality related to InApp purchase


public class IAPManager : MonoBehaviour
{

	public static IAPManager instance;

	string _label = "";
	bool _isInitialized = false;
	public static bool _purchaseDone = false;
	private bool _processingPayment = false;
	Inventory _inventory = null;

	private const string STORE_ONEPF = "org.onepf.store";
	const string SKU = "css";
	//const string SKU_AdRemove = "com.fss.removead";

	public  string publicKey = "com.tgs.nssf.pack3";
	public  string pack1 = "com.tgs.nssf.pack1"; 
	public  string pack2 = "com.tgs.nssf.pack2";
	public  string pack3 = "com.tgs.nssf.pack3";

	public GameObject BuyErrorPanel, BuySuccessfulPanel,PackageContainer, IAPBackBtn, IAPPanel;


	private void Start()
	{
		instance = this;

		//ios declare
		OpenIAB.mapSku( pack1, OpenIAB_iOS.STORE, pack1 );
		OpenIAB.mapSku( pack2, OpenIAB_iOS.STORE, pack2 );
		OpenIAB.mapSku( pack3, OpenIAB_iOS.STORE, pack3 );

		//android declare
		OpenIAB.mapSku( pack1, OpenIAB_Android.STORE_GOOGLE, pack1 );
		OpenIAB.mapSku( pack2, OpenIAB_Android.STORE_GOOGLE, pack2 );
		OpenIAB.mapSku( pack3, OpenIAB_Android.STORE_GOOGLE, pack3 );

		var options = new Options();
		options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
		options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
		options.checkInventory = false;
		options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;
		options.storeKeys = new Dictionary<string, string> { {OpenIAB_Android.STORE_GOOGLE, publicKey} };
		OpenIAB.init( options );
	}

	#if UNITY_ANDROID
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !BuyErrorPanel.activeSelf && !BuySuccessfulPanel.activeSelf && IAPPanel.activeSelf)
			IAPBackBtn.GetComponent<Button> ().onClick.Invoke ();		
		else if(Input.GetKeyDown (KeyCode.Escape) && IAPPanel.activeSelf)
			BackBtn ();
	}
	#endif

	public void OnEnable()
	{
		OpenIABEventManager.billingSupportedEvent += billingSupportedEvent;
		OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
		OpenIABEventManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		OpenIABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
		OpenIABEventManager.purchaseSucceededEvent += OnPurchaseSucceded;
		OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
		OpenIABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		OpenIABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
		OpenIABEventManager.transactionRestoredEvent += transactionRestoredEvent;
	}
	public void OnDisable()
	{
		OpenIABEventManager.billingSupportedEvent -= billingSupportedEvent;
		OpenIABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		OpenIABEventManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		OpenIABEventManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
		OpenIABEventManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		OpenIABEventManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent -= OnPurchaseSucceded;
		OpenIABEventManager.transactionRestoredEvent -= transactionRestoredEvent;
	}

	private void OnPurchaseSucceded(Purchase purchase)
	{
		//Debug.Log ("Purchase Succeeded - "+ purchase.Sku + " and " + PluginManager._insta.inAppRemoveAdID);
		//PlayerPrefs.SetInt ("InApp",1);
		//PlayerPrefs.Save ();
		_purchaseDone = true;
		Debug.Log ("Working");

		if (purchase.Sku.Equals (pack1))
		{
			PlayerPrefs.SetInt ("Money",PlayerPrefs.GetInt("Money") + 5000);
			PackageContainer.SetActive (false);
			BuySuccessfulPanel.SetActive (true);
			Debug.Log("pack1");
			PlayerPrefs.SetInt ("Inerstitial", 0);
			PlayerPrefs.Save ();
		}
		else if (purchase.Sku.Equals (pack2)){
			PlayerPrefs.SetInt ("Money",PlayerPrefs.GetInt("Money") + 15000);
			PackageContainer.SetActive (false);
			BuySuccessfulPanel.SetActive (true);
			Debug.Log("pack2");
			PlayerPrefs.SetInt ("Inerstitial", 0);
			PlayerPrefs.Save ();
		}
		else if (purchase.Sku.Equals (pack3)){
			PlayerPrefs.SetInt ("Money",PlayerPrefs.GetInt("Money") + 50000);
			PackageContainer.SetActive (false);
			BuySuccessfulPanel.SetActive (true);
			Debug.Log("pack3");
			PlayerPrefs.SetInt ("Inerstitial", 0);
			PlayerPrefs.Save ();
		}
		else {
			BuyErrorPanel.SetActive (true);
			PackageContainer.SetActive (false);
			Debug.Log(string.Format("ProcessPurchase: FAIL", purchase.Sku.ToString()));
		}

		OpenIAB.consumeProduct(purchase);
		_processingPayment = false;

		HomeUIManager.insta.saveData (); //save data
	}
	public void PurchaseProduct(string good)
	{
		//Debug.Log ("SuccessFull");
		_purchaseDone = false;
		OpenIAB.purchaseProduct(good);
	}


	//call when 5000 gold package clicked
	public void gold5000BuyBtn()
	{
		PurchaseProduct(pack1);
		audioManager.instance.PlaySound ("Click");
		Debug.Log(string.Format(pack1));
	}
	//call when 10000 gold package clicked
	public void gold15000BuyBtn()
	{
		PurchaseProduct(pack2);
		audioManager.instance.PlaySound ("Click");
	}
	//call when 20000 gold package clicked
	public void gold50000BuyBtn()
	{
		PurchaseProduct(pack3);
		audioManager.instance.PlaySound ("Click");
	}


	//closes the buy error and successful panels
	public void BackBtn()
	{
		BuyErrorPanel.SetActive (false);
		BuySuccessfulPanel.SetActive (false);
		PackageContainer.SetActive (true);
		audioManager.instance.PlaySound ("Click");
	}



	public void transactionRestoredEvent(string itemName)
	{
		if (itemName.Equals (pack1))
		{
			Debug.Log("Ad Removed Restore: " + itemName);
		}
		Debug.Log ("Restore item " + itemName);
	}

	public void restorePurchase()
	{
		Debug.Log ("restore");
		OpenIAB.restoreTransactions ();
	}


	//default methods
	private void billingSupportedEvent()
	{
		_isInitialized = true;
		Debug.Log("billingSupportedEvent");
	}
	private void billingNotSupportedEvent(string error)
	{
		Debug.Log("billingNotSupportedEvent: " + error);
	}
	private void queryInventorySucceededEvent(Inventory inventory)
	{
		Debug.Log("queryInventorySucceededEvent: " + inventory);
		if (inventory != null)
		{
			_label = inventory.ToString();
			_inventory = inventory;
		}
	}
	private void queryInventoryFailedEvent(string error)
	{
		Debug.Log("queryInventoryFailedEvent: " + error);
		_label = error;
	}
	private void purchaseSucceededEvent(Purchase purchase)
	{
		Debug.Log("purchaseSucceededEvent: " + purchase);
		_label = "PURCHASED:" + purchase.ToString();
	}
	private void purchaseFailedEvent(int errorCode, string errorMessage)
	{
		Debug.Log("purchaseFailedEvent: " + errorMessage);
		_label = "Purchase Failed: " + errorMessage;
		BuyErrorPanel.SetActive (true);
		PackageContainer.SetActive (false);
	}
	private void consumePurchaseSucceededEvent(Purchase purchase)
	{
		Debug.Log("consumePurchaseSucceededEvent: " + purchase);
		_label = "CONSUMED: " + purchase.ToString();
	}
	private void consumePurchaseFailedEvent(string error)
	{
		Debug.Log("consumePurchaseFailedEvent: " + error);
		_label = "Consume Failed: " + error;
	}

	/*private static IStoreController myController; 
	private static IExtensionProvider myExtensions;
	   
	public static string gold5000 = "com.tgs.nssf.pack1"; 
	public static string gold15000 = "com.tgs.nssf.pack2";
	public static string gold50000 = "com.tgs.nssf.pack3";

	public GameObject BuyErrorPanel, BuySuccessfulPanel,PackageContainer, IAPBackBtn, IAPPanel;
	//initialize if controller is empty
	void Start()
	{
		if (myController == null)
			InitializePurchasing();
	}
	#if UNITY_ANDROID
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !BuyErrorPanel.activeSelf && !BuySuccessfulPanel.activeSelf && IAPPanel.activeSelf)
			IAPBackBtn.GetComponent<Button> ().onClick.Invoke ();		
		else if(Input.GetKeyDown (KeyCode.Escape) && IAPPanel.activeSelf)
			BackBtn ();
	}
	#endif
	//initiallize the packages
	public void InitializePurchasing() 
	{
		if (IsInitialized())
			return;

		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		builder.AddProduct(gold5000, ProductType.Consumable);
		builder.AddProduct(gold15000, ProductType.Consumable);
		builder.AddProduct(gold50000, ProductType.Consumable);

		UnityPurchasing.Initialize(this, builder);
	}
	//if intialized than sets controller and extentions as not null
	private bool IsInitialized()
	{
		return myController != null && myExtensions != null;
	}
	//call when 5000 gold package clicked
	public void gold5000BuyBtn()
	{
		BuyProductID(gold5000);
		audioManager.instance.PlaySound ("Click");
	}
	//call when 10000 gold package clicked
	public void gold15000BuyBtn()
	{
		BuyProductID(gold15000);
		audioManager.instance.PlaySound ("Click");
	}
	//call when 20000 gold package clicked
	public void gold50000BuyBtn()
	{
		BuyProductID(gold50000);
		audioManager.instance.PlaySound ("Click");
	}
	//buy the prodect from store based on package id
	void BuyProductID(string productId)
	{
		// If Purchasing has been initialized
		if (IsInitialized ()) {
			// look up the Product reference with the general product identifier and the Purchasing 
			// system's products collection.
			Product product = myController.products.WithID (productId);

			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase) {
				Debug.Log (string.Format ("Purchasing product asychronously: '{0}'", product.definition.id));
				// buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
				// asynchronously.
				myController.InitiatePurchase (product);
			} else {
				BuyErrorPanel.SetActive (true);
				PackageContainer.SetActive (false);
				Debug.Log ("Purachase Failed");
			}
		} 
		else 
		{
			BuyErrorPanel.SetActive (true);
			PackageContainer.SetActive (false);
			Debug.Log ("Purchase Failed. Not Initialized.");
		}
	}
	//initialze the controller and extension
	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		myController = controller;
		myExtensions = extensions;
		Debug.Log("Initialized");
	}
	//if initialize failed
	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("Initialize Failed Reason:" + error);
	}
	//returns the product buy result 
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		
		if (String.Equals(args.purchasedProduct.definition.id, gold5000, StringComparison.Ordinal))
		{
			PlayerPrefs.SetInt ("Money",PlayerPrefs.GetInt("Money") + 5000);
			PackageContainer.SetActive (false);
			BuySuccessfulPanel.SetActive (true);
			Debug.Log("gold5000");
			PlayerPrefs.SetInt ("Inerstitial", 0);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, gold15000, StringComparison.Ordinal))
		{
			PlayerPrefs.SetInt ("Money",PlayerPrefs.GetInt("Money") + 15000);
			PackageContainer.SetActive (false);
			BuySuccessfulPanel.SetActive (true);
			Debug.Log("gold15000");
			PlayerPrefs.SetInt ("Inerstitial", 0);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, gold50000, StringComparison.Ordinal))
		{
			PlayerPrefs.SetInt ("Money",PlayerPrefs.GetInt("Money") + 50000);
			PackageContainer.SetActive (false);
			BuySuccessfulPanel.SetActive (true);
			Debug.Log("gold50000");
			PlayerPrefs.SetInt ("Inerstitial", 0);
		}
		else 
		{
			BuyErrorPanel.SetActive (true);
			PackageContainer.SetActive (false);
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}
		return PurchaseProcessingResult.Complete;
	}
	//if purchase failed
	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		Debug.Log(string.Format("Purchase Failed. Product: '{0}', Reason: {1}", product.definition.storeSpecificId, failureReason));
	}
	//closes the buy error and successful panels
	public void BackBtn()
	{
		BuyErrorPanel.SetActive (false);
		BuySuccessfulPanel.SetActive (false);
		PackageContainer.SetActive (true);
		audioManager.instance.PlaySound ("Click");
	}*/

}