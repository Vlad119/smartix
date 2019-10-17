using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class INNPrefScript : MonoBehaviour
{
    public TMP_Text value;

    public void Edit(int index)
    {
        value.text = AppManager.Instance.res.perms[index].inn;
    }
}
