using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CargoPrefScript : MonoBehaviour
{
    public TMP_Text number;
    public TMP_Text inn;
    public TMP_Text place;
    public TMP_Text targetDate;
    public Text from;
    public TMP_Text to;
    public TMP_Text count;
    public TMP_Text mass;
    public TMP_Text v;
    public TMP_Text getDate;
    public TMP_Text sortCenter;
    public TMP_Text fly;
    public TMP_Text svhDate;
    public TMP_Text sumbissionToCustoms;
    public TMP_Text numDT;
    public TMP_Text releaseFromCustoms;
    public TMP_Text svhOut;
    public TMP_Text delivery;
    public int index;
    public GameObject done;
    public GameObject inProgress;
    public GameObject main;
    public GameObject about;
    public GameObject wayUp;
    public GameObject wayDown;

    public GameObject getdate;
    public GameObject sortcenter;
    public GameObject svhdate;
    public GameObject flyy;
    public GameObject subtocustoms;
    public GameObject fromcustoms;
    public GameObject svhout;
    public GameObject deliveryy;
    public GameObject donee;



    private void ScrollCellIndex(int _index)
    {
        index = _index;
        var R = AppManager.Instance.res.lines;
        number.text = R[_index].number;
        inn.text = R[_index].inn;
        place.text = R[_index].place;
        targetDate.text = R[_index].targetDate;
        from.text = R[_index].from;
        to.text = R[_index].to;
        count.text = R[_index].count;
        mass.text = R[_index].mass;
        v.text = R[_index].v;
        getDate.text = R[_index].getDate;
        sortCenter.text = R[_index].sortCenter;
        fly.text = R[_index].fly;
        svhDate.text = R[_index].svhDate;
        sumbissionToCustoms.text = R[_index].sumbissionToCustoms;
        numDT.text = R[_index].numDT;
        releaseFromCustoms.text = R[_index].releaseFromCustoms;
        svhOut.text = R[_index].svhOut;
        delivery.text = R[_index].delivery;
       // Check();
        InitOpenClose(R[_index].opened);
    }

    //public void Check()
    //{
    //    if (getDate.text == "")
    //    { getdate.SetActive(false); }

    //    if (sortCenter.text == "")
    //    { sortcenter.SetActive(false); }

    //    if (svhDate.text == "")
    //    { svhdate.SetActive(false); }

    //    if (fly.text == "")
    //    { flyy.SetActive(false); }

    //    if (sumbissionToCustoms.text == "")
    //    { subtocustoms.SetActive(false); }

    //    if (releaseFromCustoms.text == "")
    //    { fromcustoms.SetActive(false); }

    //    if (svhOut.text == "")
    //    { svhout.SetActive(false); }

    //    if (svhOut.text == "")
    //    { svhout.SetActive(false); }

    //    if (delivery.text == "")
    //    { deliveryy.SetActive(false); }

    //}


    private void InitOpenClose(bool _opened)
    {
        if (_opened)
        {
            main.SetActive(true);
            about.SetActive(true);
            wayDown.SetActive(false);
            wayUp.SetActive(true);
        }
        else
        {
            main.SetActive(false);
            about.SetActive(false);
            wayDown.SetActive(true);
            wayUp.SetActive(false);
        }
    }

    public void OpenCloseButton()
    {
        var R = AppManager.Instance.res.lines;
        OpenClose(R[index].opened);
    }

    public void OpenClose(bool _opened)
    {
        var R = AppManager.Instance.res.lines;
        if (_opened)
        {
            main.SetActive(false);
            about.SetActive(false);
            R[index].opened = false;
            wayDown.SetActive(true);
            wayUp.SetActive(false);
        }
        else
        {
            main.SetActive(true);
            about.SetActive(true);
            R[index].opened = true;
            wayDown.SetActive(false);
            wayUp.SetActive(true);
        }
    }
}
