using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public Transform ca�on;
    public GameObject proyectilComun; // Proyectil normal
    public GameObject proyectilEspecial; // Proyectil que se dispara ocasionalmente
    public float rango = 5f;
    public float velocidadRotacion = 5f;
    public float tiempoEntreDisparos = 1f;
    public float tiempoCambioProyectil = 5f; // Cada cu�nto se dispara un proyectil especial
    public float Da�oHaciaEnemigos;
    public float Da�oFamiliar;

    private float tiempoDisparoRestante = 0f;
    private float tiempoCambioRestante;
    private bool usarProyectilEspecial = false;
    public bool FireballActive = false;
    public AudioSource shoot;

    private void Start()
    {
        tiempoCambioRestante = tiempoCambioProyectil;
    }

    void Update()
    {
        GameObject enemigoCercano = BuscarEnemigoCercano();
        if (enemigoCercano != null)
        {
            ApuntarAlEnemigo(enemigoCercano);

            tiempoDisparoRestante -= Time.deltaTime;
            if (tiempoDisparoRestante <= 0f)
            {
                Disparar();
                tiempoDisparoRestante = tiempoEntreDisparos;
            }
        }

        // Cuenta regresiva para disparar un proyectil especial
        tiempoCambioRestante -= Time.deltaTime;
        if (FireballActive && tiempoCambioRestante <= 0f)
        {
            usarProyectilEspecial = true;
            tiempoCambioRestante = tiempoCambioProyectil;
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
        Vector2 direccion = (Vector2)(enemigo.transform.position - ca�on.position);
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        Quaternion rotacionObjetivo = Quaternion.AngleAxis(angulo, Vector3.forward);
        ca�on.rotation = Quaternion.Lerp(ca�on.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);
    }

    void Disparar()
    {
        if (usarProyectilEspecial && proyectilEspecial != null)
        {
            Instantiate(proyectilEspecial, ca�on.position, ca�on.rotation);
            usarProyectilEspecial = false; // Despu�s del disparo especial, vuelve al com�n
        }
        else
        {
            Instantiate(proyectilComun, ca�on.position, ca�on.rotation);
        }

        shoot.Play();
    }

    public void ActivarFireball()
    {
        FireballActive = true;
    }


    public void MejoraVelocidad(float porcentaje)
    {
        // Disminuye el tiempo entre disparos de manera porcentual
        float reduccion = tiempoEntreDisparos * (porcentaje);
        tiempoEntreDisparos -= reduccion;

        // Asegura un tiempo m�nimo para evitar disparos instant�neos
        if (tiempoEntreDisparos < 0.1f)
        {
            tiempoEntreDisparos = 0.1f;
        }

        Debug.Log("Nueva velocidad de disparo: " + tiempoEntreDisparos);
    }



    public void MejoraRango(float porcentaje)
    {
        // Aumenta el rango de manera porcentual
        float Aumento = rango * (porcentaje);
        rango += Aumento;

        Debug.Log("Nuevo Da�o");
    }

    public void MejoraDa�o(float porcentaje)
    {
        // Aumenta el da�o de manera porcentual
        float Aumento = Da�oHaciaEnemigos * (porcentaje);
        Da�oHaciaEnemigos += Aumento;

        Debug.Log("Nuevo Da�o");
    }

}
