using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mejoras : MonoBehaviour
{

    public VidaTorreta VT;
    public GameObject ElegirMejora;
    // Start is called before the first frame update

    public Torreta Torreta;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Velocidad()
    {
        VT.ActiveObtXp();
        ElegirMejora.SetActive(false);
        Torreta.MejoraVelocidad();
        

    }
}
