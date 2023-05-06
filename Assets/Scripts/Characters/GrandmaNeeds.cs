using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaNeeds : MonoBehaviour
{

    public float AngrynessTotal { get { return Angryness; } }
    float Angryness = 0.0f;
    float MaxAngryness = 100.0f;
    float AngrynessSpeed = 1.0f;

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
        GameEvents.Ins.OnEventHappened += OnEventHappened;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeLastRequest + MinTimeBetweenReminders < Time.time)
        {
            if (!TryAddRequest()) RemindCurrentRequest();
            TimeLastRequest = Time.time;
        }

        AddAngrynessFromRequests();

    }

    void AddAngrynessFromRequests()
    {
        int accumulatedAttempts = 0;
        for (int i = 0; i < CurrentRequests.Count; i++)
        {
            accumulatedAttempts += CurrentRequests[i].RemindersGiven + 1;
        }
        Angryness += accumulatedAttempts * Time.deltaTime;
        if (GameEvents.Ins.OnScoreChanged != null) GameEvents.Ins.OnScoreChanged(EnumScoreType.GrannyAnger, this.AngrynessTotal);
    }

    // Intenta a�adir una nueva peticion.
    bool TryAddRequest()
    {
        if (CurrentRequests.Count >= 3) return false; // Por ahora solo permitimos uno de cada a la vez.

        // Comprueba que stattypes no han sido ya pedidos.
        List<EnumStatType> statTypes = new List<EnumStatType>(AllStatTypes);
        for (int i = 0; i < CurrentRequests.Count; i++)
        {
            statTypes.Remove(CurrentRequests[i].StatType);
        }

        // A�ade aleatoriamente un statype que no haya pedido ya.
        if (statTypes.Count > 0)
        {
            // Elige el tipo de stat que queire satisfacer.
            EnumStatType newStatType = statTypes[UnityEngine.Random.Range(0, statTypes.Count)];

            // Elige el objeto que necesitas usar seg�n el tipo de stat.
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

            // Crea nuevo request eligiendo aleatoriamente el objeto que lo satisfacir� (provisional).
            Request newReq = new Request(newStatType, objectTypeSelected[UnityEngine.Random.Range(0, objectTypeSelected.Length)]);
            CurrentRequests.Add(newReq);
            SayText($"Abuelita: Quiero que uses {newReq.ObjectType.ToString()}, apresurate");
            return true;
        }
        return false;
    }

    // Te recuerda una petici�n ya realizada, elevando el nivel de impaciencia.
    void RemindCurrentRequest()
    {
        if (CurrentRequests.Count > 0)
        {
            Request toRemind = CurrentRequests[UnityEngine.Random.Range(0, CurrentRequests.Count)];
            toRemind.RemindersGiven++;
            GetComponent<Grandma>().GenerateBubble(toRemind.ObjectType);
            SayText($"Abuelita: Recuerda usar el {toRemind.ObjectType.ToString()}, ya te lo he dicho {toRemind.RemindersGiven} veces");
        }
    }

    void SayText(string text = default(string))
    {
        // TODO: Show text in the bubble.
        Debug.Log(text);
    }


    #region Recepci�n de Eventos

    /*void ObjectSwitched(bool status, EnumObjectTypes objectType, int times)
    {

    }*/

    void OnEventHappened(EnumEventTypes eventType, EnumObjectTypes objectType)
    {
        if (eventType != EnumEventTypes.ObjectReady) return;

        for (int i = 0; i < CurrentRequests.Count; i++)
        {
            if (CurrentRequests[i].ObjectType == objectType)
            {
                // Reducir enfado por objetivo completado.
                Request req = CurrentRequests[i];

                // Placeholder: Reduce un porcentaje fijo de enfado.
                this.Angryness = this.Angryness * 0.85f;
                if (GameEvents.Ins.OnScoreChanged != null) GameEvents.Ins.OnScoreChanged(EnumScoreType.GrannyAnger, this.AngrynessTotal);

                CurrentRequests.RemoveAt(i);
                break;
            }
        }
    }

    private void OnDestroy()
    {
        GameEvents.Ins.OnEventHappened -= OnEventHappened;
    }

    #endregion Recepci�n de Eventos
}
