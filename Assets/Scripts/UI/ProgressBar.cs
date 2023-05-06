using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    InteractableObject IntObject;
    public Transform ResizableObject;

    float totalTime;
    float currentTime;

    private void Awake()
    {
        IntObject = GetComponentInParent<InteractableObject>();
    }

    void Start()
    {
        GameEvents.Ins.OnEventProgrammed += OnEventProgrammed;
        GameEvents.Ins.OnEventCancelled += OnEventCancelled;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > totalTime)
            this.gameObject.SetActive(false);
        UpdateProgressBar();
    }
    void UpdateProgressBar()
    {
        ResizableObject.localScale = new Vector3(currentTime / totalTime, 1, 1);
    }

    void OnEventProgrammed(EnumObjectTypes objectType, EnumEventTypes eventType, float timeToComplete)
    {
        if (objectType == IntObject.ObjectType && eventType == EnumEventTypes.ObjectReady)
        {
            totalTime = timeToComplete;
            currentTime = 0;
            this.gameObject.SetActive(true);
            UpdateProgressBar();
        }
    }
    void OnEventCancelled(EnumObjectTypes objectType, EnumEventTypes eventType)
    {
        if (objectType == IntObject.ObjectType && eventType == EnumEventTypes.ObjectReady)
        {
            this.gameObject.SetActive(false);
            UpdateProgressBar();
        }
    }


}
