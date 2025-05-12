using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
   
    public void Jugar()
    {
        SceneManager.LoadScene("InsertName_Histori");
    }

    public void Inf()
    {
        SceneManager.LoadScene("InsertName_Infiniti");
    }


    public void Salir()
    {
           Application.Quit();
    }

    public void Volver()
    {
        SceneManager.LoadScene("Menus");
    }

    public void Scores()
    {
        SceneManager.LoadScene("HighScores");
    }
}
