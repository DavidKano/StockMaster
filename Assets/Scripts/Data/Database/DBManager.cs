using UnityEngine;
using SQLite4Unity3d;
using System.IO;
using System.Linq;
using System.Collections.Generic;

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

        Debug.Log("Base de datos inicializada en: " + dbPath);
    }

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

    public static List<Entrada> ObtenerEntradas()
    {
        return db.Table<Entrada>().ToList();
    }

    public static void InsertarEntrada(Entrada entrada)
    {
        if (db != null)
        {
            db.Insert(entrada);
            Debug.Log("Entrada insertada correctamente en la base de datos.");
        }
        else
        {
            Debug.LogError("La base de datos no está inicializada.");
        }
    }

    public static void EliminarEntrada(int id)
    {
        db.Delete<Entrada>(id);
        Debug.Log("Entrada eliminada correctamente");
    }

    public static void ActualizarEntrada(Entrada entrada)
    {
        if (db == null) return;

        db.Update(entrada);
        Debug.Log("Entrada actualizada correctamente: " + entrada.id);
    }







}
