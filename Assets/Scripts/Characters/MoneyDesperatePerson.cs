using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[DisallowMultipleComponent]
public class MoneyDesperatePerson : MonoBehaviour
{
    public float AngrynessLevel = 0; // Max is always 100
    public AngrynessForEnergy[] AngrynessPerObject;
    public ToctocSounds[] ToctocSoundsPerAngryness;

    AudioSource asource;
    float toctocLastTime;
    float toctocDelay;
    Animator anim;

    [System.Serializable]
    public struct AngrynessForEnergy
    {
        public EnumObjectTypes ObjectType;
        public bool IsOn;
        public float AngrynessGeneratedPerSecond;
        // Idea: Multiplicador por tiempo transcurrido.
    }
    [System.Serializable]
    public struct ToctocSounds
    {
        public AudioClip Clip;
        public float FromAngrynessLevel;
        public float MinFrequency;
        public float MaxFrequency;
        public int KnockAmount;
        // Idea: Multiplicador por tiempo transcurrido.
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        asource = GetComponent<AudioSource>();
    }
    void Start()
    {
        GameEvents.Ins.OnObjectSwitched += OnObjectSwitched;
    }

    /// <summary>
    /// Recibe que objeto ha sido encendido o apagado.
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


        if (toctocLastTime + toctocDelay < Time.time)
        {
            // Comprueba si ha de trucar.
            for (int i = ToctocSoundsPerAngryness.Length - 1; i >= 0; i--)
            {
                if (AngrynessLevel >= ToctocSoundsPerAngryness[i].FromAngrynessLevel)
                {
                    toctocDelay = Random.Range(ToctocSoundsPerAngryness[i].MinFrequency, ToctocSoundsPerAngryness[i].MaxFrequency);
                    toctocLastTime = Time.time;
                    anim.SetTrigger("knocks" + ToctocSoundsPerAngryness[i].KnockAmount);
                    asource.PlayOneShot(ToctocSoundsPerAngryness[i].Clip);
                    break;
                }
                toctocDelay = 1f; // Si no truca, espera 1 animación para comprobar de nuevo.
            }
        }
    }
}
