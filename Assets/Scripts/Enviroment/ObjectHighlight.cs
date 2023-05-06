using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class ObjectHighlight : MonoBehaviour
{

    SpriteRenderer HighlightSprite;
    
    InteractableObject IntObject;
    private void Awake()
    {
        HighlightSprite = GetComponent<SpriteRenderer>();
        IntObject = GetComponentInParent<InteractableObject>();
        HighlightSprite.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        IntObject.OnHighlighted += OnHighlight;
    }

    void OnHighlight(bool highlighted)
    {
        // Resalta el sprite.
        if (HighlightSprite.enabled != highlighted)
            HighlightSprite.enabled = highlighted;
    }

    private void OnDestroy()
    {
        IntObject.OnHighlighted -= OnHighlight;
    }
}
