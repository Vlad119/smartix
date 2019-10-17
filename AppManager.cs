using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using UnityEngine.Events;

public class AppManager : MonoBehaviour
{
    [HideInInspector] public static AppManager Instance;
    public GameObject[] screens;
    public GameObject messages;
    public GameObject fon_message;
    public TMP_Text fon_message_text;
    public int activeScreenIndex;
    public List<int> prevScreenIndex;
    public string my_push_token;
    public Texture2D user_maptex;
    public int codeLength = 4;
    public UserInfo userInfo = new UserInfo();
    public Res res = new Res();

    public string inn;

    public void CheckCode(TMP_InputField field)
    {
        if (field.text.Length == codeLength)
        {
            var uInfo = AppManager.Instance.userInfo;
            var user = AppManager.Instance.userInfo.user;
            user.code = Convert.ToInt32(field.text);
            WebHandler.Instance.LoginWraper((repl) =>
            {
                userInfo = JsonUtility.FromJson<UserInfo>(repl);
                SaveUserInfo(userInfo);
                print(PlayerPrefs.GetString("userInfo"));
                user.push_token = my_push_token;
                WebHandler.Instance.UpdateUserWrapper((resp) =>
                {}, JsonUtility.ToJson(uInfo)
                );
                SwitchScreen(2);
                WebHandler.Instance.UpdateUserWrapper((resp) =>
                {
                    JsonUtility.FromJsonOverwrite(resp, uInfo);
                    AppManager.Instance.SaveUserInfo(uInfo);
                }, JsonUtility.ToJson(uInfo));

            });
            AppManager.Instance.userInfo.user.firstTime = false;
        }
    }


    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackButton();
            }
        }
    }


    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        my_push_token = token.Token;
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }

    public virtual void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
       // Debug.Log("Received a new message");
        var notification = e.Message.Notification;
        if (notification != null)
        {
           // Debug.Log("title: " + notification.Title);
          //  Debug.Log("body: " + notification.Body);
        }
        if (e.Message.From.Length > 0)
           // Debug.Log("from: " + e.Message.From);
        if (e.Message.Link != null)
        {
           // Debug.Log("link: " + e.Message.Link.ToString());
        }
        if (e.Message.Data.Count > 0)
        {
          //  Debug.Log("data:");
            foreach (System.Collections.Generic.KeyValuePair<string, string> iter in
                e.Message.Data)
            {
               // Debug.Log("  " + iter.Key + ": " + iter.Value);
            }
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("userInfo"))
        {
            LoadUser();
            SwitchScreen(2);
        }
    }


    private void OnEnable()
    {
        
        print(PlayerPrefs.GetString("userInfo"));
    }

    public void ReloadScreen()
    {
        screens[activeScreenIndex].SetActive(false);
        screens[activeScreenIndex].SetActive(true);
    }
    private async Task InitializeScreen()
    {
        if (WebHandler.Instance == null || AppManager.Instance.userInfo.access_token == "")
            await new WaitForSeconds(.01f);
    }

    private void Awake()
    {

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //   app = Firebase.FirebaseApp.DefaultInstance;
                Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
                Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
                Firebase.Messaging.FirebaseMessaging.SubscribeAsync("/topics/active_users");

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    public void SwitchScreen(int nextScreenIndex)
    {
        if (activeScreenIndex < nextScreenIndex)
        {
            //play left to right animation
        }
        else if (activeScreenIndex < nextScreenIndex)
        {
            //play right to left animation
        }
        else
        {
            //do nothing
        }
        //yield return transferAnimationLength;

        if (nextScreenIndex == -1)
        {
            activeScreenIndex = prevScreenIndex[prevScreenIndex.Count - 1];
            prevScreenIndex.RemoveAt(prevScreenIndex.Count - 1);
        }
        else
        {
            prevScreenIndex.Add(activeScreenIndex);
            activeScreenIndex = nextScreenIndex;
        }
        for (int i = 0; i < screens.Length; i++)
        {
            if (i != activeScreenIndex)
                screens[i].SetActive(false);
        }
        screens[activeScreenIndex].SetActive(true);
    }


    public void SaveUserInfo(UserInfo userInfo)
    {
        var toSave = JsonUtility.ToJson(userInfo);
        PlayerPrefs.SetString("userInfo", toSave);
    }


    public void LoadUser()
    {
        var toUnravel = PlayerPrefs.GetString("userInfo");
        userInfo = JsonUtility.FromJson<UserInfo>(toUnravel);
    }


    public void BackButton()
    {
        SwitchScreen(-1);
    }
}
