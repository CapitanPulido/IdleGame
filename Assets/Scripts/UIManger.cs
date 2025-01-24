using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameObject panel; // Panel que contiene los botones
    public List<Button> buttons; // Lista de botones (arrástralos desde el Inspector)
    private List<Improvement> currentOptions = new List<Improvement>();

    public void ShowImprovementOptions(List<Improvement> options)
    {
        currentOptions = options;
        panel.SetActive(true); // Mostrar el panel
        Time.timeScale = 0f; // Pausar el juego

        // Actualizar los botones con las opciones disponibles
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i < options.Count)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].GetComponentInChildren<Text>().text = options[i].name;
                int index = i; // Capturar el índice para la función del botón
                buttons[i].onClick.RemoveAllListeners();
                buttons[i].onClick.AddListener(() => SelectImprovement(index));
            }
            else
            {
                buttons[i].gameObject.SetActive(false); // Ocultar botones innecesarios
            }
        }
    }

    public void SelectImprovement(int index)
    {
        Debug.Log($"Seleccionaste: {currentOptions[index].name}");
        ImprovementManager manager = FindObjectOfType<ImprovementManager>();
        manager.SelectImprovement(currentOptions[index]);

        panel.SetActive(false); // Ocultar el panel
        Time.timeScale = 1f; // Reanudar el juego
    }
}
