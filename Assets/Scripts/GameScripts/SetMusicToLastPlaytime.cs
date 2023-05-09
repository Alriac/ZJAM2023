using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Solo uno por escena!
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SetMusicToLastPlaytime : MonoBehaviour
{
    static float MusicTime;

    private void OnLevelWasLoaded(int level)
    {
        GetComponent<AudioSource>().time = MusicTime;
    }

    public static void SetTime(float time)
    {
        MusicTime = time;
    }
    public static float GetTime()
    {
        return MusicTime;
    }

}
