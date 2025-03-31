using UnityEngine;
using System.IO;
using SFB; // Necesitas el paquete "StandaloneFileBrowser"
using TMPro;
using System.Text;
public class CustomFileLoader : MonoBehaviour
{
    public string _fileContent;
    public TMP_InputField scriptInputField;
    public string fileContent
    {
        get { return _fileContent; }
        set
        {
            _fileContent = value;
            // Actualiza el InputField cuando cambia el valor
            if (scriptInputField != null)
            {
                scriptInputField.text = _fileContent;
            }
        }
    }
    void Start()
    {

    }

    public void ExportToGWFile()
    {
        if (scriptInputField == null || string.IsNullOrEmpty(scriptInputField.text))
        {
            ErrorHandler.errorHandler.Error("No hay contenido para exportar!");
            return;
        }

        // Configurar la extensión y filtro
        var extensionList = new[] {
            new ExtensionFilter("GW Files", "gw"),
            new ExtensionFilter("All Files", "*")
        };

        // Abrir diálogo para guardar archivo
        string path = StandaloneFileBrowser.SaveFilePanel(
            "Exportar como .gw",
            "",
            "nuevoArchivo.gw",
            extensionList
        );

        if (!string.IsNullOrEmpty(path))
        {
            try
            {
                // Asegurar que tenga la extensión .gw
                if (!path.EndsWith(".gw"))
                {
                    path += ".gw";
                }

                // Escribir el archivo con codificación UTF-8
                File.WriteAllText(path, scriptInputField.text, Encoding.UTF8);
                ErrorHandler.errorHandler.Error($"Archivo guardado en: {path}");
            }
            catch (System.Exception e)
            {
                ErrorHandler.errorHandler.Error($"Error al exportar: {e.Message}");
            }
        }
    }

    public void LoadCustomFile()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Abrir archivo .gw", "", "gw", false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            try
            {
                fileContent = File.ReadAllText(paths[0]); // Usamos la propiedad, no el campo
                ErrorHandler.errorHandler.Info("Archivo cargado correctamente");
            }
            catch (System.Exception e)
            {
                ErrorHandler.errorHandler.Error($"Error al leer el archivo: {e.Message}");
            }
        }
    }
}