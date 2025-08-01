using SQLite4Unity3d;

public class Salida
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }

    public string articulo { get; set; }
    public string ubicacion { get; set; }
    public string cantidad { get; set; }
    public string persona { get; set; }
    public string fecha { get; set; }
}
