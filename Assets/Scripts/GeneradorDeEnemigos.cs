using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeEnemigos : MonoBehaviour
{
    [Header("Configuraci�n")]
    public GameObject prefabEnemigo; // Prefab del enemigo a generar
    public Transform[] puntosGeneracion; // Puntos donde se generar�n enemigos
    public float tiempoEntreGeneraciones = 2f; // Tiempo entre cada generaci�n

    private bool generando = false;

    private void OnDrawGizmos()
    {
        // Dibujar los puntos de generaci�n en la escena
        Gizmos.color = Color.red;
        foreach (Transform punto in puntosGeneracion)
        {
            if (punto != null)
                Gizmos.DrawSphere(punto.position, 0.2f);
        }
    }

    private void Start()
    {
        // Comenzar la generaci�n de enemigos
        if (!generando)
            StartCoroutine(GenerarEnemigos());
    }

    private IEnumerator GenerarEnemigos()
    {
        generando = true;

        while (true)
        {
            foreach (Transform punto in puntosGeneracion)
            {
                if (punto != null && prefabEnemigo != null)
                {
                    // Instanciar enemigo en el punto
                    Instantiate(prefabEnemigo, punto.position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(tiempoEntreGeneraciones);
        }
    }
}