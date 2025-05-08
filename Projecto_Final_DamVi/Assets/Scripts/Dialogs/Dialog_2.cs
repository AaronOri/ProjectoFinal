using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialog_2 : MonoBehaviour
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
        "Han pasado dias desde el ultimo ataque de las tropas enemigas que lograste defender en solitario. Ahora, es el momento de contraatacar, por lo que alzas vuelo con tu escuadron directo a acabar con la base Palpeniana. En el camino, tu escuadrón es eliminado por torretas anti-aereas, por lo que una vez mas te toca aventurarte solo contra las fuerzas enemigas.",
        "-Teniente: Soldados, ha llegado el dia vamos a entregar cuerpo y alma por acabar con esta guerra. Ya hemos sufrido lo suficiente, por lo que es el momento de devolverles la miseria que nos han traído a nosotros y nuestra familia! \r\n\r\n-Grupo soldados: Si!\r\n\r\n-Teniente: Estais listos para morir? No solo por vuestro pais, Vais a morir si hace falta por un futuro mejor para vuestros seres queridos! Quién va a ser ese heroe!?\r\n",
        "-Grupo soldados: Nosotros sr!\r\n\r\n-Teniente: Pues que el escuadron Tucan Vulkan alce el vuelo! Sera nuestro ultimo viaje al infierno seniores!\r\n",
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
