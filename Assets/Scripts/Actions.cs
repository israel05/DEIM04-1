
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;


public class Actions : MonoBehaviour
{
    Animator animator;
    public float velocidad = 0f;
    Rigidbody2D rbJugador;
    SpriteRenderer srJugador;
    bool miraALaDerecha = true;
   



     ControlGatete controlGatetePersonalizado;

    [SerializeField] private AudioSource pasos;
    [SerializeField] private float velocidadMovimiento = 4f;
    [SerializeField] private float dragPersonaje = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rbJugador = GetComponent<Rigidbody2D>();
        srJugador = GetComponent<SpriteRenderer>();
        pasos = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        animator.SetFloat("Velocidad_goblin", Mathf.Max(Mathf.Abs(rbJugador.velocity.x), Mathf.Abs(rbJugador.velocity.y)));
        rbJugador.drag = dragPersonaje;
    }


    void Awake()
    {

        pasos.Stop();
        controlGatetePersonalizado = new ControlGatete();
        controlGatetePersonalizado.JugadorGatete.Enable();
        //asocio el animador
        animator = GetComponent<Animator>();
        //aparece al principio quieto
        animator.SetFloat("Velocidad", 0f);
      
        controlGatetePersonalizado.JugadorGatete.Moverse.performed += Muevete;
        controlGatetePersonalizado.JugadorGatete.Moverse.canceled += Deteniendo;
        controlGatetePersonalizado.JugadorGatete.Usar.performed += Usar;

        miraALaDerecha = true;
    }

    private void Deteniendo(InputAction.CallbackContext context)
    {
        if (rbJugador!= null) //podría darse el caso de que se hubiera destruido el jugador
        {
            rbJugador.drag = dragPersonaje;
            pasos.Stop();
        }   
    }


    void Muevete(InputAction.CallbackContext ctx)
    {
        pasos.Play();
        Vector2 input = ctx.ReadValue<Vector2>();        
        float movementSpeed = velocidadMovimiento;        
        if (input.magnitude > 1)
        {
            input.Normalize(); // Normalize if the input exceeds length 1 to prevent faster movement on diagonal
        }
        // Set the player's velocity with the input
        rbJugador.velocity = new Vector2(input.x * movementSpeed, input.y * movementSpeed);      
        rbJugador.drag = dragPersonaje - dragPersonaje; // cero

        // Handle character flip (left/right)
        if (input.x < 0 && miraALaDerecha)
        {
            srJugador.flipX = true;
            miraALaDerecha = false;
        }
        if (input.x > 0 && !miraALaDerecha)
        {
            srJugador.flipX = false;
            miraALaDerecha = true;
        }
    }



    void Usar(InputAction.CallbackContext ctx)
    {
        // se ha pulsado la F
        print("USo algo");
        // si tengo cinco vidas
        // si estoy en la meta

        if (Singleton.Instance.TocandoMeta && Singleton.Instance.PuntosDeJugador >= 5) //si estoy tocando y tengo 5 puntos
        {
            print("TELEPORT");
            Singleton.Instance.SaltoDeNivel();
        } else
        {
            print("NO PUEDES TELEPORTAR");
            print("estas tocando meta?" + Singleton.Instance.TocandoMeta.ToString());
            print("tienes suficientes tesoros?" + Singleton.Instance.PuntosDeJugador.ToString());
        }

       



    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("COLISIONO CON : "+collision.gameObject.name);

        if (collision.gameObject.tag == "Sierra")
        {
            
           // Invoke("Muerto", 4f);
        }
        if (collision.gameObject.tag == "Zombie")
        {
          
            // Invoke("Muerto", 4f);
        }


        if (collision.gameObject.tag == "cofre")
        {
           
        }
        // si hubiera sido un collider, pero es un trigger
       /*
        if(collision.gameObject.tag == "Meta")
        {
            tocandoMeta = true;
            print("Colision con Meta");
        } else
        {
            tocandoMeta = false;
        }
       */
    }

  
   
}
