using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HistorialManager : MonoBehaviour
{
    public Transform content;       // ContentHistorial
    public GameObject filaPrefab;   // Prefab ItemHistorialUI

    public Button btnActualizarHistorial;
    public GameObject panelAñadirEntrada;


    [Header("Panel de filtro")]
    public GameObject panelFiltroHistorial;
    public TMP_InputField inputBuscarArticuloHistorial;
    public TMP_InputField inputBuscarUbicacionHistorial;
    public TMP_Dropdown dropdownTipoHistorial;
    public Button btnAbrirFiltroHistorial;
    public Button btnFiltrarHistorial;
    public Button btnCancelarHistorial;


    void OnEnable()  // o Start(), como prefieras
    {
        MostrarHistorial();
        btnActualizarHistorial.onClick.AddListener(MostrarHistorial);
        btnAbrirFiltroHistorial.onClick.AddListener(() => panelFiltroHistorial.SetActive(true));
        btnCancelarHistorial.onClick.AddListener(Cancelar);
        btnFiltrarHistorial.onClick.AddListener(FiltrarHistorial);



    }

    public void MostrarHistorial()
    {
        foreach (Transform t in content)
            GameObject.Destroy(t.gameObject);

        List<HistorialItem> datos = DBManager.ObtenerHistorial();

        foreach (var h in datos)
        {
            var go = Instantiate(filaPrefab, content);
            go.GetComponent<ItemHistorialUI>().Configurar(h);
        }
    }
    private void FiltrarHistorial()
    {
        string art = inputBuscarArticuloHistorial.text.Trim().ToLower();
        string ubi = inputBuscarUbicacionHistorial.text.Trim().ToLower();
        string tipo = dropdownTipoHistorial.options[dropdownTipoHistorial.value].text;

        var todos = DBManager.ObtenerHistorial();

        List<HistorialItem> filtrado = todos.FindAll(h =>
        {
            bool porArticulo = string.IsNullOrEmpty(art) || h.articulo.ToLower().Contains(art);
            bool porUbicacion = string.IsNullOrEmpty(ubi) || h.ubicacion.ToLower().Contains(ubi);
            bool porTipo = tipo == "Todos" || h.tipo == tipo;

            return porArticulo && porUbicacion && porTipo;
        });

        MostrarEnLista(filtrado);
        panelFiltroHistorial.SetActive(false);
        if (panelAñadirEntrada.activeSelf)
        {
            Debug.LogWarning("Panel de entrada estaba activo. Lo cerramos a la fuerza.");
            panelAñadirEntrada.SetActive(false);
        }
        
    }


    private void MostrarEnLista(List<HistorialItem> lista)
    {
        foreach (Transform t in content)
            Destroy(t.gameObject);

        foreach (var h in lista)
        {
            var go = Instantiate(filaPrefab, content);
            go.GetComponent<ItemHistorialUI>().Configurar(h);
        }
    }

    private void Cancelar()
    {
        this.gameObject.SetActive(false);  // Oculta el panel actual de salida
        panelFiltroHistorial.SetActive(false);           // Oculta el panel visual padre si hace falta

        if (panelAñadirEntrada.activeSelf)
        {
            Debug.LogWarning("Panel de entrada estaba activo al cancelar. Lo cerramos.");
            panelAñadirEntrada.SetActive(false);
        }
    }


}
