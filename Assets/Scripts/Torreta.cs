using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public Transform cañon;
    public GameObject proyectilComun; // Proyectil normal
    public GameObject proyectilEspecial; // Proyectil que se dispara ocasionalmente
    public float rango = 5f;
    public float velocidadRotacion = 5f;
    public float tiempoEntreDisparos = 1f;
    public float tiempoCambioProyectil = 5f; // Cada cuánto se dispara un proyectil especial
    public float DañoHaciaEnemigos;
    public float DañoFamiliar;

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
        Vector2 direccion = (Vector2)(enemigo.transform.position - cañon.position);
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        Quaternion rotacionObjetivo = Quaternion.AngleAxis(angulo, Vector3.forward);
        cañon.rotation = Quaternion.Lerp(cañon.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);
    }

    void Disparar()
    {
        if (usarProyectilEspecial && proyectilEspecial != null)
        {
            Instantiate(proyectilEspecial, cañon.position, cañon.rotation);
            usarProyectilEspecial = false; // Después del disparo especial, vuelve al común
        }
        else
        {
            Instantiate(proyectilComun, cañon.position, cañon.rotation);
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

        // Asegura un tiempo mínimo para evitar disparos instantáneos
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

        Debug.Log("Nuevo Daño");
    }

    public void MejoraDaño(float porcentaje)
    {
        // Aumenta el daño de manera porcentual
        float Aumento = DañoHaciaEnemigos * (porcentaje);
        DañoHaciaEnemigos += Aumento;

        Debug.Log("Nuevo Daño");
    }

}
