using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public Transform ca�on; // Referencia al ca��n
    public GameObject proyectilPrefab; // Prefab del proyectil
    public float rango = 5f; // Rango de la torreta
    public float velocidadRotacion = 5f; // Velocidad de rotaci�n
    public float tiempoEntreDisparos = 1f; // Tiempo inicial entre disparos
    public float Da�oHaciaEnemigos;
    public float Da�oFamiliar;
    public List<Sprite> sprites = new List<Sprite>();

    private SpriteRenderer spriteRenderer;

    private float tiempoDisparoRestante = 0f;

    private void Start()
    {
          spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Encuentra al enemigo m�s cercano
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

        // Presiona Escape para mejorar la velocidad de disparo
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            MejoraVelocidad(25f); // Disminuye el tiempo entre disparos un 25%
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

        if (-22.5f <= angulo && angulo <= 22.5f)
        {
            spriteRenderer.sprite = sprites[0];
        }
        //else if (22.5f <= angulo && angulo <= 67.5f)
        //{
        //    spriteRenderer.sprite = sprites[1];
        //}
        //else if (67.5f <= angulo && angulo <= 112.5f)
        //{
        //    spriteRenderer.sprite = sprites[2];
        //}
        //else if (112.5f <= angulo && angulo <= 157.5f)
        //{
        //    spriteRenderer.sprite = sprites[3];
        //}
    }

    void Disparar()
    {
        // Instancia el proyectil
        Instantiate(proyectilPrefab, ca�on.position, ca�on.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el rango de la torreta en la escena
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rango);
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