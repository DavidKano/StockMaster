using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistorialItem
{
    public string tipo { get; set; }       // "Entrada" | "Salida"
    public string articulo { get; set; }
    public string ubicacion { get; set; }
    public int    cantidad { get; set; }   // positivo para entrada, negativo para salida
    public string actor { get; set; }      // proveedor/persona
    public string fecha { get; set; }      // dd/MM/yyyy
    public int    fecha_orden { get; set; } // yyyymmdd para ordenar
}