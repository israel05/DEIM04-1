using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PaseosZombie : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float[] xLimits;

    [SerializeField]
    float[] yLimits;

    [SerializeField]
    bool movimientoHorizontal;



    public float Speed { get; set;}
    public float[] XLimits {get; set;}
    public float[] YLimits {get; set;}
    public bool MovimientoHorizontal { get; set; }

    Rigidbody2D rbZombie;
    SpriteRenderer srZombie;
    Vector2 direccion2D;



    private void Awake()
    {
        srZombie = GetComponent<SpriteRenderer>();  
        rbZombie = GetComponent<Rigidbody2D>();
        if (movimientoHorizontal)
        {
            direccion2D = new Vector2(1f, 0f);
           
        }
        else
        {
            direccion2D = new Vector2(0f, 1f);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (movimientoHorizontal)
        {
            if(rbZombie.position.x > xLimits[1])
            {
                direccion2D = new Vector2(-1f, 0f);
                srZombie.flipX = true;
            }
            if(rbZombie.position.x < xLimits[0])
            {
                direccion2D = new Vector2(1f, 0f);
                srZombie.flipX = false;
            }
        } else
        {
            // en el movimiento vertical lo mejor para saber a dónde mira sería un raycast
            if (rbZombie.position.y > yLimits[1])
            {
                direccion2D = new Vector2(0f, -1f);
            }
            if (rbZombie.position.y < yLimits[0])
            {
                direccion2D = new Vector2(0f, 1f);
            }
        }
        rbZombie.MovePosition(rbZombie.position + speed * Time.deltaTime * direccion2D);

        Animador();
    }
    void Animador()
    {
            //se esta moviendo
            GetComponent<Animator>().SetFloat("Velocidad", 3);      

    }


    /**
     * Este método se llama cuando el objeto entra en colisión con otro objeto, en al tocar el zombie con el jugador
     * retara una vida, en el GAmeManager si la vida es cero disparara fin de juego
     */
    void OnCollisionEnter2D(Collision2D objetoColisionado)
    {

       // AudioSource.PlayClipAtPoint(soniditoDeRecogidaDeObjeto, transform.position);
        Debug.Log("Te has chocado con un zombie");
        // notifica en el gameManager que se ha recogido un objeto
        Singleton.Instance.RestaVidas();
        // yo soy destruido
        //  if (objetoColisionado.gameObject != gameObject)
        //  {
        //   // Destroy the game object
        //    Destroy(gameObject);
        //  }

    }
}
