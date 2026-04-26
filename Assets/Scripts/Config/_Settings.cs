using UnityEngine;
using System.Collections;
using System.IO;

public class _Settings : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class FileHandler
{
    public void UpdateFile(string fileName, string content) //atualizar o arquivo de texto com o valor do lance atual
    {
        string dataDir = Application.dataPath + "/LeilaoData/";
        if (!Directory.Exists(dataDir))
        {
            Directory.CreateDirectory(dataDir);
        }

        File.WriteAllText(dataDir + fileName, content);
    }
}
