using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SQLite4Unity3d;


public class RegisterUI : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public TMP_InputField inputApellidos;
    public TMP_InputField inputDni;

    public Button btnRegistrarse;
    public Button btnVolver;

    void Start()
    {
        btnRegistrarse.onClick.AddListener(OnRegistrarseClick);
        btnVolver.onClick.AddListener(OnVolverClick);

         DBManager.Init();
    }

    void OnRegistrarseClick()
    {
        string nombre = inputNombre.text.Trim();
        string apellidos = inputApellidos.text.Trim();
        string dni = inputDni.text.Trim();

        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos) || string.IsNullOrEmpty(dni))
        {
            Debug.LogWarning("Todos los campos son obligatorios.");
            return;
        }

        Debug.Log($"Registrando: {nombre} {apellidos}, DNI: {dni}");

        Usuario nuevo = new Usuario(dni, nombre, apellidos);
        DBManager.RegistrarUsuario(nuevo);
    

    }

    void OnVolverClick()
    {
        // Si la escena de login se llama "LoginScene":
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoginScene");
    }
}
