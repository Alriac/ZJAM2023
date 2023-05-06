using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDesperatePerson : MonoBehaviour
{
    public float AngrynessLevel = 0; // Max is always 100
    public AngrynessForEnergy[] AngrynessPerObject;


    [System.Serializable]
    public struct AngrynessForEnergy
    {
        public EnumObjectTypes ObjectType;
        public bool IsOn;
        public float AngrynessGeneratedPerSecond;
        // Idea: Multiplicador por tiempo transcurrido.
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.Ins.OnObjectSwitched += OnObjectSwitched;
    }

    /// <summary>
    /// Recibe qué objeto ha sido encendido o apagado.
    /// </summary>
    private void OnObjectSwitched(bool status, EnumObjectTypes objectType, int number)
    {
        for (int i = 0; i < AngrynessPerObject.Length; i++)
        {
            if (AngrynessPerObject[i].ObjectType == objectType)
            {
                AngrynessPerObject[i].IsOn = status;
            }

        }
    }
    void Update()
    {
        for (int i = 0; i < AngrynessPerObject.Length; i++)
        {
            if (AngrynessPerObject[i].IsOn)
            {
                AngrynessLevel += AngrynessPerObject[i].AngrynessGeneratedPerSecond * Time.deltaTime;
            }
        }
        if (GameEvents.Ins.OnScoreChanged != null)
            GameEvents.Ins.OnScoreChanged(EnumScoreType.MoneyDesperatePersonAngryness, this.AngrynessLevel);
    }
}
