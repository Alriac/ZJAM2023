using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
