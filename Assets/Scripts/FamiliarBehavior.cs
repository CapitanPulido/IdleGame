using UnityEngine;

public class FamiliarBehavior : MonoBehaviour
{
    public float health = 50f;
    public float damage = 5f;
    public float attackCooldown = 1f;
    public float range = 2f;

    private Transform target;
    private float attackTimer;

    void Update()
    {
        // Buscar enemigos en rango
        if (target == null)
        {
            FindClosestEnemy();
        }

        // Atacar al enemigo
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= range)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackCooldown)
                {
                    Attack();
                    attackTimer = 0f;
                }
            }
            else
            {
                target = null;
            }
        }
    }

    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemigo");
        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy.transform;
        }
    }

    void Attack()
    {
        // Aquí podrías implementar daño al enemigo objetivo
        Debug.Log("Familiar atacó a " + target.name + " infligiendo " + damage + " de daño.");
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject); // Eliminar al familiar si su vida llega a 0
        }
    }
}

