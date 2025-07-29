using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntradasManager : MonoBehaviour
{
    public GameObject filaPrefab; // Prefab ItemEntrada
    public Transform content;     // Donde se instancian (Content del ScrollView)

    public AñadirEntradaUI panelAñadirEntrada;

    public ConfirmarEliminarUI confirmacionEliminar;



    void OnEnable()
    {
        MostrarEntradas();
    }

    public void MostrarEntradas()
    {
        // 1. Limpiar contenido previo
        foreach (Transform hijo in content)
        {
            Destroy(hijo.gameObject);
        }

        // 2. Obtener entradas desde la base de datos (esto lo implementaremos pronto)
        List<Entrada> listaEntradas = DBManager.ObtenerEntradas();

        // 3. Instanciar una fila por cada entrada
        foreach (Entrada entrada in listaEntradas)
        {
            GameObject fila = Instantiate(filaPrefab, content);
            ItemEntradaUI itemUI = fila.GetComponent<ItemEntradaUI>();

            itemUI.Configurar(
                entrada.articulo,
                entrada.ubicacion,
                entrada.cantidad,
                entrada.proveedor,
                entrada.fecha,
                entrada
            );

            // Aquí le pasamos el panel desde el manager:
            itemUI.AsignarPanelEdicion(panelAñadirEntrada);

            itemUI.panelConfirmarEliminar = confirmacionEliminar;

            itemUI.entradasManager = this;                          




        }
    }
}
