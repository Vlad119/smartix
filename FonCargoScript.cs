using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FonCargoScript : MonoBehaviour
{
    public LoopScrollRect scroll;
    public GameObject bottom;

    public void OnEnable()
    {
       // PlayerPrefs.DeleteAll();
        LoadCargo();
    }

    async public void LoadCargo()
    {
        await WebHandler.Instance.GetLoadCargoWraper((repl) =>
        {
            JsonUtility.FromJsonOverwrite(repl, AppManager.Instance);
            Debug.Log("load cargo");
        }       
        ); 

        ViewCargoPref();
    }

    public void ViewCargoPref()
    {
        try
        {
            var AM = AppManager.Instance;
            var R = AppManager.Instance.res.lines;
                if (R.Count > 0)
                {
                    scroll.totalCount = R.Count;
                    scroll.RefillCells();
                }
        }
        catch { }
    }

    private void Start()
    {
        bottom.SetActive(true);
    }
}
