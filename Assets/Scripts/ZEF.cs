using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZEF : MonoBehaviour
{
    public static bool MejoraZEF = false;
    public GameObject ZonaEfectoFuego;
    public float cooldownZE;
    public bool active = false;

    [Header("Cooldown Config")]
    public float cooldownMax = 10f; // Tiempo inicial entre spawns
    public float minCooldown = 2f; // Límite mínimo del cooldown

    [Header("Spawn Area")]
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    private Vector2 lastSpawnPosition;
    private float minDistance = 0.5f;

    [Header("Scaling Config")]
    public float currentScaleMultiplier = 1f; // Factor actual de escala
    public float maxScale = 3f; // Tamaño máximo permitido

    void Update()
    {
       

        cooldownZE += Time.deltaTime;

        if (active && cooldownZE >= cooldownMax)
        {
            SpawnZE();
            cooldownZE = 0;
        }

        if (cooldownZE >= cooldownMax)
        {
            
            cooldownZE = 0;
        }

    }

    private void SpawnZE()
    {
        Vector2 spawnPosition;
        int maxAttempts = 10;
        int attempts = 0;

        do
        {
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            spawnPosition = new Vector2(randomX, randomY);
            attempts++;
        }
        while (Vector2.Distance(spawnPosition, lastSpawnPosition) < minDistance && attempts < maxAttempts);

        lastSpawnPosition = spawnPosition;
        GameObject newZE = Instantiate(ZonaEfectoFuego, spawnPosition, Quaternion.identity);

        // Ajustar el tamaño del objeto hasta el límite máximo
        float newScale = Mathf.Min(newZE.transform.localScale.x * currentScaleMultiplier, maxScale);
        newZE.transform.localScale = new Vector3(newScale, newScale, 1f);
    }

    // 🔹 Aumenta el tamaño del objeto en porcentaje (ej: 1.1f para 10% más)
    public void AumentarTamaño(float porcentaje)
    {
        currentScaleMultiplier *= porcentaje;
        currentScaleMultiplier = Mathf.Min(currentScaleMultiplier, maxScale);
    }

    // 🔹 Reduce el cooldown en porcentaje (ej: 0.9f para reducir 10%)
    public void ReducirCooldown(float porcentaje)
    {
        cooldownMax *= porcentaje;
        cooldownMax = Mathf.Max(cooldownMax, minCooldown);
    }

    public void Active()
    {
        active = true;
    }
}
