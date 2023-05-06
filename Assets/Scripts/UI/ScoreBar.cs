using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    RectTransform ScoreBarUI;

    public EnumScoreType ScoreType;
    public float MaxScore;
    public float CurrentScore;
    // public float TargetScoreScore; // TODO: Animation

    private void Awake()
    {
        ScoreBarUI = GetComponent<RectTransform>();
    }

    void Start()
    {
        GameEvents.Ins.OnScoreChanged += ScoreChanged;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ScoreChanged(EnumScoreType scoreType, float newAmount)
    {
        if (this.ScoreType == scoreType)
        {
            CurrentScore = newAmount;
            ScoreBarUI.localScale = new Vector3(CurrentScore / MaxScore, 1, 1);
        }
    }
}
