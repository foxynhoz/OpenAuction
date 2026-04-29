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
        string leiloesDir = dataDir + "/Leiloes/";
        string obsDir = dataDir + "/OBS_Stuff/";
        string videosDir = dataDir + "/Videos/";

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
    static string leiloesDir = dataDir + "/Leiloes/";
    static string obsDir = dataDir + "/OBS_Stuff/";
    static string videosDir = dataDir + "/Videos/";

    public void UpdateFile(string fileName, string content, string folderName, bool DebugLog) //atualizar o arquivo passado com o conteúdo passado
    {
        switch (folderName)
        {
            case "Data":
                if (!Directory.Exists(dataDir))
                {
                    Directory.CreateDirectory(dataDir);
                }
                File.WriteAllText(dataDir + fileName, content);
                break;
            case "OBS_Stuff":
                if (!Directory.Exists(obsDir))
                {
                    Directory.CreateDirectory(obsDir);
                }
                File.WriteAllText(obsDir + fileName, content);
                break;
            case "Leiloes":
                if (!Directory.Exists(leiloesDir))
                {
                    Directory.CreateDirectory(leiloesDir);
                }
                File.WriteAllText(leiloesDir + fileName, content);
                break;

            case "Videos":
                if (!Directory.Exists(videosDir))
                {
                    Directory.CreateDirectory(videosDir);
                }
                File.WriteAllText(videosDir + fileName, content);
                break;

            default:
                Debug.LogError("Invalid folder name: " + folderName);
                return;
        }


        switch (DebugLog)
        {
            case true:
                Debug.Log("Updating file " + fileName + " in " + folderName);
                break;
            case false:
                break;
        }
    }

    public string GetFolderPath(string folderName)
    {
        switch (folderName)
        {
            case "Data":
                return dataDir;
            case "OBS_Stuff":
                return obsDir;
            case "Leiloes":
                return leiloesDir;
            case "Videos":
                return videosDir;
            default:
                Debug.LogError("Invalid folder name: " + folderName);
                return null;
        }
    }
}


    

