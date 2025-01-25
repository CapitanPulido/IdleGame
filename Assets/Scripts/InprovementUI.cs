using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImprovementUI : MonoBehaviour
{
    

    public GameObject improvementPanel;        // Panel que contiene la interfaz de mejoras
    public ImprovementOption[] options;        // Lista de opciones de mejora
    private System.Action<int> onImprovementSelected; // Callback para procesar la mejora seleccionada

    public void ShowImprovements(string[] names, string[] descriptions, Sprite[] icons, System.Action<int> callback)
    {
        // Activar el panel de mejoras
        improvementPanel.SetActive(true);
        onImprovementSelected = callback;

        // Configurar los textos, sprites y botones
        for (int i = 0; i < options.Length; i++)
        {
            if (i < names.Length)
            {
                options[i].improvementName.text = names[i];
                options[i].improvementDescription.text = descriptions[i];
                options[i].improvementIcon.sprite = icons[i];
                options[i].improvementIcon.gameObject.SetActive(true);

                options[i].improvementButton.gameObject.SetActive(true);
                int index = i; // Necesario para evitar problemas de referencias en closures
                options[i].improvementButton.onClick.RemoveAllListeners();
                options[i].improvementButton.onClick.AddListener(() => SelectImprovement(index));
            }
            else
            {
                // Ocultar opciones sobrantes
                options[i].improvementName.text = "";
                options[i].improvementDescription.text = "";
                options[i].improvementIcon.gameObject.SetActive(false);
                options[i].improvementButton.gameObject.SetActive(false);
            }
        }
    }

    public void HideImprovements()
    {
        improvementPanel.SetActive(false);
    }

    private void SelectImprovement(int index)
    {
        // Procesar la mejora seleccionada
        onImprovementSelected?.Invoke(index);
        HideImprovements();
    }
}

[System.Serializable]
public class ImprovementOption
{
    public TMP_Text improvementName;       // Nombre de la mejora
    public TMP_Text improvementDescription; // Descripción de la mejora
    public Image improvementIcon;          // Icono de la mejora
    public Button improvementButton;       // Botón para seleccionar la mejora
}