using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mejoras2 : MonoBehaviour
{
    public GameObject ElegirMejora;
    public Torreta torreta;
    public VidaTorreta VT;
    public ZEF zef;
    public ZEH zeh;
    public ZEV zev;


    // Referencias a los botones, imágenes y textos
    public Button[] botones;
    public RawImage[] imagenes;
    public TextMeshProUGUI[] textos;

    // Lista de acciones
    private List<System.Action> acciones = new List<System.Action>();
    private List<string> nombres = new List<string>();
    private List<Texture> imagenesAcciones = new List<Texture>();
    public Transform[] spawnPoints;

    public GameObject familiar;
    public GameObject ZonaF;
    public GameObject ZonaH;
    public GameObject ZonaV;

    void Start()
    {
        torreta = GameObject.FindGameObjectWithTag("Player").GetComponent<Torreta>();

        zef = GameObject.FindGameObjectWithTag("ZEF").GetComponent<ZEF>();
        zeh = GameObject.FindGameObjectWithTag("ZEH").GetComponent<ZEH>();
        zev = GameObject.FindGameObjectWithTag("ZEV").GetComponent<ZEV>();

    }

    void Update()
    {


    }

    public void Velocidad()
    {
        VT.ActiveObtXp();
        ElegirMejora.SetActive(false);
        torreta.MejoraVelocidad(0.25f);
    }

    public void Daño()
    {
        VT.ActiveObtXp();
        ElegirMejora.SetActive(false);
        torreta.MejoraDaño(0.25f);
    }

    public void Rango()
    {
        VT.ActiveObtXp();
        ElegirMejora.SetActive(false);
        torreta.MejoraRango(0.25f);
    }

    public void Vida()
    {
        VT.ActiveObtXp();
        ElegirMejora.SetActive(false);
        VT.MejoraVida(0.25f);
    }

    public void VidaExtra()
    {
        VT.ActiveObtXp();
        VT.VidaExtra1();
        ElegirMejora.SetActive(false);
    }

    public void ZEF()
    {
        zef.Active();
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();

    }

    public void ZEFMT()
    {
        zef.AumentarTamaño(0.10f);
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();

    }
    public void ZEFMC()
    {
        zef.ReducirCooldown(0.25f);
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();

    }
    public void ZEH()
    {
        zeh.Active();
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();

    }
    public void ZEHMT()
    {
        zeh.AumentarTamaño(0.10f);
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();

    }
    public void ZEHMC()
    {
        zeh.ReducirCooldown(0.25f);
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();

    }
    public void ZEV()
    {
        zev.Active();
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();

    }
    public void ZEVMT()
    {
        zev.AumentarTamaño(0.10f);
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();

    }
    public void ZEVMC()
    {
        zev.ReducirCooldown(0.25f);
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();

    }
    public void Familiar()
    {
        SpawnFamiliar();
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();
    }



    public void AgregarAcciones()
    {
        acciones.Clear();
        nombres.Clear();
        imagenesAcciones.Clear();

        // Agregar acciones y nombres
        acciones.Add(Velocidad);
        nombres.Add("Velocidad");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/Velocidad"));

        acciones.Add(Daño);
        nombres.Add("Daño");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/Daño"));

        acciones.Add(Rango);
        nombres.Add("Rango");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/Rango"));

        acciones.Add(Vida);
        nombres.Add("Vida");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/Vida"));

        acciones.Add(VidaExtra);
        nombres.Add("VidaExtra");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/VidaExtra"));

        acciones.Add(ZEF);
        nombres.Add("ZEF");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/ZEF"));

        acciones.Add(ZEH);
        nombres.Add("ZEH");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/ZEH"));

        acciones.Add(ZEV);
        nombres.Add("ZEV");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/ZEV"));

        acciones.Add(ZEFMT);
        nombres.Add("ZEF");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/ZEF"));

        acciones.Add(ZEHMT);
        nombres.Add("ZEH");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/ZEH"));

        acciones.Add(ZEVMT);
        nombres.Add("ZEV");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/ZEV"));

        acciones.Add(ZEFMC);
        nombres.Add("ZEF");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/ZEF"));

        acciones.Add(ZEHMC);
        nombres.Add("ZEH");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/ZEH"));

        acciones.Add(ZEVMC);
        nombres.Add("ZEV");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/ZEV"));

        acciones.Add(SpawnFamiliar);
        nombres.Add("Familiar");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/Familiar"));



        // Elegir 3 acciones aleatorias sin repetir
        List<int> indicesSeleccionados = new List<int>();
        for (int i = 0; i < botones.Length; i++)
        {
            int indice = Random.Range(0, acciones.Count);
            while (!indicesSeleccionados.Contains(indice))
            {
                indicesSeleccionados.Add(indice);
            }

            // Eliminar listeners previos para evitar acumulación
            botones[i].onClick.RemoveAllListeners();

            // Asignar acción al botón
            int indiceCopia = indice; // Necesario para evitar problemas con el closure en la lambda
            botones[i].onClick.AddListener(() => acciones[indiceCopia].Invoke());

            // Asignar imagen y texto
            imagenes[i].texture = imagenesAcciones[indiceCopia];
            textos[i].text = nombres[indiceCopia];
        }
    }


    public void SpawnFamiliar()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(familiar, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Familiar generado: " + familiar.name);
    }
}
