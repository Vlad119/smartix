using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class WebHandler : MonoBehaviour
{
    public static WebHandler Instance;
    public delegate UnityWebRequest RequestCall();
    public string servAddress;
    public string registerEndpoint;
    public string loginEndpoint;
    public string loadCargoEndpoint;
    public string loadDataEndpoint;
    public string logoutEndpoint;
    public string userEndpoint;
    public string updateUserEndpoint;
    public string sendfileEndpoint;
    public string innEndpoint;
    public List<string> img_cache_string;
    public List<Texture2D> img_cache_tex;
    public GameObject loadingScreen;
    public GameObject noInternet;

    private void Awake()
    {
        SingletonImplementation();
        // loadingScreen.SetActive(false);
    }

    #region Requests
    private async Task PostJson(string url, string dataString, UnityAction<string> DoIfSuccess = null, bool addTokenHeader = false)
    {
        var endUrl = servAddress + url;
        var req = await IRequestSend(() =>
        {
            var request = new UnityWebRequest(endUrl, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(dataString);
            var uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.uploadHandler = uploadHandler;
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            if (!addTokenHeader)
                request.SetRequestHeader("content-type", "application/json");
            else
            {
                request.SetRequestHeader("content-type", "application/json");
                request.SetRequestHeader("Token", AppManager.Instance.userInfo.access_token);
            }
            print(request.GetRequestHeader("content-type") + "   " + (addTokenHeader ? request.GetRequestHeader("token") : "") + "    " + request.url + "    " + Encoding.UTF8.GetString(request.uploadHandler.data));
            return request;
        });
        Debug.Log("All OK");
        Debug.Log("Status Code: " + req.responseCode);
        DoIfSuccess?.Invoke(req.downloadHandler.text);
    }

    private async Task PostMultipartAsync(string url, string textSctring, byte[] image = null, UnityAction<string> DoIfSuccess = null)
    {
        string endUrl = servAddress + url; /*"https://retailbonus.ru/13423.php";*/
        var req = await IRequestSend(() =>
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            if (image != null)
            {
                print("sending image");
                formData.Add(new MultipartFormFileSection("file", image, "name.png", "image/png"));
                formData.Add(new MultipartFormDataSection("text", textSctring));
            }
            else
            {
                print("sending text");
                formData.Add(new MultipartFormDataSection("text", textSctring));
            }
            UnityWebRequest request = UnityWebRequest.Post(endUrl, formData);
            // request.SetRequestHeader("Token", AppManager.Instance.userInfo.access_token);
            return request;
        });
        Debug.Log("Request sent");
        Debug.Log("Status code: " + req.responseCode);
        DoIfSuccess?.Invoke(req.downloadHandler.text);
    }


    private async Task GetRequest(string url, string getParameters = null, UnityAction<string> DoIfSuccess = null)
    {
        var endUrl = servAddress + url + (getParameters == null ? "" : getParameters);
        var req = await IRequestSend(() =>
        {
            var request = new UnityWebRequest(endUrl, "GET");
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("token", AppManager.Instance.userInfo.access_token);
            return request;
        });
          Debug.Log("Request sent");
        Debug.Log("Status code: " + req.responseCode);
        DoIfSuccess?.Invoke(req.downloadHandler.text);
    }


    private async Task LoadImage(string url, UnityAction<Texture2D> DoIfSuccess)
    {
        if (!(url.Contains(".jpg") || url.Contains(".png")))
        {
            DoIfSuccess(Resources.Load<Texture2D>("no-image"));
        }
        else
        {
            int i = 0;
            bool find = false;
            foreach (string s in img_cache_string)
            {
                if (s == url) { find = true; break; }
                i++;
            }
            if (find)
            {
                DoIfSuccess(img_cache_tex[i]);
            }
            else
            {
                var req = await IRequestSend(() =>
                {
                    UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
                    return request;
                });
               // Debug.Log("Loading image");
                var tex = DownloadHandlerTexture.GetContent(req);
                tex.Apply();
                img_cache_string.Add(url);
                img_cache_tex.Add(tex);
                DoIfSuccess(tex);
            }
        }
    }

    public async Task<UnityWebRequest> IRequestSend(RequestCall data)
    {
        UnityWebRequest request;//= data();

        request = data();
        //   loadingScreen.SetActive(true);
        await request.SendWebRequest();
        //    loadingScreen.SetActive(false);
        //Debug.Log(request.isNetworkError);
        //if (request.responseCode == 403)
        //{
        //    SceneManager.LoadScene(0);
        //}
        if (request.error != null)
        {
            print(request.error);
            print(request.downloadHandler.text);
            // noInternet.SetActive(true);
            await new WaitWhile(() => { return noInternet.activeSelf == true; });
        }
        Debug.Log(request.downloadHandler.text);
        return request;
    }
    #endregion

    public async void UpdateUserWrapper(UnityAction<string> afterFinish, string data)
    {
        await PostJson(updateUserEndpoint, data, afterFinish, true);
    }

    public async void PostSendFileWrapper(UnityAction<string> afterFininish, string data, byte[] file = null)
    {
        await PostMultipartAsync(sendfileEndpoint, data, file, afterFininish);
    }

    public async Task LoadImageWrapper(UnityAction<Texture2D> afterFinish, string imgUrl)
    {
        await LoadImage(imgUrl, afterFinish);
    }


    public void RegisterWraper(UnityAction<string> afterFinish)
    {
        var userJSON = JsonUtility.ToJson(AppManager.Instance.userInfo.user);
        PostJson(registerEndpoint, userJSON, afterFinish);
    }



    public void LoginWraper(UnityAction<string> afterFinish)
    {
        var userJSON = JsonUtility.ToJson(AppManager.Instance.userInfo.user);
        PostJson(loginEndpoint, userJSON, afterFinish);
    }


    public void UploadData(UnityAction<string> afterFinish)
    {
        // var userJSON = JsonUtility.ToJson(AppManager.Instance.places);
        //PostJson(loginEndpoint, userJSON, afterFinish);
    }

    //public void BuyPointsWraper(int pointsToBuy, UnityAction<string> afterFinish)
    //{
    //    var points = new Points(pointsToBuy);
    //    PostJson(buyPointsEndpoint, JsonUtility.ToJson(points), afterFinish, true);
    //}

    //public void BuyWaterWraper(int waterToBuy, int vodomatID, UnityAction<string> afterFinish)
    //{
    //    var water = new Water(waterToBuy, vodomatID);
    //    PostJson(buyWaterEndpoint, JsonUtility.ToJson(water), afterFinish, true);
    //}


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public async Task GetLoadCargoWraper(UnityAction<string> afterfinish)
    {
        await GetRequest(null, loadCargoEndpoint, afterfinish);
    }


    public async Task SendINNWrapper(UnityAction<string> afterfinish)
    {
        await GetRequest(innEndpoint, AppManager.Instance.inn);
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    private void SingletonImplementation()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
            Destroy(this);
    }
}
