
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
        animator = GetComponent<Animator>();
        animator.SetFloat("Velocidad", 0f);
      
        controlGatetePersonalizado.JugadorGatete.Moverse.performed += Muevete;
        controlGatetePersonalizado.JugadorGatete.Moverse.canceled += Deteniendo;
        controlGatetePersonalizado.JugadorGatete.Usar.performed += Usar;

        miraALaDerecha = true;
    }

    private void Deteniendo(InputAction.CallbackContext context)
    {
        rbJugador.drag = dragPersonaje;
        pasos.Stop();


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
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("COLISIONO CON : "+collision.gameObject.name);

        if (collision.gameObject.tag == "Sierra")
        {
            print("HAS MUERTO");
           // Invoke("Muerto", 4f);
        }

        if (collision.gameObject.tag == "cofre")
        {
            print("ES UN COFRE");
        }

        if(collision.gameObject.tag == "Meta")
        {
            // Invoke("CargarNivelFinal", 4f);

            print("ES UNA META");
        }
    }

    private void Muerto()
    {
       // GameManager.Destroy(this);
    }

   
}
