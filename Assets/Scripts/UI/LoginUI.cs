using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using SQLite4Unity3d;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField dniInput;
    public Button btnEntrar;
    public TextMeshProUGUI feedbackText;

    public GameObject panelRegistro;


    void Start()
    {
        DBManager.Init();  // Importante: asegura conexión a SQLite
        btnEntrar.onClick.AddListener(OnEntrarClick);
    }

    void OnEntrarClick()
    {
        string dni = dniInput.text.Trim();

        if (string.IsNullOrEmpty(dni))
        {
            feedbackText.text = "Por favor, introduce tu DNI.";
            return;
        }

        if (DBManager.ExisteUsuario(dni))
        {
            Debug.Log("DNI válido. Cargando pantalla principal...");

            // Guardar el usuario actual (opcional para mostrar su nombre luego)
            PlayerPrefs.SetString("dni", dni);
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            feedbackText.text = "DNI no registrado.";
        }
    }

    public void MostrarPanelRegistro()
    {
        SceneManager.LoadScene(1);
    }

    



}

