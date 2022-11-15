using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour {

	public static Achievement instance;

	[Header("UI")]
	public GameObject MainMenuUI;
	public GameObject AchievementUI;

	[Header("Achievements")]
	public GameObject Level;
	public GameObject LevelComplete, Kill, KillComplete, Headshot, HeadshotComplete, BossKill, BossKillComplete;

	[Header("Sliders")]
	public Slider LevelSlider;
	public Slider KillSlider, HeadshotSlider, BossKillSlider;

	[Header("Texts")]
	public Text LevelAchievementTxt;
	public Text LevelProgressTxt, LevelRewardTxt;
	public Text KillAchievementTxt, KillProgressTxt, KillRewardTxt;
	public Text HeadshotAchievementTxt, HeadshotProgressTxt, HeadshotRewardTxt;
	public Text BossProgressTxt;

	[Header("ClaimBtns")]
	public Button LevelClaimBtn;
	public Button KillClaimBtn, HeadshotClaimBtn, BossKillClaimBtn;

	int LevelAchievement, KillAchievement,HeadshotAchievement;

	void Awake()
	{
		instance = this;
	}

	void Start () 
	{
		init ();
		SetAchievements ();
	}

	void init()
	{
		Level.SetActive (true);
		LevelComplete.SetActive (false);
		Kill.SetActive (true);
		KillComplete.SetActive (false);
		Headshot.SetActive (true);
		HeadshotComplete.SetActive (false);
		BossKill.SetActive (true);
		BossKillComplete.SetActive (false);
	}

	public void SetAchievements()
	{
		switch (PlayerPrefs.GetInt ("LevelAchievement", 0)) 
		{
		case 0:
			LevelAchievement = 10;
			ClaimBtnEnable ();
			LevelAchievementTxt.text = "Reach at Level 10";
			LevelProgressTxt.text = "(" + PlayerPrefs.GetInt ("Level", 1) + "/10)";
			LevelRewardTxt.text = "Get 500 coins";
			LevelSlider.maxValue = LevelAchievement;
			LevelSlider.value = PlayerPrefs.GetInt ("Level", 1);
			break;
		case 1:
			LevelAchievement = 25;
			ClaimBtnEnable ();
			LevelAchievementTxt.text = "Reach at Level 25";
			LevelProgressTxt.text = "(" + PlayerPrefs.GetInt ("Level", 1) + "/25)";
			LevelRewardTxt.text = "Get 1000 coins";
			LevelSlider.maxValue = LevelAchievement;
			LevelSlider.value = PlayerPrefs.GetInt ("Level", 1);
			break;
		case 2:
			LevelAchievement = 50;
			ClaimBtnEnable ();
			LevelAchievementTxt.text = "Reach at Level 50";
			LevelProgressTxt.text = "(" + PlayerPrefs.GetInt ("Level", 1) + "/50)";
			LevelRewardTxt.text = "Get 3000 coins";
			LevelSlider.maxValue = LevelAchievement;
			LevelSlider.value = PlayerPrefs.GetInt ("Level", 1);
			break;
		case 3:
			Level.SetActive (false);
			LevelComplete.SetActive (true);
			break;
		}
		switch (PlayerPrefs.GetInt ("KillAchievement", 0)) 
		{
		case 0:
			KillAchievement = 50;
			ClaimBtnEnable ();
			KillAchievementTxt.text = "Kill 50 Enemies";
			KillProgressTxt.text = "(" + PlayerPrefs.GetInt ("AchievementKill", 0) + "/50)";
			KillRewardTxt.text = "Get 100 coins";
			KillSlider.maxValue = KillAchievement;
			KillSlider.value = PlayerPrefs.GetInt ("AchievementKill", 0);
			break;
		case 1:
			KillAchievement = 150;
			ClaimBtnEnable ();
			KillAchievementTxt.text = "Kill 150 Enemies";
			KillProgressTxt.text = "(" + PlayerPrefs.GetInt ("AchievementKill", 0) + "/150)";
			KillRewardTxt.text = "Get 300 coins";
			KillSlider.maxValue = KillAchievement;
			KillSlider.value = PlayerPrefs.GetInt ("AchievementKill", 0);
			break;
		case 2:
			KillAchievement = 300;
			ClaimBtnEnable ();
			KillAchievementTxt.text = "Kill 300 Enemies";
			KillProgressTxt.text = "(" + PlayerPrefs.GetInt ("AchievementKill", 0) + "/300)";
			KillRewardTxt.text = "Unlock Cold Steel 4 Edge Star";
			KillSlider.maxValue = KillAchievement;
			KillSlider.value = PlayerPrefs.GetInt ("AchievementKill", 0);
			break;
		case 3:
			Kill.SetActive (false);
			KillComplete.SetActive (true);
			break;
		}
		switch (PlayerPrefs.GetInt ("HeadshotAchievement", 0))
		{
		case 0:
			HeadshotAchievement = 25;
			ClaimBtnEnable ();
			HeadshotAchievementTxt.text = "25 Headshots";
			HeadshotProgressTxt.text = "(" + PlayerPrefs.GetInt ("AchievementHeadshot", 0) + "/25)";
			HeadshotRewardTxt.text = "Get 50 coins";
			HeadshotSlider.maxValue = HeadshotAchievement;
			HeadshotSlider.value = PlayerPrefs.GetInt ("AchievementHeadshot", 0);
			break;
		case 1:
			HeadshotAchievement = 50;
			ClaimBtnEnable ();
			HeadshotAchievementTxt.text = "50 Headshots";
			HeadshotProgressTxt.text = "(" + PlayerPrefs.GetInt ("AchievementHeadshot", 0) + "/50)";
			HeadshotRewardTxt.text = "Get 100 coins";
			HeadshotSlider.maxValue = HeadshotAchievement;
			HeadshotSlider.value = PlayerPrefs.GetInt ("AchievementHeadshot", 0);
			break;
		case 2:
			HeadshotAchievement = 100;
			ClaimBtnEnable ();
			HeadshotAchievementTxt.text = "100 Headshots";
			HeadshotProgressTxt.text = "(" + PlayerPrefs.GetInt ("AchievementHeadshot", 0) + "/100)";
			HeadshotRewardTxt.text = "Unlock Ninja Cap";
			HeadshotSlider.maxValue = HeadshotAchievement;
			HeadshotSlider.value = PlayerPrefs.GetInt ("AchievementHeadshot", 0);
			break;
		case 3:
			Headshot.SetActive (false);
			HeadshotComplete.SetActive (true);
			break;
		}
		switch (PlayerPrefs.GetInt ("BossKillAchievement", 0))
		{
		case 0:
			ClaimBtnEnable ();
			BossProgressTxt.text = "(" + (int)(PlayerPrefs.GetInt ("Level", 1) / 3) + "/10)";
			BossKillSlider.value = (int)(PlayerPrefs.GetInt ("Level", 1) / 3);
			break;
		case 1:
			BossKill.SetActive (false);
			BossKillComplete.SetActive (true);
			break;
		}

	}

	void ClaimBtnEnable()
	{
		if (Achievement.instance.LevelSlider.value >= Achievement.instance.LevelSlider.maxValue)
			LevelClaimBtn.GetComponent<Button> ().interactable = true;
		else
			LevelClaimBtn.GetComponent<Button> ().interactable = false;

		if (Achievement.instance.KillSlider.value >= Achievement.instance.KillSlider.maxValue)
			KillClaimBtn.GetComponent<Button> ().interactable = true;
		else
			KillClaimBtn.GetComponent<Button> ().interactable = false;

		if (Achievement.instance.HeadshotSlider.value >= Achievement.instance.HeadshotSlider.maxValue)
			HeadshotClaimBtn.GetComponent<Button> ().interactable = true;
		else
			HeadshotClaimBtn.GetComponent<Button> ().interactable = false;

		if (Achievement.instance.BossKillSlider.value >= Achievement.instance.BossKillSlider.maxValue)
			BossKillClaimBtn.GetComponent<Button> ().interactable = true;
		else
			BossKillClaimBtn.GetComponent<Button> ().interactable = false;
	}

	public void ClaimBtn(int index)
	{
		switch (index)
		{
		case 0:
			if (PlayerPrefs.GetInt ("Level", 1) >= LevelAchievement)
			{
				audioManager.instance.PlaySound ("Click");
				if (PlayerPrefs.GetInt ("LevelAchievement", 0) == 0) 
				{
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt ("Money") + 500);
					HomeUIManager.insta.MoneyTxt.text = "" + PlayerPrefs.GetInt ("Money");
					PlayerPrefs.SetInt ("LevelAchievement",1);
					SetAchievements ();
				}
				else if (PlayerPrefs.GetInt ("LevelAchievement", 0) == 1) 
				{
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt ("Money") + 1000);
					HomeUIManager.insta.MoneyTxt.text = "" + PlayerPrefs.GetInt ("Money");
					PlayerPrefs.SetInt ("LevelAchievement",2);
					SetAchievements ();
				}
				else if (PlayerPrefs.GetInt ("LevelAchievement", 0) == 2) 
				{
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt ("Money") + 3000);
					HomeUIManager.insta.MoneyTxt.text = "" + PlayerPrefs.GetInt ("Money");
					PlayerPrefs.SetInt ("LevelAchievement",3);
					SetAchievements ();
				}
			}
			break;
		case 1:
			if(PlayerPrefs.GetInt("AchievementKill",0) >= KillAchievement)
			{
				audioManager.instance.PlaySound ("Click");
				if (PlayerPrefs.GetInt ("KillAchievement",0) == 0)
				{
					PlayerPrefs.SetInt ("Money", PlayerPrefs.GetInt ("Money") + 100);
					HomeUIManager.insta.MoneyTxt.text = "" + PlayerPrefs.GetInt ("Money");
					PlayerPrefs.SetInt ("AchievementKill", 0);
					PlayerPrefs.SetInt ("KillAchievement",1);
					SetAchievements ();
				}
				else if (PlayerPrefs.GetInt ("KillAchievement") == 1)
				{
					PlayerPrefs.SetInt ("Money", PlayerPrefs.GetInt ("Money") + 300);
					HomeUIManager.insta.MoneyTxt.text = "" + PlayerPrefs.GetInt ("Money");
					PlayerPrefs.SetInt ("AchievementKill", 0);
					PlayerPrefs.SetInt ("KillAchievement",2);
					SetAchievements ();
				}
				else if (PlayerPrefs.GetInt ("KillAchievement") == 2)
				{
					PlayerPrefs.SetInt("ColdSteel4EdgeStar", 1);
					PlayerPrefs.SetInt ("AchievementKill", 0);
					PlayerPrefs.SetInt ("KillAchievement",3);
					SetAchievements ();
				}
			}
			break;
		case 2:
			if(PlayerPrefs.GetInt("AchievementHeadshot",0) >= HeadshotAchievement)
			{
				audioManager.instance.PlaySound ("Click");
				if(PlayerPrefs.GetInt("HeadshotAchievement",0) == 0)
				{
					PlayerPrefs.SetInt ("Money", PlayerPrefs.GetInt ("Money") + 50);
					HomeUIManager.insta.MoneyTxt.text = "" + PlayerPrefs.GetInt ("Money");
					PlayerPrefs.SetInt ("AchievementHeadshot", 0);
					PlayerPrefs.SetInt ("HeadshotAchievement",1);
					SetAchievements ();
				}
				else if(PlayerPrefs.GetInt("HeadshotAchievement") == 1)
				{
					PlayerPrefs.SetInt ("Money", PlayerPrefs.GetInt ("Money") + 100);
					HomeUIManager.insta.MoneyTxt.text = "" + PlayerPrefs.GetInt ("Money");
					PlayerPrefs.SetInt ("AchievementHeadshot", 0);
					PlayerPrefs.SetInt ("HeadshotAchievement",2);
					SetAchievements ();
				}
				else if(PlayerPrefs.GetInt("HeadshotAchievement") == 2)
				{
					PlayerPrefs.SetInt ("NinjaCap",1);
					PlayerPrefs.SetInt ("AchievementHeadshot", 0);
					PlayerPrefs.SetInt ("HeadshotAchievement",3);
					SetAchievements ();
				}
			}
			break;
		case 3:
			if(PlayerPrefs.GetInt("Level",1)>=30)
			{
				audioManager.instance.PlaySound ("Click");
				if (PlayerPrefs.GetInt ("BossKillAchievement", 0) == 0)
				{
					PlayerPrefs.SetInt ("Money", PlayerPrefs.GetInt ("Money") + 2500);
					HomeUIManager.insta.MoneyTxt.text = "" + PlayerPrefs.GetInt ("Money");
					PlayerPrefs.SetInt ("BossKillAchievement",1);
					SetAchievements ();
				}
			}
			break;
		}
		HomeUIManager.insta.checkAchievement ();
	}

	public void BackBtn()
	{
		audioManager.instance.PlaySound ("Click");
		MainMenuUI.SetActive (true);
		AchievementUI.SetActive (false);
	}

}
