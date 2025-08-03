using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static DBManager;

public class StockManager : MonoBehaviour
{
    [Header("Listado de stock")]
    public Transform content;
    public GameObject prefabItemStock;

    [Header("Panel de búsqueda")]
    public GameObject panelBuscarStock;
    public TMP_InputField inputArticuloBuscar;
    public TMP_InputField inputUbicacionBuscar;
    public TMP_Dropdown dropdownCantidadBuscar;

    [Header("Botones")]
    public Button btnBuscar;
    public Button btnFiltrar;
    public Button btnCancelar;

    public Button BtnActualizar;

    [Header("Panel")]

    public GameObject panelAñadirEntrada;


    private void Start()
    {
        btnBuscar.onClick.AddListener(() => panelBuscarStock.SetActive(true));
        //btnCancelar.onClick.AddListener(() => panelBuscarStock.SetActive(false));
        btnCancelar.onClick.AddListener(CancelarBusqueda);
        btnFiltrar.onClick.AddListener(FiltrarStock);
        BtnActualizar.onClick.AddListener(MostrarStock);


        DBManager.Init();


        MostrarStock(); // carga inicial
    }

    public void MostrarStock()
    {
        List<StockItem> stock = DBManager.ObtenerStockActual();
        MostrarEnLista(stock);
    }

    private void FiltrarStock()
    {
        string articulo = inputArticuloBuscar.text.Trim().ToLower();
        string ubicacion = inputUbicacionBuscar.text.Trim().ToLower();
        string cantidadFiltro = dropdownCantidadBuscar.options[dropdownCantidadBuscar.value].text;

        List<StockItem> stockFiltrado = DBManager.ObtenerStockActual().FindAll(item =>
        {
            bool coincideArticulo = string.IsNullOrEmpty(articulo) || item.articulo.ToLower().Contains(articulo);
            bool coincideUbicacion = string.IsNullOrEmpty(ubicacion) || item.ubicacion.ToLower().Contains(ubicacion);
            bool coincideCantidad =
                cantidadFiltro == "Todos" ||
                (cantidadFiltro == "0" && item.cantidad == 0) ||
                (cantidadFiltro == ">0" && item.cantidad > 0);

            return coincideArticulo && coincideUbicacion && coincideCantidad;
        });

        MostrarEnLista(stockFiltrado);
        panelBuscarStock.SetActive(false);
        if (panelAñadirEntrada.activeSelf)
        {
            Debug.LogWarning("Panel de entrada estaba activo. Lo cerramos a la fuerza.");
            panelAñadirEntrada.SetActive(false);
        }
    }

    private void MostrarEnLista(List<StockItem> stock)
    {
        foreach (Transform hijo in content)
            Destroy(hijo.gameObject);

        foreach (StockItem item in stock)
        {
            GameObject fila = Instantiate(prefabItemStock, content);
            fila.GetComponent<ItemStockUI>().Configurar(item.articulo, item.ubicacion, item.cantidad);
        }
    }

    private void CancelarBusqueda()
    {
        panelBuscarStock.SetActive(false);

        if (panelAñadirEntrada.activeSelf)
        {
            Debug.LogWarning("Panel de entrada estaba activo al cancelar búsqueda. Lo cerramos.");
            panelAñadirEntrada.SetActive(false);
        }
    }


}
