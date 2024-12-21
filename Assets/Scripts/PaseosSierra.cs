using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PaseosSierra : MonoBehaviour
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

    Rigidbody2D rbSierra;
    Vector2 direccion2D;



    private void Awake()
    {
        rbSierra = GetComponent<Rigidbody2D>();
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
        //en vez de usar un anim usaremos un transfor
        transform.Rotate(0, 0, 340);


        if (movimientoHorizontal)
        {
            if(rbSierra.position.x > xLimits[1])
            {
                direccion2D = new Vector2(-1f, 0f);
            }
            if(rbSierra.position.x < xLimits[0])
            {
                direccion2D = new Vector2(1f, 0f);
            }
        } else
        {
            if (rbSierra.position.y > yLimits[1])
            {
                direccion2D = new Vector2(0f, -1f);
            }
            if (rbSierra.position.y < yLimits[0])
            {
                direccion2D = new Vector2(0f, 1f);
            }
        }
        rbSierra.MovePosition(rbSierra.position + speed * Time.deltaTime * direccion2D);
    }
}
