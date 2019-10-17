using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BottomBarScript : MonoBehaviour
{
    public GameObject search;
    public GameObject calculate;
    public GameObject delivery;
    public GameObject user;

    public TMP_Text searchT;
    public TMP_Text calculateT;
    public TMP_Text deliveryT;
    public TMP_Text userT;

    public void Search()
    {
        Default();
        searchT.color = new Color32(255, 188, 0, 255);
        search.GetComponent<Image>().color = new Color32(255, 188, 0, 255);
        AppManager.Instance.SwitchScreen(2);
    }

    public void Calculate()
    {
        Default();
        calculateT.color = new Color32(255, 188, 0, 255);
        calculate.GetComponent<Image>().color = new Color32(255, 188, 0, 255);
        AppManager.Instance.SwitchScreen(3);
    }

    public void Delivery()
    {
        Default();
        deliveryT.color = new Color32(255, 188, 0, 255);
        delivery.GetComponent<Image>().color = new Color32(255, 188, 0, 255);
        AppManager.Instance.SwitchScreen(4);
    }

    public void User()
    {
        Default();
        userT.color = new Color32(255, 188, 0, 255);
        user.GetComponent<Image>().color = new Color32(255, 188, 0, 255);
        AppManager.Instance.SwitchScreen(5);
    }

    public void Default()
    {
        searchT.color = new Color32(218, 218, 218, 255);
        search.GetComponent<Image>().color = new Color32(218, 218, 218, 255);

        calculateT.color = new Color32(218, 218, 218, 255);
        calculate.GetComponent<Image>().color = new Color32(218, 218, 218, 255);

        deliveryT.color = new Color32(218, 218, 218, 255);
        delivery.GetComponent<Image>().color = new Color32(218, 218, 218, 255);

        userT.color = new Color32(218, 218, 218, 255);
        user.GetComponent<Image>().color = new Color32(218, 218, 218, 255);
    }
}
