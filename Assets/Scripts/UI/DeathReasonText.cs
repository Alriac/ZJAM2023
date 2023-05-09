using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DeathReasonText : MonoBehaviour
{

    public TMP_Text text_component;

    public string EndingGrandmaAngryText;
    public string EndingLandlordAngryText;
    public string EndingCatJumpedText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnumGameEndingReason endingReason = (EnumGameEndingReason)PlayerPrefs.GetInt("game ended reason");

        string endingText = string.Empty;
        switch (endingReason)
        {
            case EnumGameEndingReason.CatIsGone:
                endingText = EndingCatJumpedText; break;
            case EnumGameEndingReason.GrandmaAngry:
                endingText = EndingGrandmaAngryText; break;
            case EnumGameEndingReason.LandlordAngry:
                endingText = EndingLandlordAngryText; break;
        }
        text_component.SetText(endingText);
    }
}
