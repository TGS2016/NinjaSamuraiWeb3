﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelmetSelection : MonoBehaviour {

	public GameObject[] SelectBtns, BuyBtns;
	public GameObject BuyUI, DiscriptionPanel,BackBtn, HatPanel;
	public Text HitPointTxt, moneyTxt, UnlockTxt;

	int IsHatUnlocked;
	//Set the text of selected hat
	void Start ()
	{
		SelectBtns = GameObject.FindGameObjectsWithTag("SelectBtn");
		//Finds the all buy buttons for hat
		BuyBtns = new GameObject[GameObject.FindGameObjectsWithTag("BuyBtn").Length];
		int j=0;
		for (int i = 0; i < this.gameObject.GetComponentsInChildren<Button> ().Length; i++) 
		{
			//sets the buy button
			if (this.gameObject.GetComponentsInChildren<Button> () [i].tag == "BuyBtn") 
			{
				BuyBtns[j] = this.gameObject.GetComponentsInChildren<Button> ()[i].gameObject;
				j++;
			}
		}
		updateSelectedHatTxt ();

		btnVisibility ();
	}

	void OnDisable(){
		//Array.Clear(SelectBtns, 0, SelectBtns.Length);
		//Array.Clear(BuyBtns, 0, BuyBtns.Length);
	}

	void OnEnable(){
		
		gold ();



	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			BackBtn.GetComponent<Button> ().onClick.Invoke ();			
		}
	}

	//updated the playerprefs of selected hat
	public void SetHat(int value)
	{
		PlayerPrefs.SetInt ("Hat", value);
		audioManager.instance.PlaySound ("Click");
		updateSelectedHatTxt ();
	}
	//set the text of selected text and set the text select for other select buttons
	public void updateSelectedHatTxt()
	{
		Button SelectedHatBtn;

		for (int i = 0; i < SelectBtns.Length; i++) 
			SelectBtns [i].GetComponentInChildren<Text> ().text = "Select";

		//Than Selected weapon's text is set to Selected
		int selectedHat = PlayerPrefs.GetInt ("Hat",0);

		if (selectedHat == 0) 
		{
			SelectedHatBtn = GameObject.Find ("BlackStrawHatBtn").GetComponent<Button>();
			SelectedHatBtn.GetComponentInChildren<Text> ().text = "Selected";
			HitPointTxt.text = "Hit Point : 2";
		}
		else if (selectedHat == 1) 
		{
			SelectedHatBtn = GameObject.Find ("BambooStrawHatBtn").GetComponent<Button>();
			SelectedHatBtn.GetComponentInChildren<Text> ().text = "Selected";
			HitPointTxt.text = "Hit Point : 2";
		}
		else if (selectedHat == 2) 
		{
			SelectedHatBtn = GameObject.Find ("SherlockHatBtn").GetComponent<Button>();
			SelectedHatBtn.GetComponentInChildren<Text> ().text = "Selected";
			HitPointTxt.text = "Hit Point : 3";
		}
		else if (selectedHat == 3) 
		{
			SelectedHatBtn = GameObject.Find ("PirateHat1Btn").GetComponent<Button>();
			SelectedHatBtn.GetComponentInChildren<Text> ().text = "Selected";
			HitPointTxt.text = "Hit Point : 3";
		}
		else if (selectedHat == 4) 
		{
			SelectedHatBtn = GameObject.Find ("PirateHat2Btn").GetComponent<Button>();
			SelectedHatBtn.GetComponentInChildren<Text> ().text = "Selected";
			HitPointTxt.text = "Hit Point : 4";
		}
		else if (selectedHat == 5) 
		{
			SelectedHatBtn = GameObject.Find ("TopperHatBtn").GetComponent<Button>();
			SelectedHatBtn.GetComponentInChildren<Text> ().text = "Selected";
			HitPointTxt.text = "Hit Point : 4";
		}
		else if (selectedHat == 6) 
		{
			SelectedHatBtn = GameObject.Find ("CowboyHatBtn").GetComponent<Button>();
			SelectedHatBtn.GetComponentInChildren<Text> ().text = "Selected";
			HitPointTxt.text = "Hit Point : 5";
		}
		else if (selectedHat == 7) 
		{
			SelectedHatBtn = GameObject.Find ("ArmyHelmet1Btn").GetComponent<Button>();
			SelectedHatBtn.GetComponentInChildren<Text> ().text = "Selected";
			HitPointTxt.text = "Hit Point : 6";
		}
		else if (selectedHat == 8) 
		{
			SelectedHatBtn = GameObject.Find ("ArmyHelmet2Btn").GetComponent<Button>();
			SelectedHatBtn.GetComponentInChildren<Text> ().text = "Selected";
			HitPointTxt.text = "Hit Point : 7";
		}
		else if (selectedHat == 9) 
		{
			SelectedHatBtn = GameObject.Find ("WizardHatBtn").GetComponent<Button>();
			SelectedHatBtn.GetComponentInChildren<Text> ().text = "Selected";
			HitPointTxt.text = "Hit Point : 7";
		}
		else if (selectedHat == 10) 
		{
			SelectedHatBtn = GameObject.Find ("SamuraiHelmetBtn").GetComponent<Button>();
			SelectedHatBtn.GetComponentInChildren<Text> ().text = "Selected";
			HitPointTxt.text = "Hit Point : 8";
		}
		else if (selectedHat == 11)
		{
			SelectedHatBtn = GameObject.Find ("NinjaCapBtn").GetComponent<Button>();
			SelectedHatBtn.GetComponentInChildren<Text> ().text = "Selected";
			HitPointTxt.text = "Hit Point : 7";
		}
		else if (selectedHat == 12)
		{
			SelectedHatBtn = GameObject.Find ("SantaCapBtn").GetComponent<Button>();
			SelectedHatBtn.GetComponentInChildren<Text> ().text = "Selected";
			HitPointTxt.text = "Hit Point : 10";
		}
	}
	//check the prefs values and based on that enable or disable the buy button
	public void btnVisibility()	
	{
		IsHatUnlocked = PlayerPrefs.GetInt ("Bamboo",0);
		setVisibility (0);

		IsHatUnlocked = PlayerPrefs.GetInt ("Sherlock",0);
		setVisibility (1);

		IsHatUnlocked = PlayerPrefs.GetInt ("Pirate1",0);
		setVisibility (2);

		IsHatUnlocked = PlayerPrefs.GetInt ("Pirate2",0);
		setVisibility (3);

		IsHatUnlocked = PlayerPrefs.GetInt ("Topper",0);
		setVisibility (4);

		IsHatUnlocked = PlayerPrefs.GetInt ("Cowboy",0);
		setVisibility (5);

		IsHatUnlocked = PlayerPrefs.GetInt ("Army1",0);
		setVisibility (6);

		IsHatUnlocked = PlayerPrefs.GetInt ("Army2",0);
		setVisibility (7);

		IsHatUnlocked = PlayerPrefs.GetInt ("Wizard",0);
		setVisibility (8);

		IsHatUnlocked = PlayerPrefs.GetInt ("Samurai",0);
		setVisibility (9);

		IsHatUnlocked = PlayerPrefs.GetInt ("NinjaCap",0);
		if (IsHatUnlocked == 1) 
		{
			UnlockTxt.gameObject.SetActive (false);
			SelectBtns [11].gameObject.GetComponent<Button> ().interactable = true;
		} 
		else 
		{
			UnlockTxt.gameObject.SetActive (true);
			SelectBtns [11].gameObject.GetComponent<Button> ().interactable = false;
		}
	}

	void setVisibility(int i)
	{
		if (IsHatUnlocked == 1) 
		{
			SelectBtns [i + 1].gameObject.SetActive (true);
			BuyBtns [i].gameObject.SetActive (false);
		}
		else 
		{
			SelectBtns [i + 1].gameObject.SetActive (false);
			BuyBtns [i].gameObject.SetActive (true);
		}
	}
	//display gold text
	public void gold()
	{
		moneyTxt.text = "" + PlayerPrefs.GetInt ("Money", 50);
	}
	//sends the prefs value
	public void BuyBtn(string prefsKey)
	{
		BuyUI.SetActive (true);
		HatPanel.SetActive (false);
		DiscriptionPanel.SetActive (false);
		audioManager.instance.PlaySound ("Click");
		CostandValue (prefsKey);
		GameObject.Find("BuyUI").gameObject.GetComponent<BuyHat>().setHatKey(prefsKey);
	}
	//sends the cost and prefs value of hat
	void CostandValue(string prefsKey)
	{
		//setCostandValue(Cost,Value)
		if (prefsKey == "Bamboo")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyHat>().setCostandVale(1000,1);
		else if (prefsKey == "Sherlock")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyHat>().setCostandVale(2000,2);
		else if (prefsKey == "Pirate1")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyHat>().setCostandVale(2500,3);
		else if (prefsKey == "Pirate2")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyHat>().setCostandVale(3000,4);
		else if (prefsKey == "Topper")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyHat>().setCostandVale(3500,5);
		else if (prefsKey == "Cowboy")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyHat>().setCostandVale(5000,6);
		else if (prefsKey == "Army1")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyHat>().setCostandVale(6000,7);
		else if (prefsKey == "Army2")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyHat>().setCostandVale(7000,8);
		else if (prefsKey == "Wizard")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyHat>().setCostandVale(7500,9);
		else if (prefsKey == "Samurai")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyHat>().setCostandVale(10000,10);
	}

}
