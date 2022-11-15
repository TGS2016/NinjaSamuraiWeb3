using Defective.JSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseManager : MonoBehaviour
{


    public const string getProfile_api = "https://firestore.googleapis.com/v1/projects/metahackprojects/databases/(default)/documents/gta_userdata/";

    public const string getgameNFTData_api = "https://firestore.googleapis.com/v1/projects/metahackprojects/databases/(default)/documents/gta_gamedata/nft_data";


    #region Singleton
    public static DatabaseManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    
    #endregion



    [SerializeField] private LocalData data=new LocalData();

    public LocalData GetLocalData()
    {
       
        return data;
    }


    private void Start()
    {
       
    }
    IEnumerator updateProfile(int dataType, bool createnew = false)
    {

        JSONObject a = new JSONObject();
        JSONObject b = new JSONObject();
        JSONObject c = new JSONObject();
        //JSONObject d = new JSONObject();

        string url = getProfile_api + PlayerPrefs.GetString("Account", "test").ToLower();
        switch (dataType)
        {
            case 0:

                if (!createnew) url += "?updateMask.fieldPaths=userdata";
                else
                {
                    data = new LocalData();
                    data.name = "Player" + UnityEngine.Random.Range(0,99999).ToString();
                   /* if (PlayerCarsInfo.Instance)
                    {
                        data.carDetails = PlayerCarsInfo.Instance.carDefaultData;
                       
                    }*/
                }

                c.AddField("stringValue", Newtonsoft.Json.JsonConvert.SerializeObject(data));
                b.AddField("userdata", c);
                break;
           /* case 3:
                if (!createnew) url += "?updateMask.fieldPaths=gamedata";
                c.AddField("stringValue", PlayerPrefs.GetString("data"));
                b.AddField("gamedata", c);
                break;*/
        }

        WWWForm form = new WWWForm();

        Debug.Log("TEST updateProfile");

        // Serialize body as a Json string
        //string requestBodyString = "";



        a.AddField("fields", b);

        Debug.Log(a.Print(true));

        // Convert Json body string into a byte array
        byte[] requestBodyData = System.Text.Encoding.UTF8.GetBytes(a.Print());

        using (UnityWebRequest www = UnityWebRequest.Put(url, requestBodyData))
        {
            www.method = "PATCH";

            // Set request headers i.e. conent type, authorization etc
            //www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Content-length", (requestBodyData.Length.ToString()));
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
               /*  Debug.Log(www.downloadHandler.text);
                 if (UIManager.Instance)
                {
                    UIManager.Instance.UpdatePlayerUIData(true, data);
                }

                if (createnew)
                {

                    EvmosManager.Instance.EnablePlayPanels();
                    UIManager.Instance.OpenEditProfile();
                }*/
            }
        }
    }

    IEnumerator CheckProfile(bool firstTime = false)
    {
        string url = getProfile_api + PlayerPrefs.GetString("Account", "test2").ToLower();

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            //www.method = "PATCH";

            // Set request headers i.e. conent type, authorization etc
            //www.SetRequestHeader("Content-Type", "application/json");
            // www.SetRequestHeader("Content-length", (requestBodyData.Length.ToString()));
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Profile not found " + www.downloadHandler.text);
                //Debug.Log(www.error);
                Debug.Log(www.downloadHandler.text);

                StartCoroutine(updateProfile(0, true));
            }
            else
            {
                //TEST RESET DATA
               //StartCoroutine(updateProfile(0, true));

               JSONObject obj = new JSONObject(www.downloadHandler.text);
                Debug.Log(obj);
                //Debug.Log("CheckProfile " + www.downloadHandler.text);
                data = Newtonsoft.Json.JsonConvert.DeserializeObject<LocalData>(obj.GetField("fields").GetField("userdata").GetField("stringValue").stringValue);

              /*  if (UIManager.Instance) {
                    UIManager.Instance.username = data.name;
                    UIManager.Instance.user_char = data.char_id;

                    UIManager.Instance.UpdateUserName(data.name, SingletonDataManager.userethAdd);
                }

                EvmosManager.Instance.EnablePlayPanels();

                if (PlayerCarsInfo.Instance)
                {
                    PlayerCarsInfo.Instance.SetCarsData();
                }*/

                
             
            }
        }
    }

    
  
    
    

    public void GetData(bool firstTime=false)
    {
        StartCoroutine(CheckProfile(firstTime));
        //ConvertEpochToDatatime(1659504437);
    }

    public void UpdateData(LocalData localData)
    {
        data = localData;
        StartCoroutine(updateProfile(0));
    }
   
    


    public void ChangeGenderAndNameData(string username)
    {
      
        data.name = username;
        UpdateData(data);
       // UIManager.username = username;        
    }

    public string GetUserName()
    {
        if (data != null)
        {
            return data.name;
        }
        else
        {
            return "";
        }
    }
}
[System.Serializable]
public class LocalData
{

    public string name;
    public int char_id;
    public int coins = 0;
    public int selected_car=1;
    public string last_spin_time= "0";    
    public List<TranscationInfo> transactionsInformation = new List<TranscationInfo>();
    public List<CarUpgradeInfo> carDetails = new List<CarUpgradeInfo>();
    public int missionCompleted=0;

    public LocalData()
    {

        name = "Player";
        char_id = 0;
        coins = 0;
        selected_car = 1;
        last_spin_time = "0";
        transactionsInformation = new List<TranscationInfo>();
        carDetails = new List<CarUpgradeInfo>();
        missionCompleted = 0;

    }

}
[System.Serializable]
public class CarUpgradeInfo
{
    public int car_index;
    public int carCost;
    public bool isBought = false;
    public int current_acceleratoin_level;
    public int current_speed_level;
    public int current_braking_level;
    public int current_nitrus_level;
    public int current_handling_level;
    public float handling_amount;



}



[System.Serializable]
public class TranscationInfo
{
    public string transactionId;
    public string transactionStatus;
    public int coinAmount;
    public TranscationInfo(string Id, string status)
    {
        transactionId = Id;
        transactionStatus = status;        
    }
}