using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Recolectable : MonoBehaviour
{

    [SerializeField] AudioClip soniditoDeRecogidaDeObjeto; // La música de moneda recogida


    

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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        Debug.Log("trigger exit 2d");
        GetComponent<Animator>().StopPlayback();
    }




    void OnCollisionEnter2D(Collision2D objetoColisionado)
    {      

        AudioSource.PlayClipAtPoint(soniditoDeRecogidaDeObjeto, transform.position);
        Debug.Log("Te has chocado con un cofre");
        // notifica en el gameManager que se ha recogido un objeto
        Singleton.Instance.SumaPuntos();
        // yo soy destruido
        if (objetoColisionado.gameObject != gameObject)
        {
            // Destroy the game object
            Destroy(gameObject);
        }

    }



}
