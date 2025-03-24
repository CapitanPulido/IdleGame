using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReinaSlime : MonoBehaviour
{
    public float MaxHealth;
    public float ActualHealth;
    public float Daño;
    public float Speed;
    public Transform ZonaDeAtaque;
    private NavMeshAgent agent;
    private GameObject jugador;
    private bool enZonaDeAtaque = false;

    public GameObject aliadoPrefab; // Aliados normales
    public GameObject miniSlimePrefab; // Mini slimes al dividirse
    public GameObject proyectilPrefab; // Proyectiles de slime
    public Transform[] puntosDeInvocacion; // Puntos para invocar

    public float invocarCooldown;
    public float saltoCooldown ;
    public float proyectilCooldown;
    public float divisionCooldown;

    private bool puedeInvocar = true;
    private bool puedeSaltar = true;
    private bool puedeDisparar = true;
    private bool puedeDividir = true;

    public Animator animator;

    public Transform cañon;
    public GameObject proyectilComun; // Proyectil normal

    private enum EstadoJefe { Moviendo, Atacando }
    private EstadoJefe estadoJefe;

    void Awake()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        ActualHealth = MaxHealth;
        estadoJefe = EstadoJefe.Moviendo;

        

    }

    void Update()
    {
        if (ActualHealth <= 0)
        {
            Die();
            return;
        }

        if (Vector3.Distance(transform.position, ZonaDeAtaque.position) < 2f && !enZonaDeAtaque)
        {
            enZonaDeAtaque = true;
            IniciarPatronDeAtaque();
        }

        if (estadoJefe == EstadoJefe.Moviendo)
        {
            MoverHaciaJugador();
        }
        
        GestionarPatrones();
        

        // Flip del sprite hacia el jugador
        FlipSpriteHaciaJugador();

    }

    void MoverHaciaJugador()
    {
        agent.SetDestination(ZonaDeAtaque.position);
    }

    void IniciarPatronDeAtaque()
    {
        estadoJefe = EstadoJefe.Atacando;
    }

    void GestionarPatrones()
    {
        if (puedeInvocar)
        {
            StartCoroutine(PatronInvocarAliados());
        }

        if (puedeSaltar)
        {
            
            StartCoroutine(PatronSaltoAplastante());
        }

        if (puedeDisparar)
        {
            StartCoroutine(PatronDispararProyectiles());
        }

        if (ActualHealth <= MaxHealth * 0.5f && puedeDividir)
        {
            StartCoroutine(PatronDivisionDeSlimes());
        }
    }

    IEnumerator PatronInvocarAliados()
    {
        puedeInvocar = false;
        foreach (Transform punto in puntosDeInvocacion)
        {
            Instantiate(aliadoPrefab, punto.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(invocarCooldown);
        puedeInvocar = true;
    }

    IEnumerator PatronSaltoAplastante()
    {
        puedeSaltar = false;

        // Simular el salto (puedes añadir una animación aquí)
        animator.Play("ReinaSalto");
        agent.isStopped = true;
        yield return new WaitForSeconds(2); // Tiempo de preparación del salto

        animator.Play("ReinaIdle");
        // Simular caída en el mismo lugar
        agent.isStopped = false;

        // Daño en área tras el aterrizaje
        Collider[] objetosCercanos = Physics.OverlapSphere(transform.position, 3f);
        foreach (Collider objeto in objetosCercanos)
        {
            if (objeto.CompareTag("Player"))
            {
                objeto.GetComponent<VidaTorreta>()?.TakeDamage(Daño);
            }
        }

        yield return new WaitForSeconds(saltoCooldown);
        puedeSaltar = true;
        Debug.Log("puede saltar");
    }

    IEnumerator PatronDispararProyectiles()
    {
        puedeDisparar = false;
  
        Disparar();
        yield return new WaitForSeconds(1);
        Disparar();
        yield return new WaitForSeconds(1);
        Disparar();  

        yield return new WaitForSeconds(proyectilCooldown);
        puedeDisparar = true;
    }

    void Disparar()
    {
        
       Instantiate(proyectilComun, cañon.position, cañon.rotation);
        
    }

    IEnumerator PatronDivisionDeSlimes()
    {
        puedeDividir = false;

        for (int i = 0; i < 4; i++) // Genera 4 mini slimes
        {
            Vector3 spawnPos = transform.position + Random.insideUnitSphere * 2;
            spawnPos.y = transform.position.y;
            Instantiate(miniSlimePrefab, spawnPos, Quaternion.identity);
        }

        yield return new WaitForSeconds(divisionCooldown);
        puedeDividir = true;
    }

    public void TakeDamage(float amount)
    {
        ActualHealth -= amount;
        if (ActualHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("La Reina Slime ha sido derrotada");
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        // Visualiza el área de daño del salto
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , 3f);
    }

    // Función para girar el sprite hacia el jugador
    void FlipSpriteHaciaJugador()
    {
        if (jugador != null)
        {
            Vector3 direccion = jugador.transform.position - transform.position;

            // Si el jugador está a la derecha, el sprite no se voltea (1), si está a la izquierda, se voltea (-1)
            if (direccion.x > 0)
            {
                transform.localScale = new Vector3(5, 5, 5);
            }
            else if (direccion.x < 0)
            {
                transform.localScale = new Vector3(-5, 5, 5);
            }
        }
    }
}
