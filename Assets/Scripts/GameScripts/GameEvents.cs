using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    /// Eventos generales sin datos asociados mas que el tipo de evento y objeto que lo ha generado.
    /// </summary>
    public Action<EnumEventTypes, EnumObjectTypes> OnEventHappened;

    public Action<EnumObjectTypes, EnumEventTypes, float> OnEventProgrammed;
    public Action<EnumObjectTypes, EnumEventTypes> OnEventCancelled;

    // Actualiza la cifra actual del score seleccionado.
    public Action<EnumScoreType, float> OnScoreChanged;

    public Action OnPlayerActionKey;

    public Action<string, string, FontStyles> OnNewTextForMainDialog;
}
