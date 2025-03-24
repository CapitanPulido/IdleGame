using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilEnemy : MonoBehaviour
{
    public float velocidad = 10f; // Velocidad del proyectil
    public float tiempoVida = 3f; // Tiempo antes de destruir el proyectil

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cuerpo"))
        {
            // Aquí puedes añadir lógica para aplicar daño al enemigo
            //Destroy(collision.gameObject); // Ejemplo: destruir el enemigo
            Destroy(gameObject); // Destruir el proyectil
        }
    }
}
