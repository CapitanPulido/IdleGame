using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mENU : MonoBehaviour
{
    // Start is called before the first frame update
    public void Jugar()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Opciones()
    {
        SceneManager.LoadScene("Rick");
    }
}
