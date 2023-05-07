using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSprireFromScores : MonoBehaviour
{
    public EnumScoreType ScoreType;
    SpriteRenderer srenderer;
    Image img;

    public Sprite[] Sprites;
    public float[] Scores;

    int currentIndex = 0;

    private void Awake()
    {
        srenderer = GetComponent<SpriteRenderer>();
        img = GetComponent<Image>();
    }

    void Start()
    {
        if (srenderer != null) srenderer.sprite = Sprites[currentIndex];
        if (img != null) img.sprite = Sprites[currentIndex];
        GameEvents.Ins.OnScoreChanged += ScoreChanged;
    }
    private void OnDestroy()
    {
        GameEvents.Ins.OnScoreChanged -= ScoreChanged;
    }

    private void ScoreChanged(EnumScoreType scoreType, float newAmount)
    {
        if (this.ScoreType == scoreType)
        {
            for (int i = 0; i < Scores.Length; i++)
            {
                if (newAmount < Scores[i])
                {
                    if (currentIndex != i)
                    {
                        currentIndex = i;
                        if (srenderer != null) srenderer.sprite = Sprites[currentIndex];
                        if (img != null) img.sprite = Sprites[currentIndex];
                    }
                    break;
                }
            }

        }
    }
}
