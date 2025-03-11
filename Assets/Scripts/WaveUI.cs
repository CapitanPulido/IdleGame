using UnityEngine;
using TMPro; // Importar TextMeshPro

public class WaveUI : MonoBehaviour
{
    public TMP_Text waveText; // Cambiar Text a TMP_Text
    public TMP_Text enemiesRemainingText;

    private void OnEnable()
    {
        WaveManager.OnWaveStarted += UpdateWaveText;
    }

    private void OnDisable()
    {
        WaveManager.OnWaveStarted -= UpdateWaveText;
    }

    private void UpdateWaveText(int waveIndex)
    {
        waveText.text = $"{waveIndex + 0}";
    }

    public void UpdateEnemiesRemaining(int enemiesRemaining)
    {
        enemiesRemainingText.text = $"Enemigos restantes: {enemiesRemaining}";
    }
}
