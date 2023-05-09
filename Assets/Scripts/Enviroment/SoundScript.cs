using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundScript : MonoBehaviour
{
    AudioSource source;

    public EnumEventTypes ForEventType;
    //public EnumStatType ForStatType;
    public EnumObjectTypes ForObjectType;
    public bool IgnoreObjectType;
    public bool IgnoreStatType;

   

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        switch (ForEventType)
        {
            case EnumEventTypes.ObjectReady:
                GameEvents.Ins.OnEventHappened += OnObjectObjectReady; break;
            case EnumEventTypes.ObjectSwitchedOff:
                GameEvents.Ins.OnObjectSwitched += OnObjectSwitchedOff; break;
            case EnumEventTypes.ObjectSwitchedOn:
                GameEvents.Ins.OnObjectSwitched += OnObjectSwitchedOn; break;
            case EnumEventTypes.ObjectSwitched:
                GameEvents.Ins.OnObjectSwitched += OnObjectSwitched; break;
            case EnumEventTypes.WindowOpenedTooLong:
                GameEvents.Ins.OnEventHappened += OnWindowOpenedTooLong; break;
                // TODO: Registrar mas tipos de eventos para los sonidos que tengamos y añadirlos aqui.
        }

        if(ForEventType == EnumEventTypes.ObjectSwitchedOn)
            GameEvents.Ins.OnObjectSwitched += TurnOffSound;
    }

    void OnObjectObjectReady(EnumEventTypes etype, EnumObjectTypes oType)
    {
        if (etype != EnumEventTypes.ObjectReady) return;
        if (oType != ForObjectType && !IgnoreObjectType) return;

        source.Play();
    }

    void OnObjectSwitchedOff(bool newStatus, EnumObjectTypes oType, int times)
    {
        if (newStatus) return;
        if (oType != ForObjectType && !IgnoreObjectType) return;

        source.Play();
    }
    void OnObjectSwitchedOn(bool newStatus, EnumObjectTypes oType, int times)
    {
        if (!newStatus) return;
        if (oType != ForObjectType && !IgnoreObjectType) return;

        source.Play();
    }

    void TurnOffSound(bool newStatus, EnumObjectTypes oType, int times)
    {
        if (newStatus) return;
        if (oType != ForObjectType && !IgnoreObjectType) return;

        source.Stop();
    }

    void OnObjectSwitched(bool newStatus, EnumObjectTypes oType, int times)
    {
        if (oType != ForObjectType && !IgnoreObjectType) return;

        source.Play();
    }

    void OnWindowOpenedTooLong(EnumEventTypes etype, EnumObjectTypes oType)
    {
        if (etype != EnumEventTypes.WindowOpenedTooLong) return;
        if (oType != ForObjectType && !IgnoreObjectType) return;

        source.Play();
    }
}
