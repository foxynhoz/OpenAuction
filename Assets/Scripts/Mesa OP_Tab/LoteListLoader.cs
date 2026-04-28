using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoteListLoader : MonoBehaviour
{
    [Header("Pasta dos JSONs")]
    string defaultFolderPath = "/LeilaoData/Leiloes"; // Caminho padrão
    string folderPath = "/LeilaoData/Leiloes/";

    [Header("UI")]
    public Transform contentParent; // Content do Scroll View
    public GameObject buttonPrefab; // Prefab do botão

    LotesHandler listaHandler;

    public void RefreshLoteList()
    { 
        listaHandler = FindObjectOfType<LotesHandler>();

        LoadLoteJSONButtons(Application.dataPath + folderPath + listaHandler.leilaoAtivo.ToLower() + ".json");
    }

    public void LoadLoteJSONButtons(string filePath)
    {
        Debug.Log("Carregando JSON: " + filePath);
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        string jsonText = File.ReadAllText(filePath);
        JsonData data = JsonUtility.FromJson<JsonData>(jsonText);

        if (data == null || data.animais == null)
        {
            Debug.LogWarning("JSON vazio ou inválido");
            return;
        }

        foreach (var animal in data.animais)
        {
            GameObject btnObj = Instantiate(buttonPrefab, contentParent);
            LoteButtonData loteData = btnObj.GetComponent<LoteButtonData>();

            loteData.LoteID = animal.loteID;

            Text txt = btnObj.GetComponentInChildren<Text>();

            if (txt != null)
                txt.text = animal.loteID.ToString();
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
}
