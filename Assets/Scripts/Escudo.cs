using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{
    public float EscudoCooldown;
    public bool Active = false;
    public GameObject Escudo1;
    public float Daño;

    public float CurrentHealth;
    public float MaxHealth = 100f;
    public float MinHealth = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentHealth == 0)
        {
            Escudo1.SetActive(false);
        }
       
        EscudoCooldown = EscudoCooldown + Time.deltaTime; 

        if (Active && EscudoCooldown >= 10)
        {
            Escudo1.SetActive(true);
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            CurrentHealth -= Daño;
        }
    }

}
