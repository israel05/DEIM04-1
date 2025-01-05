using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinDeJuego : MonoBehaviour
{

    [SerializeField] AudioClip musicaFinal; // La música de final de juego
    [SerializeField] GameObject canvasJuego; // El canvas de juego
    [SerializeField] Animator animacionFinalJuego; // La animación que se va a lanzar
                                                   //  [SerializeField] float tiempoEsperaAntesDeMenuInicio = 2f; // Tiempo de espera antes de volver al menú principal
    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Acabo de chocarme, lanzando los pasos del final");
        // esta es la función de fin de juego        // que se escuche la musica de final de juego      // Reproducir música de final de juego
        AudioSource.PlayClipAtPoint(musicaFinal, transform.position);
        // qeu se active el canvan de juego          // Activar el canvas de juego
        canvasJuego.SetActive(true);
        // que se lance al animación
        GetComponent<Animator>().enabled = true;
        // Lanzar la animación            // que se desactiven los controles      // que se espere un rato   // Invoke("CargarMenuPrincipal", tiempoEsperaAntesDeMenuInicio);
        // que nos lleve al menú ppal             
    }
   

}
