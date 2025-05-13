using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialog_3 : MonoBehaviour
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
        "Has conseguido diezmar las fuerzas enemigas hasta el punto en el que se ven obligadas a retirarse. Pero, acto seguido, un enorme avion de combate aparece. En esa maquina de guerra esta el general enemigo, buscando tu cabeza, y comienza un epico combate aereo en el que despues de lo que se sienten horas, acabas victorioso.",
        "-Soldado raso: Teniente Rocket! Se ha detectado un avion que se dirige al buque segun inteligencia es el General Khalid.\r\n\r\n-Teniente: Este ataque a la desesperada no le saldra segun lo previsto yo se lo impedire!\r\n\r\n-Soldado raso: Deja que llame refuerzos para ayudarle!\r\n",
        "-Teniente: No! De esto me he de encargar yo!\r\n\r\n-Comunicador: Aqui el General Khalid! Rocket se que estas alli ven a enfrentarte a mi si te atreves!! \r\n\r\n-Teniente: Yo de ti no me confiaria general, estas perdido!\r\n",
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
