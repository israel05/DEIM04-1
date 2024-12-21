using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Recolectable : MonoBehaviour
{

    [SerializeField] AudioClip soniditoDeRecogidaDeObjeto; // La música de moneda recogida


    [SerializeField] TMP_Text puntuacionTMP;// el canvas del marcador

    [SerializeField] int puntuacionNumerica = 10; //el valor del item recogido

    

    // Start is called before the first frame update
    void Start()
    {
        
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Animator>().Play("palpito");
        Debug.Log("trigger enter 2d");
        Debug.Log("trigger enter 2d");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        Debug.Log("trigger exit 2d");
        GetComponent<Animator>().StopPlayback();
    }




    void OnCollisionEnter2D(Collision2D objetoColisionado)
    {
       

        AudioSource.PlayClipAtPoint(soniditoDeRecogidaDeObjeto, transform.position);
        Debug.Log("Te has chocado conmigo");
       float resultadoAcumulador = puntuacionNumerica + float.Parse(puntuacionTMP.text);
       puntuacionTMP.text = resultadoAcumulador.ToString();


        // al contador de células, uno más
        // yo soy destruido
        if (objetoColisionado.gameObject != gameObject)
        {
            // Destroy the game object
            Destroy(gameObject);
        }

    }



}
