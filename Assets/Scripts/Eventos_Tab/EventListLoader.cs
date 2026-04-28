using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SFB;

public class EventListLoader : MonoBehaviour
{
    [Header("Pasta dos JSONs")]
    public string defaultFolderPath = "LeilaoData/Leiloes"; // Caminho padrão
    public string folderPath = "LeilaoData/Leiloes";

    [Header("UI")]
    public Transform contentParent; // Content do Scroll View
    public GameObject buttonPrefab; // Prefab do botão

    void Start()
    {
        LoadEventoJSONButtons();
    }

    public void LoadEventoJSONButtons()
    {
        string fullPath = Path.Combine(Application.dataPath, folderPath);
        string[] files = Directory.GetFiles(fullPath, "*.json");

        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (string file in files)
        {
            string jsonText = File.ReadAllText(file);
            JsonData data = JsonUtility.FromJson<JsonData>(jsonText);

            string fileName = Path.GetFileNameWithoutExtension(file);


            int itemCount = 0;
            if (data != null && data.animais != null)
                itemCount = data.animais.Length;

            GameObject btnObj = Instantiate(buttonPrefab, contentParent);
            EventButtonData EventData = btnObj.GetComponent<EventButtonData>();

            EventData.filePath = file; // caminho completo do JSON
            EventData.eventName = fileName; // nome do evento

            Text[] texts = btnObj.GetComponentsInChildren<Text>();

            // assumindo:
            // texts[0] = contador
            // texts[1] = nome

            if (texts.Length >= 2)
            {
                texts[1].text = fileName.ToUpper(); // nome do JSON
                texts[0].text = itemCount.ToString(); // aqui o número de itens
            }
        }
    }
}

[System.Serializable]
public class JsonData
{
    public LoteData[] animais;
}

[System.Serializable]
public class Item
{
    public int id;
}
