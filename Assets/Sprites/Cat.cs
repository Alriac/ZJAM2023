using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{


    public Animator a;
    public GameObject window;
    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        a.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (window.transform.GetChild(1).GetComponent<Animator>().GetBool("Is On")) {
            a.enabled = true;
        }
        //if (window)
    }
}
