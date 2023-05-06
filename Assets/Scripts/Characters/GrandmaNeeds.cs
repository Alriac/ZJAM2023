using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaNeeds : MonoBehaviour
{

    public float AngrynessTotal { get { return Angryness; } }
    float Angryness;

    public float StatHunger;
    public float StatFun;
    public float StatTemp;

    float TimeLastRequest;
    public float MinTimeBetweenRequests;
    public float MinTimeBetweenReminders;

    public EnumObjectTypes[] ObjectsForHunger;
    public EnumObjectTypes[] ObjectsForFun;
    public EnumObjectTypes[] ObjectsForTemp;

    List<Request> CurrentRequests = new List<Request>();

    EnumStatType[] AllStatTypes;
    class Request
    {
        public Request(EnumStatType StatType, EnumObjectTypes ObjectType)
        {
            this.StatType = StatType;
            this.ObjectType = ObjectType;
            TimeRequested = Time.time;
            RemindersGiven = 0;
        }
        public EnumStatType StatType;
        public EnumObjectTypes ObjectType;
        public float TimeRequested;
        public int RemindersGiven;
    }

    private void Awake()
    {
        AllStatTypes = (EnumStatType[])Enum.GetValues(typeof(EnumStatType));
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TimeLastRequest + MinTimeBetweenReminders < Time.time)
        {
            if (!TryAddRequest()) RemindCurrentRequest();
            TimeLastRequest = Time.time;
        }

        // TODO: Aumentar enfado según las peticiones abiertas actualmente.
    }

    // Intenta añadir una nueva petición.
    bool TryAddRequest()
    {
        if (CurrentRequests.Count >= 3) return false; // Por ahora solo permitimos uno de cada a la vez.

        // Comprueba que stattypes no han sido ya pedidos.
        List<EnumStatType> statTypes = new List<EnumStatType>(AllStatTypes);
        for (int i = 0; i < CurrentRequests.Count; i++)
        {
            statTypes.Remove(CurrentRequests[i].StatType);
        }

        // Añade aleatoriamente un statype que no haya pedido ya.
        if(statTypes.Count > 0)
        {
            // Elige el tipo de stat que queire satisfacer.
            EnumStatType newStatType = statTypes[UnityEngine.Random.Range(0, statTypes.Count)];

            // Elige el objeto que necesitas usar según el tipo de stat.
            EnumObjectTypes[] objectTypeSelected = { };
            switch (newStatType)
            {
                case EnumStatType.Entretainment:
                    objectTypeSelected = ObjectsForFun; break;
                case EnumStatType.Hunger:
                    objectTypeSelected = ObjectsForHunger; break;
                case EnumStatType.Temperature:
                    objectTypeSelected = ObjectsForTemp; break;
            }

            // Crea nuevo request eligiendo aleatoriamente el objeto que lo satisfacirá (provisional).
            Request newReq = new Request(newStatType, objectTypeSelected[UnityEngine.Random.Range(0, objectTypeSelected.Length)]);
            CurrentRequests.Add(newReq);
        }
        return false;
    }

    // Te recuerda una petición ya realizada, elevando el nivel de impaciencia.
    void RemindCurrentRequest()
    {
        if (CurrentRequests.Count > 0)
        {
            Request toRemind = CurrentRequests[UnityEngine.Random.Range(0, CurrentRequests.Count)];
            toRemind.RemindersGiven++;
            SayText($"Abuelita: Quiero que uses {toRemind.ObjectType.ToString()}");
        }
    }

    void SayText(string text = default(string))
    {
        // TODO: Show text in the bubble.
        Debug.Log(text);
    }
}
