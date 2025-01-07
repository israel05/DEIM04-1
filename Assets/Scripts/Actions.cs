using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Actions : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rbJugador;
    SpriteRenderer srJugador;
    bool miraALaDerecha = true;
    ControlGatete controlGatetePersonalizado;

    [SerializeField] private AudioSource pasos;
    [SerializeField] private float velocidadMovimiento = 4f;
    [SerializeField] private float dragPersonaje = 4f;

    // Start is called before the first frame update
    void Start()
    {
        rbJugador = GetComponent<Rigidbody2D>();
        srJugador = GetComponent<SpriteRenderer>();
        pasos = GetComponent<AudioSource>();
        pasos.Stop();
    }

    private void FixedUpdate()
    {
        // Esto asegura que el valor de la velocidad se actualice en cada frame.
        Vector2 input = controlGatetePersonalizado.JugadorGatete.Moverse.ReadValue<Vector2>(); // Se lee la entrada en cada frame.
        animator.SetFloat("Velocidad_goblin", Mathf.Max(Mathf.Abs(rbJugador.velocity.x), Mathf.Abs(rbJugador.velocity.y)));
        rbJugador.drag = dragPersonaje;

        if (input.magnitude > 1)
        {
            input.Normalize(); // Normaliza para evitar movimientos más rápidos en diagonal
        }

        // Movimiento continuo
        rbJugador.velocity = new Vector2(input.x * velocidadMovimiento, input.y * velocidadMovimiento);

        // Reproducir el sonido de pasos solo si el jugador está moviéndose
        if (input.magnitude > 0.1f && !pasos.isPlaying)
        {
            pasos.Play();
        }
        else if (input.magnitude <= 0.1f && pasos.isPlaying)
        {
            pasos.Stop();
        }

        // Manejar el giro del personaje
        if (input.x < 0 && miraALaDerecha)
        {
            srJugador.flipX = true;
            miraALaDerecha = false;
        }
        else if (input.x > 0 && !miraALaDerecha)
        {
            srJugador.flipX = false;
            miraALaDerecha = true;
        }
    }

    void Awake()
    {
        controlGatetePersonalizado = new ControlGatete();
        controlGatetePersonalizado.JugadorGatete.Enable();
        // Asocia el animador
        animator = GetComponent<Animator>();
        // Inicializa el animador en reposo al principio
        animator.SetFloat("Velocidad_goblin", 0f);

        controlGatetePersonalizado.JugadorGatete.Usar.performed += Usar;
    }

    void Usar(InputAction.CallbackContext ctx)
    {
        // Código para cuando se usa la tecla de acción (por ejemplo, la tecla 'F')
        print("USo algo");
        if (Singleton.Instance.TocandoMeta && Singleton.Instance.PuntosDeJugador >= 5)
        {
            print("TELEPORT");
            Singleton.Instance.SaltoDeNivel();
        }
        else
        {
            print("NO PUEDES TELEPORTAR");
            print("estas tocando meta?" + Singleton.Instance.TocandoMeta.ToString());
            print("tienes suficientes tesoros?" + Singleton.Instance.PuntosDeJugador.ToString());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Manejo de colisiones (puedes agregar más lógica aquí)
        print("COLISIONO CON : " + collision.gameObject.name);
    }
}
