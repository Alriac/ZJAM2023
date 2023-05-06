using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Objeto interaccionable de la casa: Horno, Luz, Ventana...
/// </summary>
[DisallowMultipleComponent]
public class InteractableObject : MonoBehaviour
{
    #region Inspector variables
    public bool SwitchStatus { get; private set; } // On/Off
    public bool HighlightStatus { get; private set; } // Highligted?
    [SerializeField]
    public EnumObjectTypes ObjectType;
    [SerializeField]
    double Cooldown;
    [SerializeField]
    EventFromObject[] EventsLaunched; // Lista de eventos que lanza, rellenar desde inspector.

    #endregion Inspector variables

    #region Internal variables

    [HideInInspector]
    public float TimeOfSwitch;
    [HideInInspector]
    public bool IsBroken; // Algunos objetos ya no pueden usarse mas.

    #endregion Internal variables

    #region Events

    public Action<bool> OnHighlighted;

    #endregion Events

    /// <summary>
    /// Cambia el estado on/off o abierto/cerrado del objeto.
    /// </summary>
    /// <returns>El nuevo estado de on/off o abierto/cerrado.</returns>
    public bool Switch()
    {
        SwitchStatus = !SwitchStatus;
        for (int i = 0; i < EventsLaunched.Length; i++)
        {
            // Resetea los tiempos de lanzamiento de los eventos.
            EventsLaunched[i].NextTimeLaunched = Time.time + EventsLaunched[i].LaunchAfterTime;
            EventsLaunched[i].WasLaunched = false;
            if (EventsLaunched[i].OnSwitchStatus == this.SwitchStatus)
            {
                if (GameEvents.Ins.OnEventProgrammed != null) GameEvents.Ins.OnEventProgrammed(this.ObjectType, EventsLaunched[i].EventType, (float)EventsLaunched[i].LaunchAfterTime);
                // Debug.Log($"Corriendo evento {EventsLaunched[i].EventType} de {this.ObjectType}, en {(EventsLaunched[i].NextTimeLaunched - Time.time)} segundos");
            }
            else
            {
                if (GameEvents.Ins.OnEventCancelled != null) GameEvents.Ins.OnEventCancelled(this.ObjectType, EventsLaunched[i].EventType);
            }
        }
        if (GameEvents.Ins.OnEventHappened != null) GameEvents.Ins.OnEventHappened(SwitchStatus ? EnumEventTypes.ObjectSwitchedOn : EnumEventTypes.ObjectSwitchedOff, this.ObjectType);
        if (GameEvents.Ins.OnObjectSwitched != null) GameEvents.Ins.OnObjectSwitched(SwitchStatus, ObjectType, 1);
        Debug.Log($"Objeto: {gameObject.name}, Evento: {(SwitchStatus ? EnumEventTypes.ObjectSwitchedOn : EnumEventTypes.ObjectSwitchedOff)}");
        return SwitchStatus;
    }

    /// <summary>
    /// Resalta el objeto o lo mantiene resaltado si ya lo está.
    /// </summary>
    public void HighlightObject(bool isHighlighted)
    {
        HighlightStatus = isHighlighted;
        if (OnHighlighted != null) OnHighlighted(isHighlighted);
    }

    [System.Serializable]
    public class EventFromObject
    {
        public bool OnSwitchStatus; // Determina si el evento se procesa con el objeto encendido o con el objeto apagado.
        public EnumEventTypes EventType; // Tipo de evento a lanzar.
        public double LaunchAfterTime; // Solo se lanza si esta cantidad de tiempo ha pasado desde el ultimo on/off. 
        public double NextTimeLaunched; // Tiempo de próximo lanzamiento.
        public bool WasLaunched; // Indica si ya se lanzó.
        public bool IsLoopWhileSwitched; // Idea: Se repite mientras se mantenga el estado on/off
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeOfSwitch = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventsLaunched != null)
        {
            float TimeOnStatus = TimeOfSwitch - Time.time;

            for (int i = 0; i < EventsLaunched.Length; i++)
            {
                EventFromObject ev = EventsLaunched[i];
                if (
                    ev.OnSwitchStatus == SwitchStatus &&
                    !ev.WasLaunched && // || ev.IsLoopWhileSwitched) &&
                    ev.NextTimeLaunched <= Time.time
                    )
                {
                    ev.WasLaunched = true;
                    ev.NextTimeLaunched = Time.time + ev.LaunchAfterTime;
                    GameEvents.Ins.OnEventHappened(ev.EventType, this.ObjectType);

                    Debug.Log($"Evento {ev.EventType} del objeto {gameObject.name}");
                }
            }
        }
    }
}
