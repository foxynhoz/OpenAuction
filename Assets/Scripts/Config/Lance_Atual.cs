using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Lance_Atual : MonoBehaviour
{
    

    public InputField LanceAtual_TXT;
    public Text EntreLance_TXT;

    public float entreLance = 10f;
    public float LanceValue = 0f;

    void Start()
    {
        LanceAtual_TXT.text = LanceValue.ToString();
        UpdateFile();
    }

    void Update()
    {
        EntreLance_TXT.text = entreLance.ToString();
        LanceAtual_TXT.text = LanceValue.ToString();
    }

    public void addLance()
    {
        LanceValue += entreLance;
        UpdateFile();

    }
    public void subLance()
    {
        if (LanceValue > 0f)
        {
            LanceValue -= entreLance;
            UpdateFile();
            if (LanceValue < 0f)
            {
                LanceValue = 0f;
            }
        }
    }

    public void changeEntreLance()
    {
        entreLance = float.Parse(EntreLance_TXT.text);
    }
    
    public void Vendido()
    {
        LanceValue = 0f;
        UpdateFile();
    }

    private void UpdateFile()
    {
        string dataDir = Application.dataPath + "/LeilaoData";
        if (!Directory.Exists(dataDir))
        {
            Directory.CreateDirectory(dataDir);
        }

        File.WriteAllText(dataDir + "/LanceAtual.txt", LanceValue.ToString());
        Debug.Log(dataDir + "/LanceAtual.txt");
    }
}
