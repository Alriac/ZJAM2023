using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mueve a la abuela por la sala según su estado de enfado y sus peticiones.
/// </summary>
public class GrandmaMovement : MonoBehaviour
{
    public Vector2 MinCoords;
    public Vector2 MaxCoords;

    Vector2 targetPos;
    float currentSpeed = 0; // Cambia según el nivel de enfado.
    float currentAnger = 0;

    Coroutine co;

    private void Awake()
    {
        targetPos = transform.position;
    }

    void Start()
    {
        GameEvents.Ins.OnScoreChanged += OnScoreChanged;
    }

    void Update()
    {

    }
    private void OnDestroy()
    {
        GameEvents.Ins.OnScoreChanged -= OnScoreChanged;
    }

    void OnScoreChanged(EnumScoreType sType, float newValue)
    {
        if (sType != EnumScoreType.GrannyAnger) return;
        currentAnger = newValue;
    }
}
