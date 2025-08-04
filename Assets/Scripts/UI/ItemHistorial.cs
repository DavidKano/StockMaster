using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemHistorialUI : MonoBehaviour
{
    public TMP_Text textoTipo;
    public TMP_Text textoArticulo;
    public TMP_Text textoUbicacion;
    public TMP_Text textoCantidad;
    public TMP_Text textoActor;
    public TMP_Text textoFecha;

    public void Configurar(HistorialItem h)
    {
        textoTipo.text      = h.tipo;              // "Entrada" / "Salida"
        textoArticulo.text  = h.articulo;
        textoUbicacion.text = h.ubicacion;
        textoActor.text     = h.actor;
        textoFecha.text     = h.fecha;

        // Cantidad con signo y color
        int cant = h.cantidad; // entrada +, salida -
        textoCantidad.text = (cant > 0 ? "+" : "") + cant.ToString();

        // Color simple: verde para entradas, rojo para salidas
        var col = cant >= 0 ? new Color32(28, 172, 120, 255) : new Color32(200, 70, 70, 255);
        textoCantidad.color = col;
        textoTipo.color = col;
    }
}
