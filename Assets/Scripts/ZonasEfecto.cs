using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonasEfecto : MonoBehaviour
{
    public float Daño;
    public float CooldownZE;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // cooldown tiempo de vida
        CooldownZE = CooldownZE + Time.deltaTime;    
        if (CooldownZE >= 10 )
        {
            Destroy(gameObject);
        }
    }

   
}
