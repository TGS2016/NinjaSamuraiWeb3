﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyHat : MonoBehaviour {

	public GameObject NotEnoughMOneyUI, DiscriptionPanel, HatPanel;

	public Text MoneyTxt, HatCostTxt, HitPointTxt;

	int money, cost, value;
	string prefs;

	void Start () 
	{
		//Display Total Money
		money = PlayerPrefs.GetInt ("Money",50);
		MoneyTxt.text = money.ToString();
	}

	#if UNITY_ANDROID
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			if (NotEnoughMOneyUI.activeSelf)
			{
				CloseBtn ();
				return;
			} 
			else
				CancelBtn ();
		}
	}
	#endif

	//Set the Cost and the PlayerPrefs value of the Hat
	public void setCostandVale (int weaponCost,int Value)
	{
		cost = weaponCost;
		value = Value;
		HatCostTxt.text = cost.ToString();
	}
	//Sets the HitPoints in description and prefs value
	public void setHatKey(string prefsKey)
	{
		prefs = prefsKey;

		if (prefs == "Bamboo") 
			HitPointTxt.text = "Hit Point : 2";
		else if (prefs == "Sherlock") 
			HitPointTxt.text = "Hit Point : 3";
		else if (prefs == "Pirate1") 
			HitPointTxt.text = "Hit Point : 3";
		else if (prefs == "Pirate2") 
			HitPointTxt.text = "Hit Point : 4";
		else if (prefs == "Topper") 
			HitPointTxt.text = "Hit Point : 4";
		else if (prefs == "Cowboy") 
			HitPointTxt.text = "Hit Point : 5";
		else if (prefs == "Army1") 
			HitPointTxt.text = "Hit Point : 6";
		else if (prefs == "Army2") 
			HitPointTxt.text = "Hit Point : 7";
		else if (prefs == "Wizard") 
			HitPointTxt.text = "Hit Point : 7";
		else if (prefs == "Samurai")
			HitPointTxt.text = "Hit Point : 8";
	}

	public void BuyBtn()
	{
		//If not enough money
		if (money < cost) 
		{
			NotEnoughMOneyUI.SetActive (true);
			audioManager.instance.PlaySound ("NoMoney");
		} 
		else 
		{
			//Subtract money based on hat cost
			money -= cost;
			PlayerPrefs.SetInt ("Money", money);
			MoneyTxt.text = "" + money;
			audioManager.instance.PlaySound ("MoneySpend");
			//buy the hat based on prefs value
			if (prefs == "Bamboo")
				PlayerPrefs.SetInt ("Bamboo", 1);
			else if (prefs == "Sherlock")
				PlayerPrefs.SetInt ("Sherlock", 1);
			else if (prefs == "Pirate1")
				PlayerPrefs.SetInt ("Pirate1", 1);
			else if (prefs == "Pirate2")
				PlayerPrefs.SetInt ("Pirate2", 1);
			else if (prefs == "Topper")
				PlayerPrefs.SetInt ("Topper", 1);
			else if (prefs == "Cowboy")
				PlayerPrefs.SetInt ("Cowboy", 1);
			else if (prefs == "Army1")
				PlayerPrefs.SetInt ("Army1", 1);
			else if (prefs == "Army2")
				PlayerPrefs.SetInt ("Army2", 1);
			else if (prefs == "Wizard")
				PlayerPrefs.SetInt ("Wizard", 1);
			else if (prefs == "Samurai")
				PlayerPrefs.SetInt ("Samurai", 1);
			//set bought hat as default hat
			PlayerPrefs.SetInt ("Hat", value);
		
			HatPanel.SetActive(true);
			DiscriptionPanel.SetActive (true);

			// disable the buy button
			GameObject.Find ("HelmetPanel").GetComponent<HelmetSelection> ().btnVisibility ();
			GameObject.Find ("HelmetPanel").GetComponent<HelmetSelection> ().updateSelectedHatTxt ();
			//update remaining gold
			GameObject.Find ("HelmetPanel").GetComponent<HelmetSelection> ().gold ();

			this.gameObject.SetActive (false);
		}
	}
	//closes not enough money panel
	public void CloseBtn()
	{
		audioManager.instance.PlaySound ("Click");
		NotEnoughMOneyUI.SetActive (false);
	}
	//cancel the buy process
	public void CancelBtn()
	{
		audioManager.instance.PlaySound ("Click");
		HatPanel.SetActive (true);
		DiscriptionPanel.SetActive (true);
		this.gameObject.SetActive (false);
	}

}
