using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaTorreta : MonoBehaviour
{
    public float CurrentHealth;
    public float MaxHealth = 100f;
    public float MinHealth = 0f;

    public int currentLevel = 1;
    public int currentExperience = 0;
    public int experienceToNextLevel = 100;

    public Slider Xp;
    public Slider Vida;

    public delegate void LevelUpAction(int newLevel);
    public static event LevelUpAction OnLevelUp;

    private int initialLevel;
    private int initialExperience;
    private int initialExperienceToNextLevel;
    private float initialHealth;

    void Start()
    {
        // Guardar los valores iniciales
        initialLevel = currentLevel;
        initialExperience = currentExperience;
        initialExperienceToNextLevel = experienceToNextLevel;
        initialHealth = MaxHealth;

        // Inicializar los valores
        ResetStats();
        Vida.maxValue = MaxHealth; Vida.minValue = MinHealth;
    }

    void Update()
    {
        // Suaviza la actualización del slider
        //Xp.value = Mathf.Lerp(Xp.value, currentExperience, Time.deltaTime * 10f);
        Xp.value = currentExperience;
        Vida.value = CurrentHealth;
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
            experienceToNextLevel += Mathf.RoundToInt(experienceToNextLevel * 0.25f);

            // Recuperar 20% de salud al subir de nivel
            CurrentHealth = Mathf.Min(CurrentHealth + (MaxHealth * 0.2f), MaxHealth);

            // Notificar nivel nuevo
            OnLevelUp?.Invoke(currentLevel);

            // Comprobar si es el nivel de mejora (cada 5 niveles)
            if (currentLevel % 5 == 0)
            {
                // Llamar al método de mostrar mejoras del ImprovementManager
                ImprovementManager improvementManager = FindObjectOfType<ImprovementManager>();
                improvementManager.ShowImprovementsForLevel(currentLevel);
            }

            Xp.maxValue = experienceToNextLevel; // Actualizar el máximo del slider
        }
    }


    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= MinHealth)
        {
            CurrentHealth = MinHealth;
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        Debug.Log("¡La torreta ha muerto!");
        // Aquí puedes reiniciar el juego o la torreta
        ResetStats();
    }

    /// <summary>
    /// Reinicia todos los valores a los iniciales.
    /// </summary>
    public void ResetStats()
    {
        // Restablecer valores iniciales
        currentLevel = initialLevel;
        currentExperience = initialExperience;
        experienceToNextLevel = initialExperienceToNextLevel;

        MaxHealth = initialHealth;
        CurrentHealth = MaxHealth;

        // Actualizar la interfaz
        Xp.maxValue = experienceToNextLevel;
        Xp.value = currentExperience;

        Debug.Log("Valores restablecidos");
    }

    void OnDestroy()
    {
        OnLevelUp = null; // Evitar referencias nulas
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemigo"))
        {
            CurrentHealth -= 5;
        }
    }
}

