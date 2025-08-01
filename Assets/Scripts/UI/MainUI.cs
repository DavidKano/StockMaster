using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI textoUsuario;

    public GameObject panelEntradas;
    public GameObject panelSalidas;
    public GameObject panelStock;
    public GameObject panelHistorial;

    public Button btnEntradas;
    public Button btnSalidas;
    public Button btnStock;
    public Button btnHistorial;

    void Start()
    {
        // Cargar nombre desde DB según DNI guardado en PlayerPrefs
        string dniGuardado = PlayerPrefs.GetString("dni", "");
        Usuario usuario = DBManager.ObtenerUsuario(dniGuardado);

        if (usuario != null)
        {
            textoUsuario.text = "Bienvenido/a, " + usuario.nombre + " " + usuario.apellidos;
        }
        else
        {
            textoUsuario.text = "Bienvenido/a, usuario desconocido";
        }

        // Asignar botones
        btnEntradas.onClick.AddListener(() => CambiarPanel(panelEntradas));
        btnSalidas.onClick.AddListener(() => CambiarPanel(panelSalidas));
        btnStock.onClick.AddListener(() => CambiarPanel(panelStock));
        btnHistorial.onClick.AddListener(() => CambiarPanel(panelHistorial));

        // Mostrar panel inicial
        CambiarPanel(panelEntradas);
    }

    void CambiarPanel(GameObject panelActivo)
    {
        panelEntradas.SetActive(false);
        panelSalidas.SetActive(false);
        panelStock.SetActive(false);
        panelHistorial.SetActive(false);

        panelActivo.SetActive(true);
    }

    public void CerrarSesion()
    {
        PlayerPrefs.DeleteKey("dni"); // Borra el usuario actual
        SceneManager.LoadScene(0); // Carga la escena del login
    }
    

}
