using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    RectTransform ScoreBarUI;

    public bool HorizontalBar;
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
            float newScale = CurrentScore / MaxScore;
            ScoreBarUI.localScale = new Vector3(HorizontalBar ? newScale : 1f, !HorizontalBar ? newScale : 1f, 1);
        }
    }
}
