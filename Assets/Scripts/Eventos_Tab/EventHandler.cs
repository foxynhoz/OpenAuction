using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class EventHandler : MonoBehaviour
{
    [SerializeField] InputField fileNameInput;
    LotesHandler lotesHandler;
    ErrorHandler errorHandler;

    private void Start()
    {
        errorHandler = FindAnyObjectByType<ErrorHandler>();
        lotesHandler = FindAnyObjectByType<LotesHandler>();
    }
    public void novoLeilao()
    {
        if (string.IsNullOrEmpty(fileNameInput.text))
        {
            Debug.LogError("Nome do leilão não pode ser vazio.");
            errorHandler.showError("Nome do leilão não pode ser vazio.");
            return;
        }

        if (File.Exists(Application.dataPath + "/LeilaoData/Leiloes/" + fileNameInput.text.ToLower() + ".json"))
        {
            Debug.LogError("Já existe um leilão com esse nome. Escolha outro nome ou exclua o leilão existente.");
            errorHandler.showError("Já existe um leilão com esse nome. Escolha outro nome ou exclua o leilão existente.");
            return;
        }

        lotesHandler.leilaoAtivo = fileNameInput.text;
        lotesHandler.lotes.Clear();
        lotesHandler.SalvarLista();
    }

    public void Delete()
    {
        EventButtonData data = GetComponent<EventButtonData>();
        if (data == null)
        {
            Debug.LogError("EventButtonData component not found on the GameObject.");
            return;
        }

        string filePath = data.filePath;

        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
                Debug.Log($"File deleted successfully: {filePath}");
                lotesHandler.leilaoAtivo = "";
                try
                {
                    File.Delete(filePath + ".meta");
                }
                catch (IOException ex)
                {
                    Debug.LogError($"There is no Meta File to be deleted:");
                }
            }
            catch (IOException ex)
            {
                Debug.LogError($"Error deleting file: {ex.Message}");
            }
        }
        else
        {
            Debug.LogWarning($"File not found: {filePath}");
        }

        Destroy(data.gameObject);
    }
}
