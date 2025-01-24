using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaTorreta : MonoBehaviour
{
    public float CurrentHealth;
    public float MaxHealth;
    public float MinHealth;

    public int currentLevel = 1;
    public int currentExperience = 0;
    public int experienceToNextLevel = 100;

    public delegate void LevelUpAction(int newLevel);
    public static event LevelUpAction OnLevelUp;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount;
        CheckLevelUp();
    }

    void CheckLevelUp()
    {
        while (currentExperience >= experienceToNextLevel)
        {
            currentExperience -= experienceToNextLevel;
            currentLevel++;
            experienceToNextLevel += Mathf.RoundToInt(experienceToNextLevel * 0.25f); // Incremento progresivo
            OnLevelUp?.Invoke(currentLevel); // Notificar nivel nuevo
        }
    }
}
