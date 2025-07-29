using SQLite4Unity3d;
using System;

[Serializable]
public class Usuario
{
    [PrimaryKey]
    public string dni { get; set; }
    public string nombre { get; set; }
    public string apellidos { get; set; }

    public Usuario() { }

    public Usuario(string dni, string nombre, string apellidos)
    {
        this.dni = dni;
        this.nombre = nombre;
        this.apellidos = apellidos;
    }

    public override string ToString()
    {
        return $"{dni}: {nombre} {apellidos}";
    }
}
