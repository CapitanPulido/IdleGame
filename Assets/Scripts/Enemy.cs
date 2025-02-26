using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float MaxHealth;
    public float MinHealth;
    public float ActualHealth;
    public float Daño;
    public VidaTorreta VT;
    NavMeshAgent agent;


    public int experiencePoints = 50; // Experiencia otorgada por este enemigo

    // Start is called before the first frame update
    void Start()
    {
        ActualHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (ActualHealth <= 0)
        {
            Destroy(gameObject);
        }

        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Proyectil"))
        {
            TakeDamage(5);
        }

        if(collision.gameObject.CompareTag("ZEF"))
        {
            StartCoroutine(DañandoseRutina());
        }
        if(collision.gameObject.CompareTag("ZEH"))
        {
            agent.speed = agent.speed / 2;
        }
        if (collision.gameObject.CompareTag("ZEV"))
        {
            StartCoroutine(DañandoseRutina());
            agent.speed = agent.speed / 2;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(DañandoseRutina());
    }

    public void TakeDamage(float amount)
    {

        //VT.AddExperience(5);
        ActualHealth -= amount;
        if (ActualHealth <= 0)
        {
            Die();
        }
        Debug.Log("+xp");
    }
    private void Die()
    {
        WaveManager waveManager = FindObjectOfType<WaveManager>();
        if (waveManager != null)
        {
            waveManager.OnEnemyDefeated();
        }
        Destroy(gameObject);
    }

   IEnumerator DañandoseRutina()
    {
        ActualHealth -= 3;

        yield return new WaitForSeconds(2);

        StartCoroutine(DañandoseRutina());

        
    }
}
