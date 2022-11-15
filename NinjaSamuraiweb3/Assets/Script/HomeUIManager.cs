using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.Advertisements;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using Defective.JSON;


#if UNITY_IOS
	//using System.Runtime.InteropServices;
	using UnityEngine.SocialPlatforms;
#endif

public class HomeUIManager : MonoBehaviour
{

    public static HomeUIManager insta;

    public GameObject HomeUI, MainMenuUI, AchievementUI, SettingsUI, WeaponUI, StoreUI, HelmetUI, IAPUI, RewardPanel, NoAdVideoPanel, BG, RateScreen, regScreen, logBtn, RegBtn, lotOutBtn;
    public Image Highlight;

    public Sprite[] BackgroungSprite;

    public int rateEverylaunch;

    public Text MoneyTxt;

    private string restoreDatastring = null;

    public static int btnType;


#if UNITY_ANDROID
    string ANDROID_LINK = "https://play.google.com/store/apps/details?id=com.tgs.nssf";
#elif UNITY_IOS
		string IOS_LINK = "https://itunes.apple.com/app/id1441387403";
#endif

    void Awake()
    {
        insta = this;
    }

    //sets the camera size and background image size based on camera size and display ad banner
    void Start()
    {
        float horizontalResolution = 1920f;
        float currentAspect = (float)Screen.width / (float)Screen.height;
        Camera.main.orthographicSize = horizontalResolution / currentAspect / 200;

        SetBackGroundScale();
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 50));
        MoneyTxt.text = "" + PlayerPrefs.GetInt("Money");

        Achievement.instance.SetAchievements();
        checkAchievement();

#if UNITY_ANDROID
        //Advertisement.Initialize("2888015");
        logBtn.SetActive(false);
        RegBtn.SetActive(false);
        lotOutBtn.SetActive(false);
#elif UNITY_IOS
			//Advertisement.Initialize ("2888016");
			
#endif



#if !UNITY_ANDROID
		if(PlayerPrefs.GetInt ("login", 0) == 1 && !string.IsNullOrEmpty(PlayerPrefs.GetString("uname")) && !string.IsNullOrEmpty(PlayerPrefs.GetString("upass"))) { //skip if older data is available
			logBtn.SetActive(false);
			RegBtn.SetActive(false);
			lotOutBtn.SetActive(true);
		}else{
			lotOutBtn.SetActive(false);
		}
#endif

    }



#if UNITY_ANDROID
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && RateScreen.activeSelf)
        {
            closerateUs();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && NoAdVideoPanel.activeSelf)
        {
            NoAdPanelContinueBtn();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && StoreUI.activeSelf)
        {
            StoreBackBtn();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && HomeUI.activeSelf && !RewardPanel.activeSelf)
        {
            //audioManager.instance.PlaySound ("Click");
            Application.Quit();
            Debug.Log("Quit");
        }
    }
#endif

    public void checkAchievement()
    {
        if ((Achievement.instance.LevelSlider.value >= Achievement.instance.LevelSlider.maxValue && PlayerPrefs.GetInt("LevelAchievement", 0) != 3)
            || (Achievement.instance.KillSlider.value >= Achievement.instance.KillSlider.maxValue && PlayerPrefs.GetInt("KillAchievement", 0) != 3)
            || (Achievement.instance.HeadshotSlider.value >= Achievement.instance.HeadshotSlider.maxValue && PlayerPrefs.GetInt("HeadshotAchievement", 0) != 3)
            || (Achievement.instance.BossKillSlider.value >= Achievement.instance.BossKillSlider.maxValue && PlayerPrefs.GetInt("BossKillAchievement", 0) != 1))
        {
            Highlight.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            Highlight.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    //loads the game scene
    public void PlayBtn()
    {
        audioManager.instance.PlaySound("Click");
        audioManager.instance.StopSound("HomeBackground");
        audioManager.instance.PlaySound("Background");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //open the store
    public void StoreBtn()
    {
        audioManager.instance.PlaySound("Click");
        HomeUI.SetActive(false);
        StoreUI.SetActive(true);

        BG.GetComponent<SpriteRenderer>().sprite = BackgroungSprite[1];
        SetBackGroundScale();
    }
    //goes to weapon store
    public void WeaponBtn()
    {
        audioManager.instance.PlaySound("Click");
        //disable the home ui and display the weapon ui
        StoreUI.SetActive(false);
        WeaponUI.SetActive(true);
    }
    //goes to hat store
    public void HelmetBtn()
    {
        audioManager.instance.PlaySound("Click");
        //disable the home ui and display the weapon ui
        StoreUI.SetActive(false);
        HelmetUI.SetActive(true);
    }

    public void AchievementBtn()
    {
        audioManager.instance.PlaySound("Click");
        MainMenuUI.SetActive(false);
        AchievementUI.SetActive(true);
    }

    //open google play or itune store for rating
    public void RateBtn()
    {
        audioManager.instance.PlaySound("Click");
#if UNITY_ANDROID
        Application.OpenURL(ANDROID_LINK);
        Debug.Log("Rate");
#elif UNITY_IOS
		Application.OpenURL(IOS_LINK);
#endif
    }

    public void closerateUs()
    {
        audioManager.instance.PlaySound("Click");
        RateScreen.SetActive(false);
    }

    //open game settings
    public void SettingsBtn()
    {
        audioManager.instance.PlaySound("Click");
        //disable the home ui and display the settings ui
        HomeUI.SetActive(false);
        SettingsUI.SetActive(true);
    }
    //close the application
    public void QuitBtn()
    {
        //audioManager.instance.PlaySound ("Click");
        Application.Quit();
        Debug.Log("Quit");
    }
    //closes the settings
    public void SettingsBackBtn()
    {
        audioManager.instance.PlaySound("Click");
        SettingsUI.SetActive(false);
        HomeUI.SetActive(true);
        MoneyTxt.text = "" + PlayerPrefs.GetInt("Money", 50);
    }
    //closes the weapon store and hat store 
    public void BackBtn()
    {
        HomeUIManager.insta.saveData();
        audioManager.instance.PlaySound("Click");
        //disable the settings UI & weapon UI and display home UI 
        WeaponUI.SetActive(false);
        HelmetUI.SetActive(false);
        StoreUI.SetActive(true);
    }
    //comes back from store to home
    public void StoreBackBtn()
    {
        audioManager.instance.PlaySound("Click");
        HomeUI.SetActive(true);
        StoreUI.SetActive(false);
        MoneyTxt.text = "" + PlayerPrefs.GetInt("Money", 50);
        BG.GetComponent<SpriteRenderer>().sprite = BackgroungSprite[0];
        SetBackGroundScale();
    }
    //closes the no ad panel
    public void NoAdPanelContinueBtn()
    {
        NoAdVideoPanel.SetActive(false);
    }
    //Play reward video
    public void RewardBtn()
    {
        audioManager.instance.PlaySound("Click");
            NoAdVideoPanel.SetActive(true);
            

        /*if (Advertisement.IsReady ("rewardedVideo")) 
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show ("rewardedVideo", options);
		} 
		else 
		{
			
		}*/
    }
    /*
	//check if ad played skipped or failed
	void HandleShowResult(ShowResult result)
	{
        
		switch (result) 
		{
			case ShowResult.Finished:
				Debug.Log ("Ad Played successfully");
				PlayerPrefs.SetInt ("Money",PlayerPrefs.GetInt("Money",50) + 50);
				MoneyTxt.text = "" + PlayerPrefs.GetInt ("Money",50);
				Debug.Log ("Reward" + PlayerPrefs.GetInt("Money"));
				Advertisement.Initialize ("2829189",true);
				RewardPanel.SetActive (true);
			break;

			case ShowResult.Skipped:
				
				Debug.Log ("Ad Skipped");
			break;

			case ShowResult.Failed:
				Debug.Log ("Fail to Load Ad");
				NoAdVideoPanel.SetActive (true);
			break;
		}
	}
    */
    //open IAP Panel
    public void IAPBtn()
    {
        audioManager.instance.PlaySound("Click");
        IAPUI.SetActive(true);
        HomeUI.SetActive(false);
        //BG.GetComponent<SpriteRenderer>().sprite = BackgroungSprite[1];
        //SetBackGroundScale ();
    }
    //closes IAP panel
    public void IAPBackBtn()
    {
        audioManager.instance.PlaySound("Click");
        IAPUI.SetActive(false);
        HomeUI.SetActive(true);
        MoneyTxt.text = "" + PlayerPrefs.GetInt("Money", 50);
        Debug.Log("IAP");
        BG.GetComponent<SpriteRenderer>().sprite = BackgroungSprite[0];
    }
    //set the sharing text of app
    public void ShareBtn()
    {
        audioManager.instance.PlaySound("Click");
    
    }
    //set the background image size based on screen size
    void SetBackGroundScale()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;

        Sprite s = BG.GetComponent<SpriteRenderer>().sprite;

        float unitWidth = s.textureRect.width / s.pixelsPerUnit;
        float unitHeight = s.textureRect.height / s.pixelsPerUnit;

        BG.transform.localScale = new Vector3(width / unitWidth, height / unitHeight) + new Vector3(0.02f, 0.02f, 0.02f);
    }


    //== game data save on server methods ==//



    public InputField username, pass1, pass2;
    public Text infoText;
    public GameObject inputPassF2;
    private string uname, upass;

    //reg or login
    public void openRegScreen(int myType)
    {
        btnType = myType; //set screen type
        if (btnType == 1)
        { //login
            infoText.text = "Enter username and password to login";
            inputPassF2.SetActive(false);
        }
        else if (btnType == 2)
        { // register
            infoText.text = "Enter username and password to register";
            inputPassF2.SetActive(true);
        }
        regScreen.SetActive(true);
    }

    public void closeRegScreen()
    {
        regScreen.SetActive(false);
    }

    public void logOut()
    {
        PlayerPrefs.SetInt("login", 0);
        PlayerPrefs.SetString("uname", "");
        PlayerPrefs.SetString("upass", "");
        PlayerPrefs.Save();
        logBtn.SetActive(true);
        RegBtn.SetActive(true);
        lotOutBtn.SetActive(false);
    }

    public void submitBtn()
    {
        if (btnType == 1)
        { //login
            if (!string.IsNullOrEmpty(username.text) && !string.IsNullOrEmpty(pass1.text))
            {
                uname = username.text;
                upass = pass1.text;
                StartCoroutine(loginData());
            }
            else
            {
                infoText.text = "Error ! Check username or password";
            }
        }
        else if (btnType == 2)
        { // register
            if (username.text.Length < 4 || pass1.text.Length < 4)
            {
                infoText.text = "Error !\nNeed minimum 4 letter of username and password";
            }
            else if (!string.IsNullOrEmpty(username.text.ToString()) && !string.IsNullOrEmpty(pass1.text.ToString()) && pass1.text.ToString().CompareTo(pass2.text.ToString()) == 0)
            {
                uname = username.text;
                upass = pass1.text;
                StartCoroutine(registerUser());
            }
            else
            {
                infoText.text = "Error ! Check username or password";
            }
        }
    }

    // save date
    public void saveData()
    {
#if !UNITY_ANDROID
		if(PlayerPrefs.GetInt ("login", 0) == 1 && !string.IsNullOrEmpty(PlayerPrefs.GetString("uname")) && !string.IsNullOrEmpty(PlayerPrefs.GetString("upass"))) { //skip if older data is available
			//StartCoroutine(UploadData());
			uname = PlayerPrefs.GetString("uname");
			upass = PlayerPrefs.GetString("upass");
			StartCoroutine(updateData());
		}
#endif
    }

    // restore data
    public void restoreData()
    {

        JSONObject j = new JSONObject(restoreDatastring);

        string newValueK = Regex.Unescape(j.GetField("data").stringValue);
        JSONObject k = new JSONObject(newValueK);

        for (int i = 0; i < k.list.Count; i++)
        {
            string key = (string)k.keys[i];
            JSONObject jo = (JSONObject)k.list[i];
            Debug.Log(key + " and " + int.Parse(jo.stringValue));
            PlayerPrefs.SetInt(key, int.Parse(jo.stringValue));
        }

        PlayerPrefs.Save();
        MoneyTxt.text = "" + PlayerPrefs.GetInt("Money", 50);
        regScreen.SetActive(false);
        lotOutBtn.SetActive(true);

    }

    // register upload data
    IEnumerator registerUser()
    {
        //get data and convert to json
        JSONObject j = new JSONObject(JSONObject.Type.Object);
        // hats saving on server
        j.AddField("Bamboo", PlayerPrefs.GetInt("Bamboo", 0).ToString());
        j.AddField("Sherlock", PlayerPrefs.GetInt("Sherlock", 0).ToString());
        j.AddField("Pirate1", PlayerPrefs.GetInt("Pirate1", 0).ToString());
        j.AddField("Pirate2", PlayerPrefs.GetInt("Pirate2", 0).ToString());
        j.AddField("Cowboy", PlayerPrefs.GetInt("Cowboy", 0).ToString());
        j.AddField("Army1", PlayerPrefs.GetInt("Army1", 0).ToString());
        j.AddField("Army2", PlayerPrefs.GetInt("Army2", 0).ToString());
        j.AddField("Wizard", PlayerPrefs.GetInt("Wizard", 0).ToString());
        j.AddField("Samurai", PlayerPrefs.GetInt("Samurai", 0).ToString());
        j.AddField("NinjaCap", PlayerPrefs.GetInt("NinjaCap", 0).ToString());

        // weapons saving on server
        j.AddField("4EdgeStar", PlayerPrefs.GetInt("4EdgeStar", 0).ToString());
        j.AddField("5EdgeStar", PlayerPrefs.GetInt("5EdgeStar", 0).ToString());
        j.AddField("6EdgeStar", PlayerPrefs.GetInt("6EdgeStar", 0).ToString());
        j.AddField("8EdgeStar", PlayerPrefs.GetInt("8EdgeStar", 0).ToString());
        j.AddField("SpikedBall", PlayerPrefs.GetInt("SpikedBall", 0).ToString());
        j.AddField("RangedSpike", PlayerPrefs.GetInt("RangedSpike", 0).ToString());
        j.AddField("RangedNeedle", PlayerPrefs.GetInt("RangedNeedle", 0).ToString());
        j.AddField("Torpedo", PlayerPrefs.GetInt("Torpedo", 0).ToString());
        j.AddField("Sai", PlayerPrefs.GetInt("Sai", 0).ToString());
        j.AddField("Kunai", PlayerPrefs.GetInt("Kunai", 0).ToString());
        j.AddField("Knife", PlayerPrefs.GetInt("Knife", 0).ToString());
        j.AddField("ColdSteel4EdgeStar", PlayerPrefs.GetInt("ColdSteel4EdgeStar", 0).ToString());

        // other saving on server
        j.AddField("Weapon", PlayerPrefs.GetInt("Weapon", 0).ToString());
        j.AddField("Money", PlayerPrefs.GetInt("Money", 0).ToString());
        j.AddField("Hat", PlayerPrefs.GetInt("Hat", 0).ToString());
        j.AddField("Level", PlayerPrefs.GetInt("Level", 1).ToString());
        j.AddField("MaxHitPoint", PlayerPrefs.GetInt("MaxHitPoint", 10).ToString());
        j.AddField("Inerstitial", PlayerPrefs.GetInt("Inerstitial", 1).ToString());

        // achivement saving on server
        j.AddField("LevelAchievement", PlayerPrefs.GetInt("LevelAchievement", 0).ToString());
        j.AddField("KillAchievement", PlayerPrefs.GetInt("KillAchievement", 0).ToString());
        j.AddField("AchievementKill", PlayerPrefs.GetInt("AchievementKill", 0).ToString());
        j.AddField("HeadshotAchievement", PlayerPrefs.GetInt("HeadshotAchievement", 0).ToString());
        j.AddField("AchievementHeadshot", PlayerPrefs.GetInt("AchievementHeadshot", 0).ToString());
        j.AddField("BossKillAchievement", PlayerPrefs.GetInt("BossKillAchievement", 0).ToString());


        string encodedString = j.Print();
        Debug.Log("Data " + uname + " " + upass + encodedString);

        // send to server
        WWWForm form = new WWWForm();
        form.AddField("tag", "regUser");
        form.AddField("username", uname); // SystemInfo.deviceUniqueIdentifier.ToString()
        form.AddField("passcode", upass); // SystemInfo.deviceUniqueIdentifier.ToString()
        form.AddField("device_os", SystemInfo.operatingSystem.ToString());
        form.AddField("device_lang", Application.systemLanguage.ToString());
        form.AddField("device_version", Application.version.ToString());
        form.AddField("user_data", encodedString);
        //Debug.Log ("post " + form.data.ToString ());
        UnityWebRequest www = UnityWebRequest.Post("https://edu.thundergamestudio.com/ninjagame/api/fetch.php", form);
        //www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log("Form upload complete! " + www.downloadHandler.text);
            Debug.Log("Form upload complete! " + www.downloadHandler.text);
            restoreDatastring = www.downloadHandler.text;
            JSONObject k = new JSONObject(restoreDatastring);

            if (int.Parse(k.GetField("success").ToString()) == 1)
            { //check if data is available
                PlayerPrefs.SetInt("login", 1);
                PlayerPrefs.SetString("uname", username.text);
                PlayerPrefs.SetString("upass", pass1.text);
                PlayerPrefs.Save();
                Debug.Log("Reg success");
                regScreen.SetActive(false);
                logBtn.SetActive(false);
                RegBtn.SetActive(false);
                lotOutBtn.SetActive(true);

            }
            else
            {
                infoText.text = "Error !\nTry different username.\nThis username already registered.";
                Debug.Log("Reg Fail");
            }
        }
    }

    // login and get data
    IEnumerator loginData()
    {


        // send to server
        WWWForm form = new WWWForm();
        form.AddField("tag", "login");
        form.AddField("username", uname); // SystemInfo.deviceUniqueIdentifier.ToString()
        form.AddField("passcode", upass); // SystemInfo.deviceUniqueIdentifier.ToString()
                                          //form.AddField("device_id", PlayerPrefs.GetString ("uid")); // SystemInfo.deviceUniqueIdentifier.ToString()

        UnityWebRequest www = UnityWebRequest.Post("https://edu.thundergamestudio.com/ninjagame/api/fetch.php", form);
        //www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete! " + www.downloadHandler.text);

            restoreDatastring = www.downloadHandler.text;
            JSONObject j = new JSONObject(restoreDatastring);

            if (int.Parse(j.GetField("success").ToString()) == 1)
            { //check if data is available
                PlayerPrefs.SetInt("login", 1);
                PlayerPrefs.SetString("uname", username.text);
                PlayerPrefs.SetString("upass", pass1.text);
                PlayerPrefs.Save();
                restoreData();
                logBtn.SetActive(false);
                RegBtn.SetActive(false);
                lotOutBtn.SetActive(true);
            }
            else
            {

                infoText.text = "Error !\nIncorrect username or password";
            }

        }
    }

    //  upload data
    IEnumerator updateData()
    {
        //get data and convert to json
        JSONObject j = new JSONObject(JSONObject.Type.Object);
        // hats saving on server
        j.AddField("Bamboo", PlayerPrefs.GetInt("Bamboo", 0).ToString());
        j.AddField("Sherlock", PlayerPrefs.GetInt("Sherlock", 0).ToString());
        j.AddField("Pirate1", PlayerPrefs.GetInt("Pirate1", 0).ToString());
        j.AddField("Pirate2", PlayerPrefs.GetInt("Pirate2", 0).ToString());
        j.AddField("Cowboy", PlayerPrefs.GetInt("Cowboy", 0).ToString());
        j.AddField("Army1", PlayerPrefs.GetInt("Army1", 0).ToString());
        j.AddField("Army2", PlayerPrefs.GetInt("Army2", 0).ToString());
        j.AddField("Wizard", PlayerPrefs.GetInt("Wizard", 0).ToString());
        j.AddField("Samurai", PlayerPrefs.GetInt("Samurai", 0).ToString());
        j.AddField("NinjaCap", PlayerPrefs.GetInt("NinjaCap", 0).ToString());

        // weapons saving on server
        j.AddField("4EdgeStar", PlayerPrefs.GetInt("4EdgeStar", 0).ToString());
        j.AddField("5EdgeStar", PlayerPrefs.GetInt("5EdgeStar", 0).ToString());
        j.AddField("6EdgeStar", PlayerPrefs.GetInt("6EdgeStar", 0).ToString());
        j.AddField("8EdgeStar", PlayerPrefs.GetInt("8EdgeStar", 0).ToString());
        j.AddField("SpikedBall", PlayerPrefs.GetInt("SpikedBall", 0).ToString());
        j.AddField("RangedSpike", PlayerPrefs.GetInt("RangedSpike", 0).ToString());
        j.AddField("RangedNeedle", PlayerPrefs.GetInt("RangedNeedle", 0).ToString());
        j.AddField("Torpedo", PlayerPrefs.GetInt("Torpedo", 0).ToString());
        j.AddField("Sai", PlayerPrefs.GetInt("Sai", 0).ToString());
        j.AddField("Kunai", PlayerPrefs.GetInt("Kunai", 0).ToString());
        j.AddField("Knife", PlayerPrefs.GetInt("Knife", 0).ToString());
        j.AddField("ColdSteel4EdgeStar", PlayerPrefs.GetInt("ColdSteel4EdgeStar", 0).ToString());

        // other saving on server
        j.AddField("Weapon", PlayerPrefs.GetInt("Weapon", 0).ToString());
        j.AddField("Money", PlayerPrefs.GetInt("Money", 0).ToString());
        j.AddField("Hat", PlayerPrefs.GetInt("Hat", 0).ToString());
        j.AddField("Level", PlayerPrefs.GetInt("Level", 1).ToString());
        j.AddField("MaxHitPoint", PlayerPrefs.GetInt("MaxHitPoint", 10).ToString());
        j.AddField("Inerstitial", PlayerPrefs.GetInt("Inerstitial", 1).ToString());

        // achivement saving on server
        j.AddField("LevelAchievement", PlayerPrefs.GetInt("LevelAchievement", 0).ToString());
        j.AddField("KillAchievement", PlayerPrefs.GetInt("KillAchievement", 0).ToString());
        j.AddField("AchievementKill", PlayerPrefs.GetInt("AchievementKill", 0).ToString());
        j.AddField("HeadshotAchievement", PlayerPrefs.GetInt("HeadshotAchievement", 0).ToString());
        j.AddField("AchievementHeadshot", PlayerPrefs.GetInt("AchievementHeadshot", 0).ToString());
        j.AddField("BossKillAchievement", PlayerPrefs.GetInt("BossKillAchievement", 0).ToString());


        string encodedString = j.Print();
        Debug.Log("Data " + uname + " " + upass + encodedString);

        // send to server
        WWWForm form = new WWWForm();
        form.AddField("tag", "updateData");
        form.AddField("username", uname); // SystemInfo.deviceUniqueIdentifier.ToString()
        form.AddField("passcode", upass); // SystemInfo.deviceUniqueIdentifier.ToString()
        form.AddField("device_os", SystemInfo.operatingSystem.ToString());
        form.AddField("device_lang", Application.systemLanguage.ToString());
        form.AddField("device_version", Application.version.ToString());
        form.AddField("user_data", encodedString);
        //Debug.Log ("post " + form.data.ToString ());
        UnityWebRequest www = UnityWebRequest.Post("https://edu.thundergamestudio.com/ninjagame/api/fetch.php", form);
        //www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log("Form upload complete! " + www.downloadHandler.text);
            Debug.Log("Form upload complete! " + www.downloadHandler.text);
            restoreDatastring = www.downloadHandler.text;
            JSONObject k = new JSONObject(restoreDatastring);

            if (int.Parse(k.GetField("success").ToString()) == 1)
            { //check if data is available
                Debug.Log("Upload success");
            }
            else
            {
                infoText.text = "Try different username.\nThis username already registered.";
                Debug.Log("Upload Fail");
            }
        }
    }





}
