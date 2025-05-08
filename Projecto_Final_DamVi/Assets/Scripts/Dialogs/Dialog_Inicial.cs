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
        "es el anio 1942, eres el Teniente Rocket, un miembro de la fuerza aerea de Corus, un pais que lleva en guerra contra Palpe tantos anios que se ha perdido la cuenta. Una vez mas, te mandan de mision para acabar con los soldados que se avecinan a vuestra base donde todos tus camaradas estan heridos. Eres el unico que puede pilotar un avión de combate actualmente, por lo que es tu deber como teniente mantener a raya a esos bastardos.\r\n",
        "-Soldado raso: Teniente Rocket!\r\n\r\n-Teniente: Que pasa? A que se debe tanta  exaltacion?\r\n\r\n-Soldado raso: Se han detectado multiples amenazas por el radar! Son aviones de Palpe, y vienen directos a base!\r\n\r\n-Teniente: Cuantos soldados quedan capacitados?\r\n\r\n-Soldado raso: Practicamente ninguno sr…",
        "-Teniente:Mierda…! Bien, intentar mantener la base operativa, yo intentare retenerlos el máximo tiempo posible…\r\n\r\n-Soldado raso: Pero son demasiados! Esta usted seguro?\r\n\r\n-Teniente: Es la unica opcion que nos queda soldado.\r\n\r\n-Soldado raso: De acuerdo, buena suerte teniente.\r\n",
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
        if (nextButtonText != null)
            nextButtonText.text = "Siguiente"; // Button text at start

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
            // Load the next scene when dialogue finished
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogWarning("Next scene name is not set!");
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
        if (nextButtonText != null)
        {
            nextButtonText.text = "Buena suerte!"; // Change button text to indicate continuing
        }
    }
}
