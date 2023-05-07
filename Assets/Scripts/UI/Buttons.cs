using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public Button Play;
    public Button Exit;

    // Start is called before the first frame update
    void Start()
    {
		Play.onClick.AddListener(LetsPlay);
        Exit.onClick.AddListener(GetOut);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

	void LetsPlay(){
        SceneManager.LoadScene("ZJAM23");
	}

    void GetOut() {
        Application.Quit();
    }

}
