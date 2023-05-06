using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Objeto interaccionable de la casa: Horno, Luz, Ventana...
/// </summary>
public class InteractableObject : MonoBehaviour
{

    public bool SwitchStatus { get; private set; } // On/Off
    public double TimeOfSwitch;
    public double Cooldown;
    public EventFromObject[] EventsLaunched; // Lista de eventos que lanza, rellenar desde inspector.
    public bool IsBroken; // Algunos objetos ya no pueden usarse mas.
    public EnumObjectTypes ObjectType;

    /// <summary>
    /// Cambia el estado on/off o abierto/cerrado del objeto.
    /// </summary>
    /// <returns>El nuevo estado de on/off o abierto/cerrado.</returns>
    public bool Swtich() {
        SwitchStatus = !SwitchStatus;
        for (int i = 0; i < EventsLaunched.Length; i++)
        {
            // Resetea los tiempos de lanzamiento de los eventos.
            EventsLaunched[i].LastTimeLaunched = Time.time + EventsLaunched[i].LaunchAfterTime;
        }
        GameEvents.Ins.OnObjectSwitched(SwitchStatus, ObjectType, 1);
        return SwitchStatus;
    }

    public struct EventFromObject
    {
        public bool OnSwitchStatus; // Determina si el evento se procesa con el objeto encendido o con el objeto apagado.
        public EnumEventTypes EventType; // Tipo de evento a lanzar.
        public double LaunchAfterTime; // Solo se lanza si esta cantidad de tiempo ha pasado desde el ultimo on/off. 
        public double LastTimeLaunched; // Cuando se lanzó por última vez.
        // public bool IsLoopWhileSwitched; // Idea: Se repite mientras se mantenga el estado on/off
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeOfSwitch = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventsLaunched == null) return;

        double TimeOnStatus = TimeOfSwitch - Time.time;
        for (int i = 0; i < EventsLaunched.Length; i++)
        {
            EventFromObject ev = EventsLaunched[i];
            if (ev.OnSwitchStatus == SwitchStatus && ev.LastTimeLaunched <= TimeOnStatus)
            {
                ev.LastTimeLaunched = Time.time;
                // TODO; Lanza el evento del tipo indicado en ev.EventType.
            }
        }
    }
}
