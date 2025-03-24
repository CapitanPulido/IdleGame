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
    public float Da�o;
    public VidaTorreta VT;
    NavMeshAgent agent;
    public Torreta Torreta;
    public float DT;
    public float DF;

    public void Awake()
    {
        Torreta = GameObject.FindGameObjectWithTag("Player").GetComponent<Torreta>();
    }


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
            Die();
        }

        DT = Torreta.Da�oHaciaEnemigos;
        DF = Torreta.Da�oFamiliar;

        if (Input.GetKeyUp(KeyCode.O))
        {
            ActualHealth = 0;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Proyectil"))
        {
            TakeDamage(DT);
        }
        if (collision.gameObject.CompareTag("Familiar"))
        {
            TakeDamage(DF);
        }

        if (collision.gameObject.CompareTag("ZEF"))
        {
            StartCoroutine(Da�andoseRutina());
        }
        if(collision.gameObject.CompareTag("ZEH"))
        {
            agent.speed = agent.speed / 2;
        }
        if (collision.gameObject.CompareTag("ZEV"))
        {
            StartCoroutine(Da�andoseRutina());
            agent.speed = agent.speed / 2;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(Da�andoseRutina());
    }

    public void TakeDamage(float amount)
    {
        ActualHealth -= amount;        
    }
    public void Die()
    {
        WaveManager waveManager = FindObjectOfType<WaveManager>();
        if (waveManager != null)
        {
            waveManager.OnEnemyDefeated();
        }
        Destroy(gameObject);
    }

   IEnumerator Da�andoseRutina()
    {
        ActualHealth -= 30;

        yield return new WaitForSeconds(2);

        StartCoroutine(Da�andoseRutina());

        
    }

    
}
