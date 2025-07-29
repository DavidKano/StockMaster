using SQLite4Unity3d;

public class Entrada
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }

    public string articulo { get; set; }
    public string ubicacion { get; set; }
    public string cantidad { get; set; }
    public string proveedor { get; set; }
    public string fecha { get; set; }
}
