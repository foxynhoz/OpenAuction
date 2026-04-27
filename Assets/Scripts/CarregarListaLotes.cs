using System.IO;
using UnityEngine;
using static UnityEditor.Rendering.CameraUI;

public class CsvVacas : MonoBehaviour
{
    /* DEPRECATED - USAR JSON INSTEAD
     * 
     * 
     * 
    FileHandler fileHandler = new FileHandler();

    private string csvPath;
    private string[][] data;
    private int currentLine = 0;

    void Start()
    {
        // CSV dentro da pasta da build (editável)
        csvPath = Path.Combine(Application.dataPath, "LeilaoData/LISTA_DE_LOTES.csv");

        LoadCSV();
    }

    [ContextMenu("Load CSV")]
    void LoadCSV()
    {
        if (!File.Exists(csvPath))
        {
            Debug.LogError("CSV não encontrado em: " + csvPath);
            return;
        }

        string fileContent = File.ReadAllText(csvPath);

        string[] lines = fileContent.Split('\n');

        data = new string[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            // remove \r (Windows)
            string cleanLine = lines[i].Trim().Replace("\r", "");

            data[i] = cleanLine.Split(',');
        }
    }

    [ContextMenu("Next Cow")]
    public void NextCow()
    {
        if (data == null || data.Length == 0) return;

        currentLine++;

        if (currentLine >= data.Length)
            currentLine = 0;

        ShowCurrentCow();
    }

    public void PreviousCow()
    {
        if (data == null || data.Length == 0) return;

        currentLine--;

        if (currentLine < 0)
            currentLine = data.Length - 1;

        ShowCurrentCow();
    }

    void ShowCurrentCow()
    {
        string[] row = data[currentLine];

        string output = "";

        int maxCols = 7;

        for (int i = 0; i < maxCols; i++)
        {
            if (i < row.Length)
                output += row[i];

            output += "\n";
        }

        fileHandler.UpdateFile("LoteAtual.txt", output);
        
        Debug.Log("Salvo: " + " | Linha: " + currentLine);
    }
    */

}
