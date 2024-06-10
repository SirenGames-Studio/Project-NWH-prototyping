using Unity.VisualScripting;
using UnityEngine;

public class PlayerNeeds : MonoBehaviour
{
    [SerializeField] Need health;
    [SerializeField] Need hunger;
    [SerializeField] Need thirst;
    [SerializeField] Need sleep;


    public float noHungerHealthDecay;
    public float noThunderHealthDecay;

    private void Start() 
    {
        health.CurrentValue = health.MaxValue;
        hunger.CurrentValue = hunger.MaxValue;
        sleep.CurrentValue = sleep.MaxValue;
        thirst.CurrentValue = thirst.MaxValue;
    }
}

public class Need
{
    public float CurrentValue;
    public float MaxValue;
    public float StartValue;
    public float RegenRate;
    public float DecayRate;

    public void Add(float amount)
    {
        CurrentValue = Mathf.Min(CurrentValue + amount, MaxValue);
    }

    public void Subtract(float amount)
    {
        CurrentValue = Mathf.Max(CurrentValue - amount, 0f);
    }

    public float GetPercentage()
    {
        return CurrentValue / MaxValue;
    }
}
