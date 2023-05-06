using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainTextDialog : MonoBehaviour
{
    public float TimeShown;
    public float ScaleFadeInMultiplier;
    public float fadeTime;
    float currentTimeShown;

    RectTransform rect;
    CanvasGroup canvas;
    public TextMeshProUGUI textObject;

    Coroutine currentRoutine = null;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        textObject = GetComponentInChildren<TextMeshProUGUI>();
        canvas = GetComponent<CanvasGroup>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.Ins.OnNewTextForMainDialog += OnTextUpdated;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        GameEvents.Ins.OnNewTextForMainDialog -= OnTextUpdated;
    }

    void OnTextUpdated(string author, string text, FontStyles newFontStyle)
    {

        canvas.alpha = 0;
        gameObject.SetActive(true);
        textObject.text = $"{(string.IsNullOrEmpty(author) ? "" : (author + ": "))}{text}";
        textObject.fontStyle = newFontStyle;

        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
            currentTimeShown = fadeTime;
        }
        else
        {
            currentTimeShown = 0;
        }
        currentRoutine = StartCoroutine(TextCoroutine());
    }

    IEnumerator TextCoroutine()
    {
        while (currentTimeShown < TimeShown)
        {
            float t = 1f;
            if (currentTimeShown < fadeTime)
            {
                t = currentTimeShown / fadeTime;
            }
            else if (currentTimeShown >= TimeShown - fadeTime)
            {
                t = 1f - (currentTimeShown / fadeTime);
            }

            rect.localScale = Vector3.Lerp(Vector3.one * ScaleFadeInMultiplier, Vector3.one, t);
            canvas.alpha = t;

            yield return null;
            currentTimeShown += Time.deltaTime;
        }
        currentRoutine = null;
        gameObject.SetActive(false);
    }
}
