using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiaCamara : MonoBehaviour
{
    [SerializeField]
    GameObject camaraPerrete;
    [SerializeField]
    GameObject camaraMeta;

   public GameObject CamaraPerrete { get; set; }    
    public GameObject CamaraMeta { get; set; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        camaraMeta.SetActive(true);
        camaraPerrete.SetActive(false);
        StartCoroutine(VuelveACamaraPerrete());
    }


    IEnumerator VuelveACamaraPerrete()
    {
        yield return new  WaitForSeconds(6f);
        camaraPerrete.SetActive(true);
        camaraMeta.SetActive(false);
        
    }

}
