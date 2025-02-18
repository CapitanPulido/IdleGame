using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public Transform cañon; // Referencia al cañón
    public GameObject proyectilPrefab; // Prefab del proyectil
    public float rango = 5f; // Rango de la torreta
    public float velocidadRotacion = 5f; // Velocidad de rotación
    public float tiempoEntreDisparos; // Tiempo entre disparos

    private float tiempoDisparoRestante = 0f;

    void Update()
    {
        // Encuentra al enemigo más cercano
        GameObject enemigoCercano = BuscarEnemigoCercano();
        if (enemigoCercano != null)
        {
            // Apunta al enemigo
            ApuntarAlEnemigo(enemigoCercano);

            // Dispara si es el momento adecuado
            tiempoDisparoRestante -= Time.deltaTime;
            if (tiempoDisparoRestante <= 0f)
            {
                Disparar();
                tiempoDisparoRestante = tiempoEntreDisparos;
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            MejoraVelocidad();
        }
    }

    GameObject BuscarEnemigoCercano()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        GameObject enemigoCercano = null;
        float distanciaMinima = Mathf.Infinity;

        foreach (GameObject enemigo in enemigos)
        {
            float distancia = Vector2.Distance(transform.position, enemigo.transform.position);
            if (distancia < distanciaMinima && distancia <= rango)
            {
                distanciaMinima = distancia;
                enemigoCercano = enemigo;
            }
        }

        return enemigoCercano;
    }

    void ApuntarAlEnemigo(GameObject enemigo)
    {
        Vector2 direccion = (Vector2)(enemigo.transform.position - cañon.position);
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        Quaternion rotacionObjetivo = Quaternion.AngleAxis(angulo, Vector3.forward);
        cañon.rotation = Quaternion.Lerp(cañon.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);
    }

    void Disparar()
    {
        // Instancia el proyectil
        Instantiate(proyectilPrefab, cañon.position, cañon.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el rango de la torreta en la escena
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rango);
    }

    public void MejoraVelocidad()
    {
        Debug.Log("mejora");
        tiempoEntreDisparos -= Mathf.RoundToInt(tiempoEntreDisparos * 0.25f);
    }

}