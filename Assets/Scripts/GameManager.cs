using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{

    private float puntosDeJugador = 0;
    private float vidasDelJugador = 5;
    public bool gameOver = false;
    private bool tocandoMeta = false;

    [SerializeField] TMP_Text puntuacionTMP;// el canvas del marcador
    [SerializeField] TMP_Text vidasTMP;// el canvas del marcador
    [SerializeField] GameObject jugador;// el propio jugador
    [SerializeField] GameObject canvasFinDeJuego; // El canvas de fin De Juego
    [SerializeField] AudioClip musicaGameOver; // La música de final de juego
    [SerializeField] GameObject imagenSaltoNivel;


    [SerializeField] GameObject barraDeVida; // El canvas de fin De Juego
    private RectTransform barraRectTransform;


    //esta clase es un singleton
    public static Singleton Instance { get; private set; }


    public float PuntosDeJugador { get => puntosDeJugador; }
    public bool TocandoMeta { get => tocandoMeta; set => tocandoMeta = value; }

    private void Awake()
    {

        barraRectTransform = barraDeVida.GetComponent<RectTransform>(); //me quedo con el tamaño original
       

        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        puntuacionTMP.text = puntosDeJugador.ToString();
        vidasTMP.text =  vidasDelJugador.ToString();
        jugador = GameObject.FindWithTag("Player");   


    }

 
 

    public void SumaPuntos()
    {
        puntosDeJugador++;  
        puntuacionTMP.text = puntosDeJugador.ToString();    
    }

    public void RestaVidas()
    {
        vidasDelJugador--;
        vidasTMP.text = vidasDelJugador.ToString();

        CambiarBarrraVidaTamanyo();

        if (vidasDelJugador == 0)
        {
            gameOver = true;
            Debug.Log("Game Over");
            Muerte();

        }
       
    }

    public void Muerte()
    {
        //que se desactive el jugador
        Destroy(jugador);
        //que se muestre una pantalla de gameOver
        canvasFinDeJuego.SetActive(true);
        // que se dispare la animación del canvan
        // que se ponga el menu ppal
        //que se ponga una musica de gameover
        AudioSource.PlayClipAtPoint(musicaGameOver, transform.position);
        // que se espere un rato
        Invoke("CargarMenuPrincipal", 4f); //cuatro y nos vamos
        // que nos lleve al menú ppal
    }


    private void CargarMenuPrincipal()
    {
        Debug.Log("Cargando menu ppal desde muerte");
        SceneManager.LoadScene("EscenaInicial");
    }

    public void SaltoDeNivel()
    {
        imagenSaltoNivel.SetActive(true);
        Invoke("CargarSiguienteNivel", 2f); //dos y nos vamos
    }


    private void CambiarBarrraVidaTamanyo()
    {
        float porcentajeVida = vidasDelJugador / 5f; //es un porcentaje sobre esas 5 vidas
        barraRectTransform.localScale = new Vector3(porcentajeVida, 1f, 1f);
    }
}
