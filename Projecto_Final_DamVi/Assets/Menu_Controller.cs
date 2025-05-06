using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
   
    public void Jugar()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Inf()
    {
        SceneManager.LoadScene("NivelInfinito");
    }


    public void Salir()
    {
           Application.Quit();
    }

}
