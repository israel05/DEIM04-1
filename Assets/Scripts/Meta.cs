using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meta : MonoBehaviour
{

    [SerializeField]
    GameObject imagenSaltoNivel;

    private void Awake()
    {
        //imagenSaltoNivel = GetComponent<GameObject>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Animator>().Play("aparece");      
      imagenSaltoNivel.SetActive(true);
    }
}
