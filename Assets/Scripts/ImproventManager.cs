using UnityEngine;
using System.Collections.Generic;

public class ImprovementManager : MonoBehaviour
{
    public List<Improvement> availableImprovements; // Mejoras disponibles inicialmente
    private List<Improvement> currentPool = new List<Improvement>(); // Pool de mejoras activas

    public GameObject familiarPrefab; // Prefab del familiar
    private GameObject currentFamiliar; // Referencia al familiar actual

    void OnEnable()
    {
        VidaTorreta.OnLevelUp += ShowImprovements;
    }

    void OnDisable()
    {
        VidaTorreta.OnLevelUp -= ShowImprovements;
    }

    void ShowImprovements(int newLevel)
    {
        currentPool = availableImprovements.FindAll(imp => imp.unlockLevel <= newLevel);
        var options = new List<Improvement>();

        while (options.Count < 5 && currentPool.Count > 0)
        {
            int randomIndex = Random.Range(0, currentPool.Count);
            options.Add(currentPool[randomIndex]);
            currentPool.RemoveAt(randomIndex);
        }

        // Mostrar opciones en la interfaz
        UIManager uiManager = FindObjectOfType<UIManager>();
        uiManager.ShowImprovementOptions(options);
    }

    public void SelectImprovement(Improvement selectedImprovement)
    {
        Debug.Log($"Has seleccionado: {selectedImprovement.name}");

        // Si tiene una evolución, añadirla al pool de mejoras
        if (selectedImprovement.nextEvolution != null)
        {
            availableImprovements.Add(selectedImprovement.nextEvolution);
        }

        // Eliminar la mejora seleccionada del pool inicial
        availableImprovements.Remove(selectedImprovement);
    }

    public void SummonFamiliar()
    {
        if (currentFamiliar != null)
        {
            Destroy(currentFamiliar); // Reemplazar el familiar si ya existe
        }

        currentFamiliar = Instantiate(familiarPrefab, transform.position, Quaternion.identity);
        Debug.Log("Familiar invocado");
    }

    public void ShowImprovementsForLevel(int level)
    {
        // Mostrar mejoras solo si el nivel es múltiplo de 5
        if (level % 5 == 0)
        {
            // Generar las opciones de mejora aquí
            List<Improvement> availableImprovementsForLevel = availableImprovements.FindAll(improvement => improvement.unlockLevel <= level);

            var options = new List<Improvement>();

            // Aquí puedes definir cuántas mejoras mostrar (por ejemplo, 3 opciones)
            while (options.Count < 3 && availableImprovementsForLevel.Count > 0)
            {
                int randomIndex = Random.Range(0, availableImprovementsForLevel.Count);
                options.Add(availableImprovementsForLevel[randomIndex]);
                availableImprovementsForLevel.RemoveAt(randomIndex);
            }

            // Mostrar las opciones en la interfaz
            UIManager uiManager = FindObjectOfType<UIManager>();
            uiManager.ShowImprovementOptions(options);
        }
    }

}

[System.Serializable]
    public class Improvement
    {
        public string name;
        public int unlockLevel;
        public string description;
        public Improvement nextEvolution; // Siguiente evolución de esta mejora
    }


