using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IAPManager : MonoBehaviour
{

	public static IAPManager instance;

	string _label = "";
	bool _isInitialized = false;
	public static bool _purchaseDone = false;
	private bool _processingPayment = false;

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

	

	private void OnPurchaseSucceded()
	{
		_purchaseDone = true;
		Debug.Log ("Working");


/*		if (purchase.Sku.Equals (pack1))
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
*/

		HomeUIManager.insta.saveData (); //save data
	}
	public void PurchaseProduct(string good)
	{
		//Debug.Log ("SuccessFull");
		_purchaseDone = false;
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
	}


	//default methods
	
	private void purchaseFailedEvent(int errorCode, string errorMessage)
	{
		Debug.Log("purchaseFailedEvent: " + errorMessage);
		_label = "Purchase Failed: " + errorMessage;
		BuyErrorPanel.SetActive (true);
		PackageContainer.SetActive (false);
	}
	private void consumePurchaseSucceededEvent()
	{
		//Debug.Log("consumePurchaseSucceededEvent: " + purchase);
		//_label = "CONSUMED: " + purchase.ToString();
	}
	private void consumePurchaseFailedEvent(string error)
	{
		Debug.Log("consumePurchaseFailedEvent: " + error);
		_label = "Consume Failed: " + error;
	}

	

}