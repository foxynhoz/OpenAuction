using UnityEngine;

public class EventButtonData : MonoBehaviour
{
    public string filePath;
    public string eventName;

    public void SetActiveEvent()
    {
        LotesHandler lotesHandler = FindObjectOfType<LotesHandler>();
        lotesHandler.CarregarLista(eventName, filePath); // Carrega a lista atual antes de definir o leilão ativo

        // Aqui você pode implementar a lógica para carregar o JSON usando o filePath
        Debug.Log("Botão clicado! Caminho do arquivo: " + filePath);
        // Exemplo: Carregar o JSON e exibir os dados
        // string jsonText = System.IO.File.ReadAllText(filePath);
        // JsonData data = JsonUtility.FromJson<JsonData>(jsonText);
        // Faça algo com os dados carregados...
    }
}
