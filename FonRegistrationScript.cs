using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FonRegistrationScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private TMP_Text output;
    public TMP_InputField surname;
    public TMP_InputField name;
    string s;
    string phone;
    int count;

    public void SendPass()
    {
        var user = AppManager.Instance.userInfo.user;
        user.surname = surname.text;
        user.name = name.text;
        if (input.text.Length > 10)
        {
            count = input.text.Length - 10;
        }
        s = input.text.Remove(10, count);
        phone = "8" + s;
        Debug.Log(phone);
        user.phone = phone;
        PlayerPrefs.SetString("phoneNumber", user.phone.ToString());
        WebHandler.Instance.RegisterWraper((repl) =>
        {
            AppManager.Instance.SwitchScreen(1);
        }
        );
    }

}