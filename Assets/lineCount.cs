using UnityEngine;
using TMPro;
public class lineCount : MonoBehaviour
{
    public TMP_InputField script;
    public TextMeshProUGUI lines;
    int count;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        script.onValueChanged.AddListener(CountLines);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void CountLines(string text)
    {
        string[] lines = text.Split('\n');

        // Actualizar el contador
        count = lines.Length;
        if (this.lines != null)
        {
            this.lines.text = "";
            for (int i = 1; i <= count; i++)
            {
                this.lines.text += $"{i}\n";
            }


        }

    }
}



