using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemStockUI : MonoBehaviour
{
    // Referencias a los textos UI (deben asignarse en el prefab)
    public TMP_Text textoArticulo;
    public TMP_Text textoUbicacion;
    public TMP_Text textoCantidad;

    // Método para configurar la fila visual
    public void Configurar(string articulo, string ubicacion, int cantidad)
    {
        textoArticulo.text = articulo;
        textoUbicacion.text = ubicacion;
        textoCantidad.text = cantidad.ToString();
    }
}
