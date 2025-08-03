using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSalidaUI : MonoBehaviour
{
    public TextMeshProUGUI textoArticulo;
    public TextMeshProUGUI textoUbicacion;
    public TextMeshProUGUI textoCantidad;
    public TextMeshProUGUI textoPersona;
    public TextMeshProUGUI textoFecha;
    public Button btnEditar;
    public Button btnEliminar;
    public AñadirSalidaUI panelAñadirSalida;

    public ConfirmarEliminarUI panelConfirmarEliminar;
    public SalidasManager salidasManager; // ← Arrastrable desde código

    private Salida salidaActual;
    

    public void Configurar(string articulo, string ubicacion, string cantidad, string persona, string fecha, Salida salida)
    {
        textoArticulo.text = articulo;
        textoUbicacion.text = ubicacion;
        textoCantidad.text = cantidad;
        textoPersona.text = persona;
        textoFecha.text = fecha;

        salidaActual = salida;


        btnEditar.onClick.RemoveAllListeners();
        btnEditar.onClick.AddListener(() => EditarSalida());

        btnEliminar.onClick.RemoveAllListeners();
        btnEliminar.onClick.AddListener(() => EliminarSalida());


    }
    void EditarSalida()
    {
        panelAñadirSalida.RellenarCamposParaEdicion(salidaActual);
        panelAñadirSalida.gameObject.SetActive(true);
        Debug.Log("Editar entrada: " + salidaActual.id);
        // Aquí puedes abrir un panel con los datos precargados
    }
    void EliminarSalida()
    {
        panelConfirmarEliminar.Mostrar(() =>
        {
            DBManager.EliminarSalida(salidaActual.id);
            salidasManager.MostrarSalidas(); // Refrescar la tabla
            Debug.Log("Intentando eliminar salida: " + salidaActual.articulo);


        });


    }

    public void AsignarPanelEdicion(AñadirSalidaUI panel)
    {
        panelAñadirSalida = panel;
    }











}