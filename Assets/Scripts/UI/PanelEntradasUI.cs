using UnityEngine;
using UnityEngine.UI;

public class PanelEntradasUI : MonoBehaviour
{
    public GameObject panelAñadirEntrada;
    public Button btnAñadirEntrada;

    void Start()
    {
        btnAñadirEntrada.onClick.AddListener(() =>
           {
               // Aquí va:
                panelAñadirEntrada.GetComponent<AñadirEntradaUI>().PrepararParaNuevaEntrada();
           });
        }

    void MostrarPanelEntrada()
    {
        panelAñadirEntrada.SetActive(true);
    }
}
