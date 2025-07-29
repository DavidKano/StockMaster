using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemEntradaUI : MonoBehaviour
{
    public TextMeshProUGUI textoArticulo;
    public TextMeshProUGUI textoUbicacion;
    public TextMeshProUGUI textoCantidad;
    public TextMeshProUGUI textoProveedor;
    public TextMeshProUGUI textoFecha;
    public Button btnEditar;
    public Button btnEliminar;
    private AñadirEntradaUI panelAñadirEntrada; // arrástralo desde el Inspector

    public ConfirmarEliminarUI panelConfirmarEliminar;
    public EntradasManager entradasManager; // ← Arrastrable desde código



    private Entrada entradaActual;

    public void Configurar(string articulo, string ubicacion, string cantidad, string proveedor, string fecha, Entrada entrada)
    {
        textoArticulo.text = articulo;
        textoUbicacion.text = ubicacion;
        textoCantidad.text = cantidad;
        textoProveedor.text = proveedor;
        textoFecha.text = fecha;

        entradaActual = entrada;

        btnEditar.onClick.AddListener(() => EditarEntrada());
        btnEliminar.onClick.AddListener(() => EliminarEntrada());

    }
    void EditarEntrada()
    {
        panelAñadirEntrada.RellenarCampos(entradaActual);
        panelAñadirEntrada.gameObject.SetActive(true);
        Debug.Log("Editar entrada: " + entradaActual.id);
        // Aquí puedes abrir un panel con los datos precargados
    }
    void EliminarEntrada()
    {
        panelConfirmarEliminar.Mostrar(() =>
        {
        DBManager.EliminarEntrada(entradaActual.id);
        entradasManager.MostrarEntradas(); // Refrescar la tabla
        });


    }

    public void AsignarPanelEdicion(AñadirEntradaUI panel)
    {
        panelAñadirEntrada = panel;
    }











}
