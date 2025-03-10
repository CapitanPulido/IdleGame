using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMejoras : MonoBehaviour
{

    public Mejoras mejora;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        mejora.AgregarAcciones();
    }

    public void OnDisable()
    {

    }
}
