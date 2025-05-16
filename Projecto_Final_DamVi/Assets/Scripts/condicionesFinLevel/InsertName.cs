using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Solo si usas TMP_InputField; si usas InputField normal elimina esta línea

public class InsertName : MonoBehaviour
{
    [Tooltip("InputField donde el usuario introduce texto")]
    public TMP_InputField inputField; // Cambia a InputField si no usas TextMeshPro

    [Tooltip("Botón que se activa si el texto no está vacío")]
    public Button proceedButton;

    [Tooltip("Nombre exacto de la escena que se va a cargar")]
    public string sceneName;

    void Start()
    {
        if (inputField == null)
        {
            Debug.LogError("InputField no asignado en InsertName.");
            return;
        }
        if (proceedButton == null)
        {
            Debug.LogError("Botón no asignado en InsertName.");
            return;
        }

        // Inicializa botón desactivado
        proceedButton.interactable = false;

        // Añade listener para activar o desactivar botón según texto
        inputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    // Se llama cada vez que cambia el texto del InputField
    private void OnInputValueChanged(string text)
    {
        // Activa el botón si hay texto (no vacío ni solo espacios)
        proceedButton.interactable = !string.IsNullOrWhiteSpace(text);
    }

    // Método público para asignar al evento OnClick del botón en Inspector
    public void ProceedToScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("El nombre de la escena no está asignado en InsertName.");
            return;
        }
        // Verifica que la escena está en Build Settings para evitar errores
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"La escena '{sceneName}' no está agregada en Build Settings o el nombre es incorrecto.");
            return;
        }

        // ✅ INTEGRACIÓ: Assigna el nom a JsonExporter si existeix
        JsonExporter exporter = FindObjectOfType<JsonExporter>();
        if (exporter != null)
        {
            exporter.username = inputField.text;
        }

        // Carga la escena asignada
        SceneManager.LoadScene(sceneName);
    }
}

