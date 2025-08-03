using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SalidasManager : MonoBehaviour
{
    public Transform content;
    public GameObject filaPrefab;

    public Button btnAñadirSalida; // ← Botón
    public AñadirSalidaUI panelAñadirSalida; // ← Panel emergente
    public ConfirmarEliminarUI panelConfirmarEliminar;


    void Start()
    {
        MostrarSalidas(); // carga al iniciar la escena

        btnAñadirSalida.onClick.AddListener(() =>
        {
            panelAñadirSalida.PrepararParaNuevaSalida();
            panelAñadirSalida.CargarArticulos(DBManager.ObtenerArticulos());
            panelAñadirSalida.gameObject.SetActive(true);
        });
    }

    public void MostrarSalidas()
    {
        foreach (Transform hijo in content)
        {
            Destroy(hijo.gameObject);
        }

        List<Salida> salidas = DBManager.ObtenerSalidas();

        foreach (Salida salida in salidas)
        {
            GameObject fila = Instantiate(filaPrefab, content);
            var itemUI = fila.GetComponent<ItemSalidaUI>();

            itemUI.Configurar(
            salida.articulo,
            salida.ubicacion,
            salida.cantidad,
            salida.persona,
            salida.fecha,
            salida

            );
            itemUI.salidasManager = this;
            itemUI.panelAñadirSalida = panelAñadirSalida;
            itemUI.panelConfirmarEliminar = panelConfirmarEliminar;

        }
    }
}

