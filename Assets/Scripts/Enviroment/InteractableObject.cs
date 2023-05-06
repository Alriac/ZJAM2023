using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Objeto interaccionable de la casa: Horno, Luz, Ventana...
/// </summary>
public class InteractableObject : MonoBehaviour
{

    public bool SwitchStatus; // On/Off
    public double TimeOfSwitch;
    public double Cooldown;
    public List<EventFromObject> EventsLaunched = new List<EventFromObject>();
    public bool IsBroken; // Algunos objetos ya no pueden usarse mas.

    public struct EventFromObject
    {
        public bool OnSwitchStatus; // Determina si el evento se procesa con el objeto encendido o con el objeto apagado.
        public EnumEventTypes EventType; // Tipo de evento a lanzar.
        public double AfterTotalTime; // Solo se lanza si esta cantidad de tiempo ha pasado desde el ultimo on/off. 
        // public bool IsLoopWhileSwitched; // Se repite mientras se mantenga el estado on/off
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // LaunchEvent(...);
    }

    void LaunchEvent(EventFromObject Event){
        // Añadir info de estado on/off y seleccionar el observer correcto.
    }
}
