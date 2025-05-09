using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionManager : MonoBehaviour
{
    private Animator transicion;  // El Animator que controla el fade (fundido)
         // El nombre de la escena a cargar

    // Este método se llama cuando el botón es presionado
    public void OnButtonPressed()
    {
        transicion.SetTrigger("FundidoAnegro");  // Activamos el trigger que hace el fundido
    }

    // Este método es llamado desde el evento de la animación al finalizar
    public void OnFadeComplete()
    {
        SceneManager.LoadScene("EscenaPrueba2");  // Cargamos la nueva escena
    }
}

