using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameEvents : MonoBehaviour
{
    public static GameEvents Ins;

    private void Awake()
    {
        Ins = this;
    }

    // Start is called before the first frame update

    /// <summary>
    /// Estado on/off del objeto, tipo de objeto, veces que se ha lanzado,
    /// </summary>
    public Action<bool, EnumObjectTypes, int> OnObjectSwitched;
    /// <summary>
    /// Eventos generales sin datos asociados mas que el tipo de evento.
    /// </summary>
    public Action<EnumEventTypes> OnEventHappened;

    // Actualiza la cifra actual del score seleccionado.
    public Action<EnumScoreType, float> OnScoreChanged;

    public Action OnPlayerActionKey;
}
