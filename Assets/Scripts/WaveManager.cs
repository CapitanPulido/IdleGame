using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Referencias a los diferentes tipos de enemigos
    public List<GameObject> fastEnemies;
    public List<GameObject> mediumEnemies;
    public List<GameObject> slowEnemies;
    public List<GameObject> bosses; // Ordenados del más fácil al más difícil

    public Transform[] spawnPoints; // Puntos de aparición de enemigos

    private int currentWave = 1; // Contador de oleadas
    private bool isSpawning = false;
    private int enemiesRemainingInWave;

    public delegate void WaveStarted(int waveIndex);
    public static event WaveStarted OnWaveStarted;

    private void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        Debug.Log("Oleada: " + currentWave);

        if (OnWaveStarted != null)
        {
            OnWaveStarted.Invoke(currentWave);
        }

        // Revisar si es oleada de jefe
        if (currentWave % 10 == 0)
        {
            StartCoroutine(SpawnBossWave());
        }
        else
        {
            StartCoroutine(SpawnRegularWave());
        }
    }

    private IEnumerator SpawnRegularWave()
    {
        isSpawning = true;

        // Calcular la cantidad total de enemigos basados en la oleada actual
        int totalEnemies = Mathf.RoundToInt(currentWave * 1.5f) + 3;
        enemiesRemainingInWave = totalEnemies;

        while (totalEnemies > 0)
        {
            // Determinar el tipo de enemigo basado en la oleada actual
            GameObject enemyToSpawn = GetEnemyForCurrentWave();
            SpawnEnemy(enemyToSpawn);
            totalEnemies--;

            yield return new WaitForSeconds(1f / (currentWave * 0.2f + 1)); // Aumenta la velocidad de spawn con el progreso de oleadas
        }

        isSpawning = false;
    }

    private IEnumerator SpawnBossWave()
    {
        isSpawning = true;

        // Determinar el jefe según la oleada actual
        int bossIndex = Mathf.Min(currentWave / 10 - 1, bosses.Count - 1);
        GameObject bossToSpawn = bosses[bossIndex];

        SpawnEnemy(bossToSpawn);
        enemiesRemainingInWave = 1;

        isSpawning = false;
        yield return null;
    }

    private GameObject GetEnemyForCurrentWave()
    {
        int waveLevel = currentWave;
        int fastChance = Mathf.Clamp(60 - waveLevel, 10, 40);
        int mediumChance = Mathf.Clamp(40 + waveLevel / 2, 20, 50);
        int slowChance = Mathf.Clamp(0 + waveLevel / 1, 2, 3);

        int randomValue = Random.Range(0, 100);

        // Verifica si ya se alcanzó la oleada necesaria para los fastEnemies
        if (waveLevel > 10 && randomValue < fastChance && fastEnemies.Count > 0)
        {
            return fastEnemies[Random.Range(0, fastEnemies.Count)];
        }

        // Verifica si ya se alcanzó la oleada necesaria para los slowEnemies
        if (waveLevel > 20 && randomValue < fastChance + mediumChance + slowChance && slowEnemies.Count > 0)
        {
            return slowEnemies[Random.Range(0, slowEnemies.Count)];
        }

        // Si no cumple ninguna condición anterior, solo puede aparecer un mediumEnemy
        if (mediumEnemies.Count > 0)
        {
            return mediumEnemies[Random.Range(0, mediumEnemies.Count)];
        }

        // Por defecto, devuelve null (aunque esto no debería ocurrir si las listas están bien configuradas)
        Debug.LogWarning("No hay enemigos disponibles para generar.");
        return null;
    }
    

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Enemigo generado: " + enemyPrefab.name);
    }

    public void OnEnemyDefeated()
    {
        enemiesRemainingInWave--;

        if (enemiesRemainingInWave <= 0 && !isSpawning)
        {
            Debug.Log("Oleada " + currentWave + " completada.");
            currentWave++;
            StartNextWave();
        }
    }
}