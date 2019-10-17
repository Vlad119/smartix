using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoSavedScript : MonoBehaviour
{
    public float timeRemaining = 2f;
    public GameObject success;
    public void OnEnable()
    {
        timeRemaining = 2f;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
                success.SetActive(false);
        }
    }
}