using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mENU : MonoBehaviour
{

    public GameObject opciones;

    public void Start()
    {
        opciones.SetActive(false);
    }
    public void Jugar()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Opciones()
    {
        SceneManager.LoadScene("Rick");
    }

    public void Opciones2()
    {
        opciones.SetActive(true);
    }    

    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }
}
