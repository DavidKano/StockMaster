using UnityEngine;
using SQLite4Unity3d;
using System.IO;
using System.Collections.Generic;
using System.Linq;


public class DBManager
{
    private static SQLiteConnection db;

    public static void Init()
    {
        string dbName = "stockmaster.sqlite";
        string dbPath = Path.Combine(Application.persistentDataPath, dbName);

        db = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        db.CreateTable<Usuario>();
        db.CreateTable<Entrada>();
        db.CreateTable<Salida>();

        Debug.Log("Base de datos inicializada en: " + dbPath);
    }

    // ──────────────── Usuarios ────────────────

    public static void RegistrarUsuario(Usuario usuario)
    {
        if (!ExisteUsuario(usuario.dni))
        {
            db.Insert(usuario);
            Debug.Log("Usuario registrado correctamente: " + usuario);
        }
        else
        {
            Debug.LogWarning("El usuario con ese DNI ya existe.");
        }
    }

    public static bool ExisteUsuario(string dni)
    {
        var user = db.Find<Usuario>(dni);
        return user != null;
    }

    public static Usuario ObtenerUsuario(string dni)
    {
        return db.Find<Usuario>(dni);
    }

    // ──────────────── Entradas ────────────────

    public static List<Entrada> ObtenerEntradas()
    {
        return db.Table<Entrada>().ToList();
    }

    public static void InsertarEntrada(Entrada entrada)
    {
        db.Insert(entrada);
        Debug.Log("Entrada insertada correctamente en la base de datos.");
    }

    public static void EliminarEntrada(int id)
    {
        db.Delete<Entrada>(id);
        Debug.Log("Entrada eliminada correctamente");
    }

    public static void ActualizarEntrada(Entrada entrada)
    {
        db.Update(entrada);
        Debug.Log("Entrada actualizada correctamente: " + entrada.id);
    }

    // ──────────────── Salidas ────────────────

    public static void InsertarSalida(Salida salida)
    {
        db.Insert(salida);

    }

    public static void ActualizarSalida(Salida salida)
    {
        db.Update(salida);
    }

    public static void EliminarSalida(int id)
    {
        db.Delete<Salida>(id);
    }

    public static List<Salida> ObtenerSalidas()
    {
        return db.Table<Salida>().ToList();
    }

    // ──────────────── Artículos y Ubicaciones ────────────────

    public static List<string> ObtenerArticulos()
    {
        return db.Query<Entrada>("SELECT DISTINCT articulo FROM Entrada")
                 .Select(e => e.articulo)
                 .ToList();
    }

    public static List<string> ObtenerUbicacionesDeArticulo(string articulo)
    {
        return db.Query<Entrada>("SELECT DISTINCT ubicacion FROM Entrada WHERE articulo = ?", articulo)
                 .Select(e => e.ubicacion)
                 .ToList();
    }

    // ──────────────── Stock lógico ────────────────

    public static int CalcularStock(string articulo, string ubicacion)
    {
        int totalEntradas = db.Table<Entrada>()
            .Where(e => e.articulo == articulo && e.ubicacion == ubicacion)
            .Sum(e => int.TryParse(e.cantidad, out var n) ? n : 0);

        int totalSalidas = db.Table<Salida>()
            .Where(s => s.articulo == articulo && s.ubicacion == ubicacion)
            .Sum(s => int.TryParse(s.cantidad, out var n) ? n : 0);

        return totalEntradas - totalSalidas;
    }

    // ──────────────── Stock ────────────────

    public static List<StockItem> ObtenerStockActual()
    {
        string consulta = @"
        SELECT articulo, ubicacion, SUM(cantidad) as cantidad
        FROM (
            SELECT articulo, ubicacion, cantidad FROM Entrada
            UNION ALL
            SELECT articulo, ubicacion, -cantidad FROM Salida
        )
        GROUP BY articulo, ubicacion
        ORDER BY articulo, ubicacion
    ";

        return db.Query<StockItem>(consulta);
    }


    public class StockItem
{
    public string articulo { get; set; }
    public string ubicacion { get; set; }
    public int cantidad { get; set; }
}

   


}
