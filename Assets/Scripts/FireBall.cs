using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float velocidad = 10f; // Velocidad del proyectil
    public float tiempoVida = 3f; // Tiempo antes de destruir el proyectil
    public float escalaPorcentual = 100f; // Escala en porcentaje (100 = sin cambios, 150 = 1.5x, etc.)

    void Start()
    {
        AjustarEscala(escalaPorcentual);
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void AjustarEscala(float porcentaje)
    {
        float escalaFactor = porcentaje / 100f;
        transform.localScale *= escalaFactor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            // Aquí puedes añadir lógica para aplicar daño al enemigo
            //Destroy(collision.gameObject); // Ejemplo: destruir el enemigo
            //Destroy(gameObject); // Destruir el proyectil
        }
    }

    public void Escala()
    {
        escalaPorcentual += escalaPorcentual * 1.2F;
    }
}

