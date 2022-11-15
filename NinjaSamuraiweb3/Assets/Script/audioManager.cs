using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{

    //Store Clip Name
    public string name;
    //Store Clip
    public AudioClip clip;
    //Clip Volume in Range 0 - 1
    [Range(0f, 1f)]
    public float volume = 0.7f;
    //Clip Pitch in Range 0.5 - 1.5
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;
    //Repeat clip or not
    public bool loop = false;

    public AudioMixerGroup MixerGroup;

    private AudioSource source;

    //Gives Clip Source to the Audio Source which to play
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.outputAudioMixerGroup = MixerGroup;
    }

    //Plays the Clip
    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }
}

public class audioManager : MonoBehaviour
{

    public static audioManager instance;

    public bool fbAnalitic, enableGoogleAnalytics;
    public string GoogleAnalyticsId_Android, GoogleAnalyticsId_IOS;

    [SerializeField]
    Sound[] sounds;

    void Awake()
    {

        Application.targetFrameRate = 60;

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }


        if (fbAnalitic)
            initFacebookSDK();
        LogScreen("GameStart");
    }

    void Start()
    {
        //sets all sounds clip
        for (int i = 0; i < sounds.Length; i++)
        {

            GameObject _go = new GameObject("Sound_ " + i + " _" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());

        }
        //play the background clip
        if (PlayerPrefs.GetInt("Music", 1) == 1)
        {
            PlaySound("HomeBackground");
        }



    }
    //finds the clip and play that
    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            //if clip found than plays that clip and return
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.Log("No Sound");
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            //if clip found than stops that clip and return
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }
        Debug.Log("No Sound");
    }

    //////// FACEBOOK ANALITICS //////////
    void initFacebookSDK()
    {
        if (PlayerPrefs.GetInt("IsFbUrlInit", 0) == 0)
        {
            if (fbAnalitic)
            {
                PlayerPrefs.SetInt("IsFbUrlInit", 1);
                string url = "";
#if UNITY_ANDROID
                url = "http://proapi.app4edu.com/api/ninja_android.php?myToken=Tu6&&myKey=784"; //android
#else
				url = "http://proapi.app4edu.com/api/ninja_ios.php?myToken=Tu6&&myKey=784"; ///ios
#endif
                WWW www = new WWW(url);
                StartCoroutine(WaitForRequest(www));
            }
        }
        else if (PlayerPrefs.GetInt("EnaSDK") == 1)
            enableSDK();
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
            if (www.text.ToString().Equals("1"))
            {
                Debug.Log("enable sdk");
                PlayerPrefs.SetInt("EnaSDK", 1);
                Debug.Log("EnaSDk");
                enableSDK();
            }
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
        www.Dispose();
    }

    private void enableSDK()
    {
        Debug.Log("initFacbook");
        //		gameObject.AddComponent<initFacebook> ();
    }

    //////////// GOOGLE ANALYTICS  ///////////////////
    public void LogScreen(string title)
    {
        if (enableGoogleAnalytics)
        {
            string screenRes = Screen.width + "x" + Screen.height;

            string clientID = SystemInfo.deviceUniqueIdentifier;

            title = WWW.EscapeURL(title);
#if UNITY_EDITOR
            var url = "https://www.google-analytics.com/collect?v=1&ul=en-us&t=pageview&sr=" + screenRes + "&an=" + WWW.EscapeURL(Application.productName.ToString()) + "&a=448166238&tid=" + GoogleAnalyticsId_Android + "&aid=" + Application.identifier.ToString() + "&cid=" + WWW.EscapeURL(clientID) + "&_u=.sB&av=" + Application.version.ToString() + "&_v=ma1b3&cd=" + title + "&qt=600&z=185&dp=" + title + "&dt=" + title;
#elif UNITY_ANDROID
			var url = "https://www.google-analytics.com/collect?v=1&ul=en-us&t=pageview&sr=" + screenRes + "&an=" + WWW.EscapeURL (Application.productName.ToString ()) + "&a=448166238&tid=" + GoogleAnalyticsId_Android + "&aid=" + Application.identifier.ToString () + "&cid=" + WWW.EscapeURL (clientID) + "&_u=.sB&av=" + Application.version.ToString () + "&_v=ma1b3&cd=" + title + "&qt=600&z=185&dp=" + title + "&dt=" + title;
#else
			var url = "https://www.google-analytics.com/collect?v=1&ul=en-us&t=pageview&sr=" + screenRes + "&an=" + WWW.EscapeURL (Application.productName.ToString ()) + "&a=448166238&tid=" + GoogleAnalyticsId_IOS + "&aid=" + Application.identifier.ToString () + "&cid=" + WWW.EscapeURL (clientID) + "&_u=.sB&av=" + Application.version.ToString () + "&_v=ma1b3&cd=" + title + "&qt=600&z=185&dp=" + title + "&dt=" + title;
#endif

            WWW www = new WWW(url);
            StartCoroutine(WaitForRequest2(www));
            //www.Dispose ();
            Debug.Log(url + " and " + www);
        }
        else
            Debug.Log("Google Analytics disabled");
    }
    //analytics sdk
    IEnumerator WaitForRequest2(WWW www)
    {
        yield return www;
        www.Dispose();
    }






}
