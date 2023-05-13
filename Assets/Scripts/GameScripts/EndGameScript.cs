using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    public float MaxAngryness;
    float timeStarted = 0.0f;

    void Start()
    {
        timeStarted = Time.time;
        GameEvents.Ins.OnScoreChanged += OnScoreChanged;
        GameEvents.Ins.OnGameEnded = OnGameEnded;
    }

    void OnScoreChanged(EnumScoreType stype, float newScore)
    {
        if (stype == EnumScoreType.GrannyAnger || stype == EnumScoreType.MoneyDesperatePersonAngryness)
            if (newScore >= MaxAngryness)
            {
                SetMusicToLastPlaytime.SetTime(GetComponent<AudioSource>().time);
                if (GameEvents.Ins.OnGameEnded != null) GameEvents.Ins.OnGameEnded(stype == EnumScoreType.GrannyAnger ? EnumGameEndingReason.GrandmaAngry : EnumGameEndingReason.LandlordAngry);
                SceneManager.LoadScene("Muerte");
            }
    }

    void OnGameEnded(EnumGameEndingReason reason)
    {
        PlayerPrefs.SetFloat("total time played", Time.time - timeStarted);
        PlayerPrefs.SetInt("game ended reason", (int)reason);
    }

}
