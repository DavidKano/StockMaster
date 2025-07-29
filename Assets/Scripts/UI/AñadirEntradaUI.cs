using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AñadirEntradaUI : MonoBehaviour
{
    [Header("Campos del formulario")]
    public TMP_InputField inputArticulo;
    public TMP_InputField inputUbicacion;
    public TMP_InputField inputCantidad;
    public TMP_InputField inputProveedor;
    public TMP_InputField inputFecha;

    [Header("Botones")]
    public Button btnGuardar;
    public Button btnCancelar;

    [Header("Referencias externas")]
    public GameObject panel; // El propio panel para ocultarlo
    public EntradasManager entradasManager; // Para refrescar la tabla (opcional)

    private Entrada entradaEnEdicion = null;


    void Start()
    {
        btnGuardar.onClick.AddListener(GuardarEntrada);
        btnCancelar.onClick.AddListener(() => panel.SetActive(false));

        // Prellenar la fecha actual
        inputFecha.text = DateTime.Now.ToString("dd/MM/yyyy");
    }

    void GuardarEntrada()
    {
        if (string.IsNullOrWhiteSpace(inputArticulo.text) ||
            string.IsNullOrWhiteSpace(inputUbicacion.text) ||
            string.IsNullOrWhiteSpace(inputCantidad.text) ||
            string.IsNullOrWhiteSpace(inputProveedor.text))
        {
            Debug.LogWarning("Por favor, completa todos los campos.");
            return;
        }

        if (entradaEnEdicion == null)
        {
            Entrada nueva = new Entrada
            {
                articulo = inputArticulo.text.Trim(),
                ubicacion = inputUbicacion.text.Trim(),
                proveedor = inputProveedor.text.Trim(),
                fecha = inputFecha.text.Trim()
            };

            // Parsear cantidad a entero
            if (!int.TryParse(inputCantidad.text, out int cantidadNum))
            {
                Debug.LogWarning("Cantidad no válida.");
                return;
            }

            nueva.cantidad = cantidadNum.ToString();

            DBManager.InsertarEntrada(nueva);
            Debug.Log("Entrada guardada correctamente.");

        }
        else
        {
            // Modo editar
            entradaEnEdicion.articulo = inputArticulo.text;
            entradaEnEdicion.ubicacion = inputUbicacion.text;
            entradaEnEdicion.cantidad = inputCantidad.text;
            entradaEnEdicion.proveedor = inputProveedor.text;
            entradaEnEdicion.fecha = inputFecha.text;

            DBManager.ActualizarEntrada(entradaEnEdicion);

        }

        // Opcional: refrescar tabla
        if (entradasManager != null)
        {
            entradasManager.MostrarEntradas();
        }

        panel.SetActive(false); // Ocultar el panel

        entradaEnEdicion = null;

    }

    public void RellenarCampos(Entrada entrada)
    {
        entradaEnEdicion = entrada;

        inputArticulo.text = entrada.articulo;
        inputUbicacion.text = entrada.ubicacion;
        inputCantidad.text = entrada.cantidad;
        inputProveedor.text = entrada.proveedor;
        inputFecha.text = entrada.fecha;

        panel.SetActive(true);
    }

    public void PrepararParaNuevaEntrada()
{
    entradaEnEdicion = null;

    inputArticulo.text = "";
    inputUbicacion.text = "";
    inputCantidad.text = "";
    inputProveedor.text = "";
    inputFecha.text = DateTime.Now.ToString("dd/MM/yyyy");

    panel.SetActive(true);
}



}
