using TMPro;
using UnityEngine;

public class FonUserScript : MonoBehaviour
{
    [HideInInspector] public static FonUserScript Instance;
    public TMP_InputField inputINN;
    public GameObject innPref;
    public Transform innParent;
    public int i = 0;
    public TMP_InputField sname;
    public TMP_InputField fname;
    public GameObject saveBTN;
    public GameObject success;
    public GameObject userInfoSaved;


    async public void SendINN()
    {
        AppManager.Instance.inn = inputINN.text;
        await WebHandler.Instance.SendINNWrapper((repl) => { });
        if(inputINN.text!="")
        {
            inputINN.text = "";
            success.SetActive(true);
        }
    }


    private void OnEnable()
    {
        var user = AppManager.Instance.userInfo.user;
        innParent.transform.ClearChildren();
        ViewPref();
        sname.text = user.surname;
        fname.text = user.name;
        saveBTN.SetActive(false);
    }


    public void ViewPref()
    {
        var perms = AppManager.Instance.res.perms;
        for (i = 0; i < perms.Count; i++)
        {
            if (perms[i].perm == "1")
            {
                var obj = Instantiate(innPref, innParent);
                obj.GetComponent<INNPrefScript>().Edit(i);
            }
        }
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
    }


    public void UpdateUser()
    {
        Debug.Log("save user");
        var uInfo = AppManager.Instance.userInfo;
        var user = AppManager.Instance.userInfo.user;
        user.surname = sname.text;
        user.name = fname.text;
        WebHandler.Instance.UpdateUserWrapper((resp) =>
        {
            JsonUtility.FromJsonOverwrite(resp,uInfo);
            AppManager.Instance.SaveUserInfo(uInfo);
            saveBTN.SetActive(false);
            userInfoSaved.SetActive(true);
        }, JsonUtility.ToJson(uInfo));
        
    }


    public void ShowSaveBTN()
    {
        saveBTN.SetActive(true);
    }
   
}


