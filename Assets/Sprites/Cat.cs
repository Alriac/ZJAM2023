using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Cat : MonoBehaviour
{


    public Animator a;
    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        GameEvents.Ins.OnEventHappened += OnWindowOpenedToLong;
    }


    void OnWindowOpenedToLong(EnumEventTypes etype, EnumObjectTypes otype)
    {
        if (etype == EnumEventTypes.WindowOpenedTooLong && otype == EnumObjectTypes.Window)
        {
            if (GameEvents.Ins.OnGameEnded != null) GameEvents.Ins.OnGameEnded(EnumGameEndingReason.CatIsGone);
            a.SetBool("GatoSale", true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (a.GetCurrentAnimatorStateInfo(0).IsName("GatoMuere"))
        {
            SceneManager.LoadSceneAsync("Muerte");
        }
    }
}
