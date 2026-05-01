using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Threading.Tasks;
using Unity.VisualScripting;
using Unity.IO.LowLevel.Unsafe;
using System.Collections.Generic;

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

public class TimerManager : MonoBehaviour
{
    IEnumerator Timer(int delay)
    {
        yield return new WaitForSeconds(delay);
    }
}

public class FileHandler
{

    static string dataDir = Application.dataPath + "/LeilaoData/";
    static string leiloesDir = dataDir + "/Leiloes/";
    static string obsDir = dataDir + "/OBS_Stuff/";
    static string videosDir = dataDir + "/Videos/";

    int tries = 0;

    public void UpdateFile(string fileName, string content, string folderName, bool DebugLog) //atualizar o arquivo passado com o conteúdo passado
    {
        switch (folderName)
        {
            case "Data":
                if (!Directory.Exists(dataDir))
                {
                    Directory.CreateDirectory(dataDir);
                }
                try
                {
                    File.WriteAllText(dataDir + fileName, content);
                    tries = 0;
                    break;
                }
                catch (IOException e)
                {
                    if (tries < 50)
                    {
                        tries++;
                        Debug.LogError(tries + " Error writing file, Trying again...");
                        UpdateFile(fileName, content, folderName, DebugLog);
                    }
                    else
                    {
                        Debug.LogError("Error writing file after 50 attempts: " + e.Message);
                    }
                    break;
                }
            case "OBS_Stuff":
                if (!Directory.Exists(obsDir))
                {
                    Directory.CreateDirectory(obsDir);
                }
                try
                {
                    File.WriteAllText(obsDir + fileName, content);
                    tries = 0;
                    break;
                }
                catch (IOException e)
                {
                    if (tries < 50)
                    {
                        tries++;
                        Debug.LogError(tries + " Error writing file, Trying again...");
                        UpdateFile(fileName, content, folderName, DebugLog);
                    }
                    else
                    {
                        Debug.LogError("Error writing file after 50 attempts: " + e.Message);
                    }
                    break;
                }
            case "Leiloes":
                if (!Directory.Exists(leiloesDir))
                {
                    Directory.CreateDirectory(leiloesDir);
                }
                try
                {
                    File.WriteAllText(leiloesDir + fileName, content);
                    tries = 0;
                    break;
                }
                catch (IOException e)
                {
                    if (tries < 50)
                    {
                        tries++;
                        Debug.LogError(tries + " Error writing file, Trying again...");
                        UpdateFile(fileName, content, folderName, DebugLog);
                    }
                    else
                    {
                        Debug.LogError("Error writing file after 50 attempts: " + e.Message);
                    }
                    break;
                }

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


    

