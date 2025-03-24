using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public bool active = false;
    public GameObject dragon;
    // Start is called before the first frame update

    public void Awake()
    {
        dragon.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            dragon.SetActive(true);
        }
    }

    public void Active()
    {
        active = true;
    }
}
