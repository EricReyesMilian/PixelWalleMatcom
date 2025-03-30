using UnityEngine;
using System.IO;
using SFB; // Necesitas el paquete "StandaloneFileBrowser"
using TMPro;
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadCustomFile();
        }
    }
    // public void LoadCustomFile()//
    // {
    //     // Abre el diálogo para seleccionar archivo
    //     var paths = StandaloneFileBrowser.OpenFilePanel("Abrir archivo .gw", "", "gw", false);

    //     if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
    //     {
    //         fileContent = File.ReadAllText(paths[0]);
    //         Debug.Log("Contenido cargado:\n" + fileContent);
    //     }
    // }
    public void LoadCustomFile()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Abrir archivo .gw", "", "gw", false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            try
            {
                fileContent = File.ReadAllText(paths[0]); // Usamos la propiedad, no el campo
                Debug.Log("Archivo cargado correctamente");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error al leer el archivo: {e.Message}");
            }
        }
    }
}