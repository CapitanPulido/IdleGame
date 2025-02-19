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
    public float currentExperience = 0;
    public int experienceToNextLevel = 100;

    public Slider Xp;
    public Slider Vida;

    public delegate void LevelUpAction(int newLevel);
    public static event LevelUpAction OnLevelUp;

    private int initialLevel;
    private float initialExperience;
    private int initialExperienceToNextLevel;
    private float initialHealth;
    public bool ObtXp = true;

    public GameObject ElegirMejora;

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
       
        Xp.value = currentExperience;
        Vida.value = CurrentHealth;
       if( ObtXp == true ) 
        {
            currentExperience += (20 * Time.deltaTime);
        }

        if(currentExperience >= 100)
        {
            ObtXp = false;
            currentExperience = 0;
            Xp.value = 0;
            currentLevel += 1;
            
            ElegirMejora.SetActive(true);   
        }
    }

    public void ActiveObtXp()
    {
        ObtXp = true;
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

    public void MejoraVida(float porcentaje)
    {
        // Aumenta la vida manera porcentual
        float Aumento = MaxHealth * (porcentaje);
        MaxHealth += Aumento;

        Debug.Log("Nueva Vida");
    }
}

