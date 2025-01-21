using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MaxHealth;
    public float MinHealth;
    public float ActualHealth;
    // Start is called before the first frame update
    void Start()
    {
        ActualHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (ActualHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Proyectil"))
        {
            ActualHealth -= 1;
        }

    }
}
