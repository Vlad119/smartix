using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FonContactsScript : MonoBehaviour
{
    public TMP_Text phone;
    public TMP_Text site;
    public TMP_Text insta;
    public void OpenInstagram()
    {
        Application.OpenURL(insta.text);
    }

    public void OpenSite()
    {
        Application.OpenURL(site.text);
    }

    public void OpenPhone()
    {
        Application.OpenURL("tel://" + phone.text);
    }
}
