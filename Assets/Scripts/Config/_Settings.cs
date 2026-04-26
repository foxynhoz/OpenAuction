using UnityEngine;
using System.Collections;
using System.IO;

public class _Settings : MonoBehaviour
{
    private void Start()
    {
        string dataDir = Application.dataPath + "/LeilaoData/";
        string leiloesDir = dataDir + "Leiloes/";

        if (!Directory.Exists(dataDir))
        {
            Directory.CreateDirectory(dataDir);
        }
        if (!Directory.Exists(leiloesDir))
        {
            Directory.CreateDirectory(leiloesDir);
        }
    }
}

public class FileHandler
{
     static string dataDir = Application.dataPath + "/LeilaoData/";
     static string leiloesDir = dataDir + "Leiloes/";

        public void UpdateFile(string fileName, string content) //atualizar o arquivo de texto com o valor do lance atual
        {
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }

            File.WriteAllText(dataDir + fileName, content);
        }
 }
    

