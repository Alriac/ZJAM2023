using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Buttons : MonoBehaviour
{

    public Button Play;
    public Button Exit;
    public Button BackToMenu;
    public AudioSource AudioSourceMusica;
    public AudioClip AudioClipClick;
    AudioSource AudioSourceBotones;

    private void Awake()
    {
        AudioSourceBotones = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Play != null) Play.onClick.AddListener(LetsPlay);
        if (Exit != null) Exit.onClick.AddListener(GetOut);
        if (BackToMenu != null) BackToMenu.onClick.AddListener(GoToMenu);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LetsPlay()
    {
        if (AudioSourceMusica != null) SetMusicToLastPlaytime.SetTime(AudioSourceMusica.time);
        PlayClick();
        SceneManager.LoadSceneAsync("ZJAM23");
    }

    void GetOut()
    {
        PlayClick();
        Application.Quit();
    }

    void GoToMenu()
    {
        PlayClick();
        var a = SceneManager.LoadSceneAsync("Menu");
    }

    void PlayClick()
    {
        AudioSourceBotones.clip = AudioClipClick;
        AudioSourceBotones.Play();
    }
}
