using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mejoras : MonoBehaviour
{
    public GameObject ElegirMejora;
    public Torreta Torreta;
    public VidaTorreta VT;

    // Referencias a los botones, imágenes y textos
    public Button[] botones;
    public RawImage[] imagenes;
    public TextMeshProUGUI[] textos;

    // Lista de acciones
    private List<System.Action> acciones = new List<System.Action>();
    private List<string> nombres = new List<string>();
    private List<Texture> imagenesAcciones = new List<Texture>();

    void Start()
    {
    
    }

    public void Velocidad()
    {
        VT.ActiveObtXp();
        ElegirMejora.SetActive(false);
        Torreta.MejoraVelocidad(0.25f);
    }

    public void Daño()
    {
        VT.ActiveObtXp();
        ElegirMejora.SetActive(false);
        Torreta.MejoraDaño(0.25f);
    }

    public void Rango()
    {
        VT.ActiveObtXp();
        ElegirMejora.SetActive(false);
        Torreta.MejoraRango(0.25f);
    }

    public void Vida()
    {
        VT.ActiveObtXp();
        ElegirMejora.SetActive(false);
        VT.MejoraVida(0.25f);
    }

    public void AgregarAcciones()
    {
        // Agregar acciones y nombres
        acciones.Add(Velocidad);
        nombres.Add("Velocidad");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/Velocidad"));

        acciones.Add(Daño);
        nombres.Add("Daño");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/Dano"));

        acciones.Add(Rango);
        nombres.Add("Rango");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/Rango"));

        acciones.Add(Vida);
        nombres.Add("Vida");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/Vida"));

        acciones.Add(VidaExtra);
        nombres.Add("VidaExtra");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/Vida"));



        // Elegir 3 acciones aleatorias sin repetir
        List<int> indicesSeleccionados = new List<int>();
        for (int i = 0; i < botones.Length; i++)
        {
            int indice;
            do
            {
                indice = Random.Range(0, acciones.Count);
            } while (indicesSeleccionados.Contains(indice));

            indicesSeleccionados.Add(indice);

            // Asignar acción al botón
            int indiceCopia = indice; // Necesario para evitar problemas con el closure en la lambda
            botones[i].onClick.AddListener(() => acciones[indiceCopia].Invoke());

            // Asignar imagen y texto
            imagenes[i].texture = imagenesAcciones[indiceCopia];
            textos[i].text = nombres[indiceCopia];
        }
    }

    public void VidaExtra()
    {
        VT.VidaExtra1();
    }
}