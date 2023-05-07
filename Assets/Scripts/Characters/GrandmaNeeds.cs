using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public float current_cooldown;
    public float cooldown = 3.0f;
    public GameObject TextBubble;
    public GameObject GeneratedTextBubble;

    float TimeLastRequest;
    public float MinTimeBetweenRequests;
    public float MinTimeBetweenReminders;

    public EnumObjectTypes[] ObjectsForHunger;
    public EnumObjectTypes[] ObjectsForFun;
    public EnumObjectTypes[] ObjectsForTemp;

    public string[] TextsForHunger;
    public string[] TextsForHungerReminder;
    public string[] TextsForFun;
    public string[] TextsForFunReminder;
    public string[] TextsForTemp;
    public string[] TextsForTempReminder;
    public FontStyles textStyle;

    public Sprite Food;
    public Sprite Hot;
    public Sprite Cold;
    public Sprite Fun;
    public Sprite Light;

    public Sprite Fan_icon;
    public Sprite Jukebox_icon;
    public Sprite Oven_icon;
    public Sprite Tea_icon;
    public Sprite TV_icon;

    private void SetNeedSprite(EnumObjectTypes objectType)
    {
        if (objectType == EnumObjectTypes.Fan) {
            GeneratedTextBubble.GetComponent<BubbleText>().need_container.GetComponent<SpriteRenderer>().sprite = Fan_icon;
        } else if (objectType == EnumObjectTypes.Jukebox) {
            GeneratedTextBubble.GetComponent<BubbleText>().need_container.GetComponent<SpriteRenderer>().sprite = Jukebox_icon;
        } else if (objectType == EnumObjectTypes.Oven) {
            GeneratedTextBubble.GetComponent<BubbleText>().need_container.GetComponent<SpriteRenderer>().sprite = Food;
        }
        else if (objectType == EnumObjectTypes.Tv)
        {
            GeneratedTextBubble.GetComponent<BubbleText>().need_container.GetComponent<SpriteRenderer>().sprite = Fun;
        }
        else if (objectType == EnumObjectTypes.AC)
        {
            GeneratedTextBubble.GetComponent<BubbleText>().need_container.GetComponent<SpriteRenderer>().sprite = Hot;
        }
    }


    Dictionary<EnumObjectTypes, bool> ObjectStatus = new Dictionary<EnumObjectTypes, bool>();


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

        EnumObjectTypes[] allObjectTypes = (EnumObjectTypes[])Enum.GetValues(typeof(EnumObjectTypes));
        for (int i = 0; i < allObjectTypes.Length; i++)
            ObjectStatus.Add(allObjectTypes[i], false);
    }
    void Start()
    {
        GameEvents.Ins.OnObjectSwitched += OnObjectSwitched;
        GameEvents.Ins.OnEventHappened += OnEventHappened;
        current_cooldown = UnityEngine.Random.Range(0.0f, 200.0f) / 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (GeneratedTextBubble == null)
        { // Quitar cuando metamos todos los tipos.
            if (TimeLastRequest + MinTimeBetweenReminders < Time.time)
            {
                if (UnityEngine.Random.Range(0.0f, 100.0f) > 60.0f)
                {
                    if (!TryAddRequest()) RemindCurrentRequest();
                    TimeLastRequest = Time.time;
                }
            }
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

    private void SetDialogText(string newText)
    {
        if (GameEvents.Ins.OnNewTextForMainDialog != null) GameEvents.Ins.OnNewTextForMainDialog("Abuelita", newText, textStyle);
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

        // Agrega aleatoriamente un statype que no haya pedido ya.
        if (statTypes.Count > 0)
        {
            // Elige el tipo de stat que queire satisfacer.
            EnumStatType newStatType = statTypes[UnityEngine.Random.Range(0, statTypes.Count)];

            // Elige el objeto que necesitas usar segun el tipo de stat.
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

            // Crea nuevo request eligiendo aleatoriamente el objeto que lo satisfacira (provisional).
            Request newReq = new Request(newStatType, objectTypeSelected[UnityEngine.Random.Range(0, objectTypeSelected.Length)]);
            CurrentRequests.Add(newReq);
            GeneratedTextBubble = Instantiate(TextBubble, new Vector3(transform.position.x + 1.0f, transform.position.y + 0.75f, 1.0f), Quaternion.identity);
            SetNeedSprite(newReq.ObjectType);
            SetDialogText(GetTextForRequest(newReq.StatType, false));

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
            SayText($"Abuelita: Recuerda usar el {toRemind.ObjectType.ToString()}, ya te lo he dicho {toRemind.RemindersGiven} veces");
            SetDialogText(GetTextForRequest(toRemind.StatType, true));
        }
    }

    string GetTextForRequest(EnumStatType stat, bool isReminder)
    {
        string[] possibleTexts = null;
        switch (stat)
        {
            case EnumStatType.Entretainment:
                possibleTexts = isReminder ? TextsForFunReminder : TextsForFun; break;
            case EnumStatType.Hunger:
                possibleTexts = isReminder ? TextsForHungerReminder : TextsForHunger; break;
            case EnumStatType.Temperature:
                possibleTexts = isReminder ? TextsForTempReminder : TextsForTemp; break;
        }

        if (possibleTexts == null) return string.Empty;
        return possibleTexts[UnityEngine.Random.Range(0, possibleTexts.Length)];
    }


    void SayText(string text = default(string))
    {
        // TODO: Show text in the bubble.
        Debug.Log(text);
    }


    #region Recepci�n de Eventos

    void OnObjectSwitched(bool status, EnumObjectTypes objectType, int times)
    {
        // TODO: Updatear el valor de ObjectStatus
        ObjectStatus[objectType] = status;
    }

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
                Destroy(GeneratedTextBubble);
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
