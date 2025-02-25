using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MejorasManager : MonoBehaviour
{
    public float cooldownZE;
    public Vector2 spawnAreaMin; // Esquina inferior izquierda del área de spawn
    public Vector2 spawnAreaMax; // Esquina superior derecha del área de spawn
    public GameObject ZonaEfectoFuego;
    private Vector2 lastSpawnPosition; // Para evitar que se repita la misma posición

    void Update()
    {
        cooldownZE += Time.deltaTime;
        if (cooldownZE >= 10)
        {
            SpawnZE();
            cooldownZE = 0;
        }
    }

    private void SpawnZE()
    {
        Vector2 spawnPosition;

        // Asegurar que la nueva posición no sea la misma que la anterior
        do
        {
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            spawnPosition = new Vector2(randomX, randomY);
        } while (spawnPosition == lastSpawnPosition);

        lastSpawnPosition = spawnPosition; // Guardamos la última posición
        Instantiate(ZonaEfectoFuego, spawnPosition, Quaternion.identity);
    }

}
