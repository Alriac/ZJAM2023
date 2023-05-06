using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class UpdateInteractableObjectSpriteAnimation : MonoBehaviour
{
    Animator Anim;
    InteractableObject IntObj;

    // Start is called before the first frame update
    void Awake()
    {
        Anim = GetComponent<Animator>();
        IntObj = GetComponentInParent<InteractableObject>();
    }

    private void Start()
    {
        IntObj.OnSwitched += ObjectSwitched;
    }
    private void OnDestroy()
    {
        IntObj.OnSwitched -= ObjectSwitched;
    }

    void ObjectSwitched(bool newState)
    {
        Anim.SetBool("IsOn", newState);
    }

}
