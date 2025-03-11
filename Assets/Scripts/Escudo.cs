using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{
    
    public bool Active = false;
    public GameObject Escudo1;
    public float Daño;

    public float CurrentHealth;
    public float MaxHealth = 100f;
    public float MinHealth = 0f;

    

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentHealth == MinHealth)
        {
            Escudo1.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            CurrentHealth = MinHealth;
        }
    }
    public void Activar()
    {
        Active = true;
    }
    public void ReActivar()
    {
        if (Active)
        {
            Escudo1.SetActive(true);
            CurrentHealth = MaxHealth;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            CurrentHealth -= Daño;
        }
    }

    public void AumentarVida(float porcentaje)
    {
        MaxHealth *= porcentaje;
        MaxHealth = Mathf.Max(MaxHealth, MinHealth);

        CurrentHealth *= porcentaje;
        CurrentHealth = Mathf.Max(MaxHealth, MinHealth);
    }


}
