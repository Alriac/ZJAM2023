using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TotalTime : MonoBehaviour
{

    public TMP_Text text_component;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float numOfSecs = PlayerPrefs.GetFloat("total time played");
        text_component.SetText("{0} m : {1} s", TimeSpan.FromSeconds(numOfSecs).Minutes, TimeSpan.FromSeconds(numOfSecs).Seconds);
    }
}
