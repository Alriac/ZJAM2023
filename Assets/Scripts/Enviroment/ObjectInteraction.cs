using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class ObjectInteraction : MonoBehaviour
{
    SpriteRenderer sr;
    InteractableObject interactionObject;

    private void Awake()
    {
        interactionObject = GetComponent<InteractableObject>();
        sr = GetComponent<SpriteRenderer>();

    }
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.Ins.OnPlayerActionKey += PlayerInteracting;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PlayerInteracting()
    {
        if (interactionObject.HighlightStatus)
            interactionObject.Switch();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        TriggerHappened(true);
        //sr.color = Color.blue;
        //Debug.Log("hola");        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TriggerHappened(false);
        //sr.color = Color.white;
        //Console.Log("hola");
    }

    void TriggerHappened(bool IsEnter)
    {
        interactionObject.HighlightObject(IsEnter);
    }

    private void OnDestroy()
    {
        GameEvents.Ins.OnPlayerActionKey -= PlayerInteracting;
    }
}
