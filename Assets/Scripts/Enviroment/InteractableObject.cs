using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Objeto interaccionable de la casa: Horno, Luz, Ventana...
/// </summary>
public class InteractableObject : MonoBehaviour
{

    public bool SwitchStatus { get; private set; } // On/Off
    [HideInInspector]
    public double TimeOfSwitch;
    public double Cooldown;
    public EventFromObject[] EventsLaunched; // Lista de eventos que lanza, rellenar desde inspector.
    
    [HideInInspector]
    public bool IsBroken; // Algunos objetos ya no pueden usarse mas.
    public EnumObjectTypes ObjectType;

    /// <summary>
    /// Cambia el estado on/off o abierto/cerrado del objeto.
    /// </summary>
    /// <returns>El nuevo estado de on/off o abierto/cerrado.</returns>
    public bool Swtich()
    {
        SwitchStatus = !SwitchStatus;
        for (int i = 0; i < EventsLaunched.Length; i++)
        {
            // Resetea los tiempos de lanzamiento de los eventos.
            EventsLaunched[i].NextTimeLaunched = Time.time + EventsLaunched[i].LaunchAfterTime;
            EventsLaunched[i].WasLaunched = false;
        }
        GameEvents.Ins.OnEventHappened(SwitchStatus ? EnumEventTypes.ObjectSwitchedOn : EnumEventTypes.ObjectSwitchedOff);
        GameEvents.Ins.OnObjectSwitched(SwitchStatus, ObjectType, 1);
        Debug.Log($"Objeto: {gameObject.name}, Evento: {(SwitchStatus ? EnumEventTypes.ObjectSwitchedOn : EnumEventTypes.ObjectSwitchedOff)}");
        return SwitchStatus;
    }

    [System.Serializable]
    public struct EventFromObject
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
            double TimeOnStatus = TimeOfSwitch - Time.time;

            for (int i = 0; i < EventsLaunched.Length; i++)
            {
                EventFromObject ev = EventsLaunched[i];
                if (
                    ev.OnSwitchStatus == SwitchStatus &&
                    (!ev.WasLaunched && ev.IsLoopWhileSwitched) &&
                    ev.NextTimeLaunched <= TimeOnStatus
                    )
                {
                    ev.WasLaunched = true;
                    ev.NextTimeLaunched = Time.time + ev.LaunchAfterTime;
                    GameEvents.Ins.OnEventHappened(ev.EventType);

                    Debug.Log($"Objeto: {gameObject.name}, Evento: {ev.EventType}");
                }
            }
        }
    }
}
