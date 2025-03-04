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
    public Transform[] spawnPoints;

    public float cooldownZEF;
    public float cooldownZEH;
    public float cooldownZEV;
    public Vector2 spawnAreaMin; // Esquina inferior izquierda del área de spawn
    public Vector2 spawnAreaMax; // Esquina superior derecha del área de spawn
    public GameObject familiar;
    public GameObject ZonaEfectoFuego;
    public GameObject ZonaEfectoHielo;
    public GameObject ZonaEfectoVeneno;
    private Vector2 lastSpawnPosition; // Para evitar que se repita la misma posición

    public bool MejoraZEF = false;
    public bool MejoraZEH = false;
    public bool MejoraZEV = false;



    void Start()
    {
    
    }

    void Update()
    {
        cooldownZEF += Time.deltaTime;
        cooldownZEH += Time.deltaTime;
        cooldownZEV += Time.deltaTime;

        if (MejoraZEF == true && cooldownZEF >= 10)
        {
            SpawnZEF();
            cooldownZEF = 0;
        }
        if (MejoraZEH == true && cooldownZEH >= 10)
        {
            SpawnZEH();
            cooldownZEH = 0;
        }
        if (MejoraZEV == true && cooldownZEV >= 10)
        {
            SpawnZEV();
            cooldownZEV = 0;
        }

        //if (cooldownZEF >= 10)
        //{
        //    cooldownZEF = 0;
        //}
        //if (cooldownZEV >= 10)
        //{
        //    cooldownZEV = 0;
        //}
        //if (cooldownZEH >= 10)
        //{
        //    cooldownZEH = 0;
        //}

        if(Input.GetKeyDown(KeyCode.Z))
        {
            ZEF();
        }

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

    public void VidaExtra()
    {
        VT.ActiveObtXp();
        VT.VidaExtra1();
        ElegirMejora.SetActive(false);
    }

    public void ZEF()
    {
        MejoraZEF = true;
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();
    }
    public void ZEH()
    {
        MejoraZEH = true;
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();
    }
    public void ZEV()
    {
        MejoraZEV = true;
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

        acciones.Add(SpawnFamiliar);
        nombres.Add("Familiar");
        imagenesAcciones.Add(Resources.Load<Texture>("Imagenes/Familiar"));

        

            // Elegir 3 acciones aleatorias sin repetir
            List<int> indicesSeleccionados = new List<int>();
        for (int i = 0; i < botones.Length; i++)
        {
            int indice = Random.Range(0, acciones.Count); ;
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

    private void SpawnZEF()
    {
        Vector2 spawnPosition;

        // Asegurar que la nueva posición no sea la misma que la anterior
        do
        {
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            spawnPosition = new Vector2(randomX, randomY);
        } while (spawnPosition == lastSpawnPosition);

        lastSpawnPosition = spawnPosition; // Guardamos la última posición
        Instantiate(ZonaEfectoFuego, spawnPosition, Quaternion.identity);
    }

    private void SpawnZEH()
    {
        Vector2 spawnPosition;

        // Asegurar que la nueva posición no sea la misma que la anterior
        do
        {
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            spawnPosition = new Vector2(randomX, randomY);
        } while (spawnPosition == lastSpawnPosition);

        lastSpawnPosition = spawnPosition; // Guardamos la última posición
        Instantiate(ZonaEfectoHielo, spawnPosition, Quaternion.identity);
    }

    private void SpawnZEV()
    {
        Vector2 spawnPosition;

        // Asegurar que la nueva posición no sea la misma que la anterior
        do
        {
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            spawnPosition = new Vector2(randomX, randomY);
        } while (spawnPosition == lastSpawnPosition);

        lastSpawnPosition = spawnPosition; // Guardamos la última posición
        Instantiate(ZonaEfectoVeneno, spawnPosition, Quaternion.identity);
    }

    private void SpawnFamiliar()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(familiar, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Familiar generado: " + familiar.name);
    }
}