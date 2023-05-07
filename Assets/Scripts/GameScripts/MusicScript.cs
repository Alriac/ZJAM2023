using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{

    int LastAudioIndex = 0;
    int CurrentAudioIndex = 0;
    public AudioClipSettings[] AudioClips;
    AudioSource source;

    public float MinIntervalMusicChange;
    float lastTimeMusicChange = 0;

    float grandmaScore = 0;
    float landlorScore = 0;
    // Cantidad de peligro, de 0 a 100.
    float dangerLevel { get { return (grandmaScore + landlorScore) / 2f; } }

    [System.Serializable]
    public class AudioClipSettings
    {
        public AudioClip clip;
        public float fromDangerLevel;
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        GameEvents.Ins.OnObjectSwitched += OnObjectSwitched;
        GameEvents.Ins.OnScoreChanged += OnScoreChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastTimeMusicChange + MinIntervalMusicChange < Time.time)
        {
            LastAudioIndex = CurrentAudioIndex;
            CurrentAudioIndex = GetNewAudioIndex();
            if (LastAudioIndex != CurrentAudioIndex)
            {
                StartCoroutine(ManageAudioClipChange());
            }
        }
    }
    private void OnDestroy()
    {
        GameEvents.Ins.OnObjectSwitched -= OnObjectSwitched;
    }

    IEnumerator ManageAudioClipChange()
    {
        float secondsTillAudioEnd = AudioClips[LastAudioIndex].clip.length - source.time;
        lastTimeMusicChange = Time.time + secondsTillAudioEnd;

        yield return new WaitForSeconds(secondsTillAudioEnd);

        source.clip = AudioClips[CurrentAudioIndex].clip;
    }

    int GetNewAudioIndex()
    {
        for (int i = 0; i < AudioClips.Length; i++)
        {
            if (dangerLevel < AudioClips[i].fromDangerLevel)
                return i;
        }
        return 0;
    }

    void OnScoreChanged(EnumScoreType scoreType, float newAmount)
    {

    }

    void OnObjectSwitched(bool status, EnumObjectTypes objectType, int times)
    {
        if (objectType == EnumObjectTypes.Jukebox)
        {
            source.volume = status ? 1.25f : 1f;
        }
    }
}