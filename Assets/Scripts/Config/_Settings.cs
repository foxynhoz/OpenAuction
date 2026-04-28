using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class _Settings : MonoBehaviour
{
    private void Start()
    {
        StartupConfig();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }
    public void StartupConfig()
    {
        string dataDir = Application.dataPath + "/LeilaoData/";
        string leiloesDir = dataDir + "Leiloes/";
        string obsDir = dataDir + "OBS_Stuff/";
        string videosDir = dataDir + "Videos/";

        if (!Directory.Exists(dataDir))
        {
            Directory.CreateDirectory(dataDir);
        }
        if (!Directory.Exists(leiloesDir))
        {
            Directory.CreateDirectory(leiloesDir);
        }
        if (!Directory.Exists(obsDir))
        {
            Directory.CreateDirectory(obsDir);
        }
        if (!Directory.Exists(videosDir))
        {
            Directory.CreateDirectory(videosDir);
        }
    }
}

public class FileHandler
{
     static string dataDir = Application.dataPath + "/LeilaoData/";
     static string leiloesDir = dataDir + "Leiloes/";
     static string videosDir = dataDir + "Videos/";

        public void UpdateFile(string fileName, string content) //atualizar o arquivo passado com o conteúdo passado
        {
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }

            File.WriteAllText(dataDir + fileName, content);
        }

 }


    

