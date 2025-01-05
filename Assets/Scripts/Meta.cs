using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meta : MonoBehaviour
{

  
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Animator>().Play("aparece");      
        print("CERCA DE LA META");
        Singleton.Instance.TocandoMeta = true;
    }

    private void OnTriggerExit(Collider other)
    {
        print("SALE  DE LA META");
        Singleton.Instance.TocandoMeta = false;
    }


}
