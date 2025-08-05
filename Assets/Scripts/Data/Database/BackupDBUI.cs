using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using SimpleFileBrowser; // NUEVO

public class BackupDBUI : MonoBehaviour
{
    public Button btnExportar;
    public Button btnImportar;
    public TextMeshProUGUI feedbackText;

    public EntradasManager entradasManager;
    public SalidasManager salidasManager;
    public StockManager stockManager;
    public HistorialManager historialManager;




    private string dbName = "stockmaster.sqlite";

    void Start()
    {
        btnExportar.onClick.AddListener(ExportarBD);
        btnImportar.onClick.AddListener(ImportarBD);
    }

    void ExportarBD()
    {
        FileBrowser.SetDefaultFilter(".sqlite");

        FileBrowser.ShowSaveDialog(
            (paths) =>
            {
                if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
                {
                    string origen = System.IO.Path.Combine(Application.persistentDataPath, dbName);

                    // Garantiza extensión .sqlite
                    string destino = paths[0];
                    if (System.IO.Path.GetExtension(destino).ToLower() != ".sqlite")
                        destino += ".sqlite";

                    System.IO.File.Copy(origen, destino, true);
                    feedbackText.text = " Base de datos exportada correctamente.";
                }
            },
            () => { },
            FileBrowser.PickMode.Files,
            false,
            null,
            "Exportar",
            "Guardar"
        );
    }



    void ImportarBD()
    {
        // (opcional) filtra a .sqlite
        FileBrowser.SetFilters(true, new FileBrowser.Filter("SQLite", ".sqlite", ".db"));
        FileBrowser.SetDefaultFilter(".sqlite");

        FileBrowser.ShowLoadDialog(
            (paths) =>
            {
                if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
                {

                    // 1. Cierra la conexión actual
                    DBManager.Close();

                    // 2. Copia el archivo nuevo
                    string destino = System.IO.Path.Combine(Application.persistentDataPath, dbName);
                    System.IO.File.Copy(paths[0], destino, true);

                    // 3. Reabre la nueva conexión
                    DBManager.Init();
                    RefrescarTodo();



                    feedbackText.text = "✅ Base de datos importada.";
                    Debug.Log("Importada y recargada desde: " + paths[0]);
                }
            },
            () => { },
            FileBrowser.PickMode.Files,   // ← este era el que faltaba
            false,                        // no permitir múltiple selección
            null,                         // initialPath
            "Importar",                   // título
            "Seleccionar .sqlite"         // texto botón
        );

    }
    void RefrescarTodo()
    {
        if (entradasManager != null)
            entradasManager.MostrarEntradas();

        if (salidasManager != null)
            salidasManager.MostrarSalidas();

        if (stockManager != null)
            stockManager.MostrarStock();

        if (historialManager != null)
            historialManager.MostrarHistorial();

        Debug.Log("✅ Refresco de todas las pestañas completado tras importar BD.");
    }


}
