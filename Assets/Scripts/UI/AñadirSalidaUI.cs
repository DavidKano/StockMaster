using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AñadirSalidaUI : MonoBehaviour
{
    [Header("Campos del formulario")]
    public TMP_Dropdown inputDropdownArticulo;
    public TMP_Dropdown inputDropdownUbicacion;
    public TMP_InputField inputCantidad;
    public TMP_InputField inputPersona;
    public TMP_InputField inputFecha;

    [Header("Botones")]
    public Button btnGuardar;
    public Button btnCancelar;

    [Header("Referencias externas")]
    public GameObject panel2;
    public SalidasManager salidasManager;

    public GameObject panelAñadirEntrada;

    private Salida salidaEnEdicion = null;

    void Start()
    {
        btnGuardar.onClick.AddListener(GuardarSalida);
        //btnCancelar.onClick.AddListener(() => panel2.SetActive(false));
        btnCancelar.onClick.AddListener(Cancelar);
        inputDropdownArticulo.onValueChanged.AddListener(delegate { ActualizarUbicaciones(); });
        //MostrarSalidas(); // carga todas las salidas al iniciar

    }

    public void PrepararParaNuevaSalida()
    {
        salidaEnEdicion = null;
        inputCantidad.text = "";
        inputPersona.text = "";
        inputFecha.text = System.DateTime.Now.ToString("dd/MM/yyyy");
        inputDropdownArticulo.value = 0;
        ActualizarUbicaciones();
        inputDropdownUbicacion.value = 0;
    }

    /*public void RellenarCampos(Salida salida)
    {
        inputDropdownArticulo.value = inputDropdownArticulo.options.FindIndex(o => o.text == salida.articulo);
        ActualizarUbicaciones(); // actualizar antes de setear ubicación
        inputDropdownUbicacion.value = inputDropdownUbicacion.options.FindIndex(o => o.text == salida.ubicacion);
        inputCantidad.text = salida.cantidad;
        inputPersona.text = salida.persona;
        inputFecha.text = salida.fecha;

        salidaEnEdicion = salida;
    }*/

    private void GuardarSalida()
    {
        string articulo = inputDropdownArticulo.options[inputDropdownArticulo.value].text;
        string ubicacion = inputDropdownUbicacion.options[inputDropdownUbicacion.value].text;
        string persona = inputPersona.text.Trim();
        string fecha = inputFecha.text.Trim();

        if (!int.TryParse(inputCantidad.text.Trim(), out int cantidadNum))
        {
            Debug.LogWarning("Cantidad no válida.");
            return;
        }

        if (string.IsNullOrWhiteSpace(persona))
        {
            Debug.LogWarning("Falta persona responsable.");
            return;
        }

        if (salidaEnEdicion == null)
        {
            // Nueva salida
            Salida nueva = new Salida
            {
                articulo = articulo,
                ubicacion = ubicacion,
                cantidad = cantidadNum.ToString(),
                persona = persona,
                fecha = fecha
            };

            DBManager.InsertarSalida(nueva);
            salidasManager.MostrarSalidas(); // esto refresca la lista

            //DBManager.RestarDelStock(articulo, ubicacion, cantidadNum);
        }
        else
        {
            // Edición (si decides implementarlo más adelante)
            salidaEnEdicion.articulo = articulo;
            salidaEnEdicion.ubicacion = ubicacion;
            salidaEnEdicion.cantidad = cantidadNum.ToString();
            salidaEnEdicion.persona = persona;
            salidaEnEdicion.fecha = fecha;

            DBManager.ActualizarSalida(salidaEnEdicion);
        }

        salidasManager.MostrarSalidas();
        this.gameObject.SetActive(false);
        if (panelAñadirEntrada.activeSelf)
        {
            Debug.LogWarning("Panel de entrada estaba activo. Lo cerramos a la fuerza.");
            panelAñadirEntrada.SetActive(false);
        }



    }

    private void ActualizarUbicaciones()
    {
        string articulo = inputDropdownArticulo.options[inputDropdownArticulo.value].text;
        List<string> ubicaciones = DBManager.ObtenerUbicacionesDeArticulo(articulo);

        inputDropdownUbicacion.ClearOptions();
        inputDropdownUbicacion.AddOptions(ubicaciones);
    }

    // Llama esto al iniciar para rellenar el dropdown de artículos
    public void CargarArticulos(List<string> articulos)
    {
        inputDropdownArticulo.ClearOptions();
        inputDropdownArticulo.AddOptions(articulos);
        ActualizarUbicaciones();
    }

    public void RellenarCamposParaEdicion(Salida salida)
    {
        inputDropdownArticulo.value = inputDropdownArticulo.options.FindIndex(option => option.text == salida.articulo);
        ActualizarUbicaciones(); // refresca ubicaciones al seleccionar artículo
        inputDropdownUbicacion.value = inputDropdownUbicacion.options.FindIndex(option => option.text == salida.ubicacion);
        inputCantidad.text = salida.cantidad;
        inputPersona.text = salida.persona;
        inputFecha.text = salida.fecha;

        salidaEnEdicion = salida; // importante para saber que es edición
    }
    public void CerrarPanel()
    {
        panel2.SetActive(false);
    }

    private void Cancelar()
    {
        this.gameObject.SetActive(false);  // Oculta el panel actual de salida
        panel2.SetActive(false);           // Oculta el panel visual padre si hace falta

        if (panelAñadirEntrada.activeSelf)
        {
            Debug.LogWarning("Panel de entrada estaba activo al cancelar. Lo cerramos.");
            panelAñadirEntrada.SetActive(false);
        }
    }

}
