using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialog_Inicial : MonoBehaviour
{
    public TMP_Text dialogText;       // Referencia al componente Text de la UI donde aparecerá el diálogo
    public Button nextButton;         // Referencia al botón de la UI que avanza el diálogo
    public TMP_Text nextButtonText;   // Referencia al texto del botón para cambiar su texto dinámicamente
    public string nextSceneName;      // Nombre de la escena a cargar al finalizar el diálogo
    public float typingSpeed = 0.05f; // Velocidad de escritura

    private Queue<string> sentences; // Cola para almacenar las oraciones del diálogo
    private bool isTyping = false;   // Indica si el texto se está escribiendo
    private string currentSentence;   // Oración actual que se está mostrando
    private bool dialogueEnded = false; // Indica si el diálogo ha terminado

    // Ejemplo de oraciones del diálogo
    private string[] dialogSentences = new string[]
    {
        "Es el anio 1942, eres el Teniente Rocket, un miembro de la fuerza aerea de Corus, un pais que lleva en guerra contra Palpe tantos anios que se ha perdido la cuenta. Una vez mas, te mandan de mision para acabar con los soldados que se avecinan a vuestra base donde todos tus camaradas estan heridos. Eres el unico que puede pilotar un avión de combate actualmente, por lo que es tu deber como teniente mantener a raya a esos bastardos.\r\n",
        "-Soldado raso: Teniente Rocket!\r\n\r\n-Teniente: Que pasa? A que se debe tanta exaltacion?\r\n\r\n-Soldado raso: Se han detectado multiples amenazas por el radar! Son aviones de Palpe, y vienen directos a base!\r\n\r\n-Teniente: Cuantos soldados quedan capacitados?\r\n\r\n-Soldado raso: Practicamente ninguno sr…",
        "-Teniente:Mierda…! Bien, intentar mantener la base operativa, yo intentare retenerlos el máximo tiempo posible…\r\n\r\n-Soldado raso: Pero son demasiados! Esta usted seguro?\r\n\r\n-Teniente: Es la unica opcion que nos queda soldado.\r\n\r\n-Soldado raso: De acuerdo, buena suerte teniente.\r\n",
        "Buena suerte!"
    };

    void Start()
    {
        sentences = new Queue<string>(dialogSentences);

        if (nextButton != null)
        {
            nextButton.onClick.AddListener(OnNextButtonPressed);
            if (nextButtonText == null)
            {
                // Try to find TMP_Text in button children automatically if not assigned
                nextButtonText = nextButton.GetComponentInChildren<TMP_Text>();
            }
        }

        StartDialog();
    }

    void OnDestroy()
    {
        if (nextButton != null)
            nextButton.onClick.RemoveListener(OnNextButtonPressed);
    }

    public void StartDialog()
    {
        dialogueEnded = false;

        // Ensures that the button's text is not changed during the dialogue
        if (nextButtonText != null)
        {
            nextButtonText.text = nextButtonText.text; // Do not modify the button text
        }

        sentences.Clear();
        foreach (string sentence in dialogSentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        currentSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    public void OnNextButtonPressed()
    {
        if (dialogueEnded)
        {
            // Cuando el diálogo ha terminado, ejecutamos el fade out y luego cargamos la siguiente escena
            if (SceneTransitionManager.Instance != null && !string.IsNullOrEmpty(nextSceneName))
            {
                StartCoroutine(TransitionToSceneWithFade());
            }
            else
            {
                Debug.LogWarning("SceneTransitionManager not set or nextSceneName is missing!");
            }
            return;
        }

        if (isTyping)
        {
            // Terminar el texto actual inmediatamente
            StopAllCoroutines();
            dialogText.text = currentSentence;
            isTyping = false;
        }
        else
        {
            DisplayNextSentence();
        }
    }

    private void EndDialog()
    {
        dialogueEnded = true;
    }

    // Corutina para hacer el fade out antes de cargar la siguiente escena
    IEnumerator TransitionToSceneWithFade()
    {
        // Llamamos al fade out del SceneTransitionManager
        yield return StartCoroutine(SceneTransitionManager.Instance.FadeToBlack());

        // Ahora cargamos la nueva escena
        SceneManager.LoadScene(nextSceneName);
    }
}
