using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public Button Play;
    public Button Exit;
    public Button BackToMenu;
    public AudioSource asource;

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
        SetMusicToLastPlaytime.SetTime(asource.time);
        SceneManager.LoadScene("ZJAM23");
    }

    void GetOut()
    {
        Application.Quit();
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
