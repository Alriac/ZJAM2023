using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    public float MaxAngryness;

    void Start()
    {
        GameEvents.Ins.OnScoreChanged += OnScoreChanged;
    }

    void OnScoreChanged(EnumScoreType stype, float newScore)
    {
        if (stype == EnumScoreType.GrannyAnger || stype == EnumScoreType.MoneyDesperatePersonAngryness)
            if (newScore >= MaxAngryness)
            {
                SetMusicToLastPlaytime.SetTime(GetComponent<AudioSource>().time);
                SceneManager.LoadScene("Muerte");
            }
    }

}
