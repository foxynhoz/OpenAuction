using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoteListLoader : MonoBehaviour

{   FileHandler fileHandler = new FileHandler();

    [Header("UI")]
    public Transform contentParent; // Content do Scroll View
    public GameObject buttonPrefab; // Prefab do botão

    [SerializeField] LotesHandler lotesHandler;

    public void RefreshLoteList()
    { 
        lotesHandler.CarregarLista(
            lotesHandler.leilaoAtivo, fileHandler.GetFolderPath("Leiloes") + lotesHandler.leilaoAtivo.ToLower() + ".json");
        LoadLoteJSONButtons();
    }

    public void LoadLoteJSONButtons()
    {
        if(string.IsNullOrEmpty(lotesHandler.leilaoAtivo))
        {
            return;
        }


        JsonData data = JsonUtility.FromJson<JsonData>
            (File.ReadAllText(fileHandler.GetFolderPath("Leiloes") + lotesHandler.leilaoAtivo.ToLower() + ".json")
            );

        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

       

        if (data == null || data.animais == null)
        {
            Debug.LogWarning("JSON vazio ou inválido");
            return;
        }

        foreach (var animal in data.animais)
        {
            GameObject btnObj = Instantiate(buttonPrefab, contentParent);
            LoteButtonData loteButtonData = btnObj.GetComponent<LoteButtonData>();
            loteButtonData.lotesHandler = lotesHandler;

            loteButtonData.hashID = animal.hashID;
            loteButtonData.fileName = lotesHandler.leilaoAtivo.ToLower() + ".json";
            loteButtonData.filePath = fileHandler.GetFolderPath("Leiloes") + loteButtonData.fileName;

            loteButtonData.LoteID = animal.loteID;
            loteButtonData.brinco = animal.brinco;
            loteButtonData.nome = animal.nome;
            loteButtonData.infoExtras = animal.infoExtras;
            loteButtonData.sexo = animal.sexo;
            loteButtonData.idade = animal.idade;
            loteButtonData.sangue = animal.sangue;
            loteButtonData.nascimento = animal.nascimento;
            loteButtonData.ultimoParto = animal.ultimoParto;
            loteButtonData.prevParto = animal.prevParto;
            loteButtonData.producao = animal.producao;
            loteButtonData.peso = animal.peso;
            loteButtonData.pai = animal.pai;
            loteButtonData.mae = animal.mae;

            Text[] txts = btnObj.GetComponentsInChildren<Text>();

            if (txts.Length >= 2)
            {
                txts[1].text = animal.nome.ToUpper(); // nome do JSON
                txts[0].text = animal.loteID.ToString(); // aqui o número de itens
            }
            txts[0].text = animal.loteID.ToString(); // aqui o número de itens
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
