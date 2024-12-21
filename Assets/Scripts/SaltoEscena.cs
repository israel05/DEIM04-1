using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaltoEscena : MonoBehaviour
{
    public void CargarNivelFinal()
    {
        Debug.Log("Cargando pantalla final");
        SceneManager.LoadScene("EscenaFinal");
    }
}
