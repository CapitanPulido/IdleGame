using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Mejoras : MonoBehaviour
{
    public GameObject ElegirMejora;
    public Torreta torreta;
    public VidaTorreta VT;
    public ZEF zef;
    public ZEH zeh;
    public ZEV zev;

    public Escudo escudo;
    public FireBall fireball;

    public Button[] botones;
    //public RawImage[] imagenes;
    public TextMeshProUGUI[] textos;

    
    private List<string> nombres = new List<string>();
    public Transform[] spawnPoints;
    public GameObject familiar;

    public List<System.Action> acciones = new List<System.Action>();
    public List<string> mejorasDesbloqueadas = new List<string>();
    public List<string> accionesList = new List<string>();

    [Header("Cooldown Config")]
    public float cooldownMax = 10f; // Tiempo inicial entre spawns
    public float minCooldown = 2f; // Límite mínimo del cooldown
    public float EscudoCooldown;

    public void Awake()
    {
        // Configurar mejoras desbloqueadas inicialmente
        mejorasDesbloqueadas.Add("ZEF");
        mejorasDesbloqueadas.Add("ZEH");
        mejorasDesbloqueadas.Add("ZEV");
        mejorasDesbloqueadas.Add("VidaExtra");
        mejorasDesbloqueadas.Add("Daño");
        mejorasDesbloqueadas.Add("Rango");
        mejorasDesbloqueadas.Add("Velocidad");
        mejorasDesbloqueadas.Add("Vida");
        mejorasDesbloqueadas.Add("Familiar");
        mejorasDesbloqueadas.Add("ActiveEscudo");
        mejorasDesbloqueadas.Add("FireBall");
    }
    void Start()
    {
        torreta = GameObject.FindGameObjectWithTag("Player").GetComponent<Torreta>();
        escudo = GameObject.FindGameObjectWithTag("Escudo").GetComponent<Escudo>();
        
        zef = GameObject.FindGameObjectWithTag("ZEF").GetComponent<ZEF>();
        zeh = GameObject.FindGameObjectWithTag("ZEH").GetComponent<ZEH>();
        zev = GameObject.FindGameObjectWithTag("ZEV").GetComponent<ZEV>();

    }

    void Update()
    {
        EscudoCooldown = EscudoCooldown + Time.deltaTime;
        
        
        if (EscudoCooldown >= cooldownMax)
        {
            escudo.ReActivar();
            EscudoCooldown = minCooldown;

        }

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

        mejorasDesbloqueadas.Remove("VidaExtra");

    }

    public void ActiveEscudo()
    {
        escudo.Activar();
        VT.ActiveObtXp();
        ElegirMejora.SetActive(false);

        mejorasDesbloqueadas.Add("CooldownEscudo");
        mejorasDesbloqueadas.Add("AumentarVidaEscudo");
    }

    public void CooldownEscudo()
    {
        ReducirCooldown(0.25f);
        VT.ActiveObtXp();
        ElegirMejora.SetActive(false);
    }

    public void AumentarVidaEscudo()
    {
        escudo.AumentarVida(0.25f);
        VT.ActiveObtXp();
        ElegirMejora.SetActive(false);

    }

    public void ZEF()
    {
        
            zef.Active();
            ElegirMejora.SetActive(false);
            VT.ActiveObtXp();

            // Eliminar ZEF de la lista de mejoras disponibles
            mejorasDesbloqueadas.Remove("ZEF");

            // Desbloquear las mejoras ZEFMT y ZEFMC
            mejorasDesbloqueadas.Add("ZEFMT");
            mejorasDesbloqueadas.Add("ZEFMC");
        
    }

    public void ZEFMT()
    {
        zef.AumentarTamaño(1.10f);
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();

    }
    public void ZEFMC()
    {
        zef.ReducirCooldown(0.10f);
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();

    }
    public void ZEH()
    {
       
            zeh.Active();
            ElegirMejora.SetActive(false);
            VT.ActiveObtXp();

            // Eliminar ZEH de la lista de mejoras disponibles
            mejorasDesbloqueadas.Remove("ZEH");

            // Desbloquear las mejoras ZEHMT y ZEHMC
            mejorasDesbloqueadas.Add("ZEHMT");
            mejorasDesbloqueadas.Add("ZEHMC");
        
    }
    public void ZEHMT()
    {
        zeh.AumentarTamaño(1.10f);
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

            // Eliminar ZEV de la lista de mejoras disponibles
            mejorasDesbloqueadas.Remove("ZEV");

            // Desbloquear las mejoras ZEVMT y ZEVMC
            mejorasDesbloqueadas.Add("ZEVMT");
            mejorasDesbloqueadas.Add("ZEVMC");
        
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

    public void FireBall()
    {
        torreta.ActivarFireball();
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();

        mejorasDesbloqueadas.Remove("FireBall");
        mejorasDesbloqueadas.Add("FireballT");
    }

    public void  TamañoFireball()
    {
        fireball.Escala();
        ElegirMejora.SetActive(false);
        VT.ActiveObtXp();
    }

    public void AgregarAcciones()
    {

        acciones.Clear();
        nombres.Clear();
        //imagenesAcciones.Clear();



        if (mejorasDesbloqueadas.Contains("Velocidad"))
        {
            acciones.Add(Velocidad);
            nombres.Add("Velocidad");
            accionesList.Add("Velocidad");
        }

        if (mejorasDesbloqueadas.Contains("Vida"))
        {
            acciones.Add(Vida);
            nombres.Add("Vida");
        }

        if (mejorasDesbloqueadas.Contains("Daño"))
        {
            acciones.Add(Daño);
            nombres.Add("Daño");
        }

        if (mejorasDesbloqueadas.Contains("Rango"))
        {
            acciones.Add(Rango);
            nombres.Add("Rango");
        }

        if (mejorasDesbloqueadas.Contains("ActiveEscudo"))
        {
            acciones.Add(ActiveEscudo);
            nombres.Add("Escudo");
        }

        if (mejorasDesbloqueadas.Contains("CooldownEscudo"))
        {
            acciones.Add(CooldownEscudo);
            nombres.Add("EscudoCooldown");
        }

        if (mejorasDesbloqueadas.Contains("AumentarVidaEscudo"))
        {
            acciones.Add(AumentarVidaEscudo);
            nombres.Add("VidaEscudo");
        }

        if (mejorasDesbloqueadas.Contains("ZEF"))
        {
            acciones.Add(ZEF);
            nombres.Add("ZEF");
        }

        if (mejorasDesbloqueadas.Contains("ZEH"))
        {
            acciones.Add(ZEH);
            nombres.Add("ZEH");
        }

        if (mejorasDesbloqueadas.Contains("ZEV"))
        {
            acciones.Add(ZEV);
            nombres.Add("ZEV");
        }

        if (mejorasDesbloqueadas.Contains("ZEFMT"))
        {
            acciones.Add(ZEFMT);
            nombres.Add("ZEFMT");
        }

        if (mejorasDesbloqueadas.Contains("ZEHMT"))
        {
            acciones.Add(ZEHMT);
            nombres.Add("ZEHMT");
        }

        if (mejorasDesbloqueadas.Contains("ZEVMT"))
        {
            acciones.Add(ZEVMT);
            nombres.Add("ZEVMT");
        }

        if (mejorasDesbloqueadas.Contains("ZEFMC"))
        {
            acciones.Add(ZEFMC);
            nombres.Add("ZEFMC");
        }

        if (mejorasDesbloqueadas.Contains("ZEHMC"))
        {
            acciones.Add(ZEHMC);
            nombres.Add("ZEHMC");
        }

        if (mejorasDesbloqueadas.Contains("ZEVMC"))
        {
            acciones.Add(ZEVMC);
            nombres.Add("ZEVMC");
        }

        if (mejorasDesbloqueadas.Contains("VidaExtra"))
        {
            acciones.Add(VidaExtra);
            nombres.Add("VidaExtra");
        }

        if (mejorasDesbloqueadas.Contains("Familiar"))
        {
            acciones.Add(Familiar);
            nombres.Add("Familiar");
        }

        if (mejorasDesbloqueadas.Contains("FireballT"))
        {
            acciones.Add(TamañoFireball);
            nombres.Add("FireBallT");
        }



        // Crear una lista de índices que se seleccionarán aleatoriamente
        List<int> indicesDisponibles = new List<int>();
        for (int i = 0; i < acciones.Count; i++)
        {
            indicesDisponibles.Add(i);
        }

        // Seleccionar 3 índices aleatorios sin repetir
        List<int> indicesSeleccionados = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            if (indicesDisponibles.Count > 0)
            {
                int indiceAleatorio = Random.Range(0, indicesDisponibles.Count);
                indicesSeleccionados.Add(indicesDisponibles[indiceAleatorio]);
                indicesDisponibles.RemoveAt(indiceAleatorio); // Eliminar el índice seleccionado
            }
        }

        // Asignar las acciones y textos a los botones
        for (int i = 0; i < botones.Length; i++)
        {
            if (i < indicesSeleccionados.Count)
            {
                int indice = indicesSeleccionados[i];

                // Eliminar listeners previos para evitar acumulación
                botones[i].onClick.RemoveAllListeners();

                // Asignar la acción al botón
                int indiceCopia = indice; // Necesario para evitar problemas con el closure en la lambda
                botones[i].onClick.AddListener(() => acciones[indiceCopia].Invoke());

                // Asignar texto
                textos[i].text = nombres[indiceCopia];
            }
            else
            {
                // Si hay menos de 3 acciones, desactivar los botones restantes
                botones[i].gameObject.SetActive(false);
            }
        } 
    }

    public void ReducirCooldown(float porcentaje)
    {
        cooldownMax *= porcentaje;
        cooldownMax = Mathf.Max(cooldownMax, minCooldown);
    }
    public void SpawnFamiliar()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(familiar, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Familiar generado: " + familiar.name);
    }
}