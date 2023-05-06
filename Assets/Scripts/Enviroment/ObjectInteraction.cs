using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        sr.color = Color.blue;
        Debug.Log("hola");        
    }

    private void OnTriggerExit2D(Collider2D other) {
        sr.color = Color.white;
        //Console.Log("hola");        
    }
}
