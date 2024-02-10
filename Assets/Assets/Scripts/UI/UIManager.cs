﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("UIManager null");
            }
            return _instance;
        }
    }
    public Text gemCountText;

    public void OpenShop(int gemCount)
    {
        gemCountText.text = gemCount.ToString(); 
    }
    private void Awake()
    {
        _instance = this;
    }
}
