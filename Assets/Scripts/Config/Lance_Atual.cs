using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Lance_Atual : MonoBehaviour
{
    [SerializeField] Text ErrorMessage;

    public Text LanceAtual_TXT;
    public Text LanceAnterior_TXT;

    public InputField I_Lance; 
    public InputField I_EntreLance;

    public float entreLance = 10f;
    public float LanceValue = 0f;

    void Start()
    {
        I_Lance.text = (LanceValue + entreLance).ToString();
        I_EntreLance.text = entreLance.ToString();

        UpdateFile();
    }

    void Update()
    {
        LanceAtual_TXT.text = LanceValue.ToString();      
    }

    public void changeLance()
    {
        try
        {
            if (float.Parse(I_Lance.text) > LanceValue)
            {
                LanceAnterior_TXT.text = LanceValue.ToString();
                LanceValue = float.Parse(I_Lance.text);
                I_Lance.text = (LanceValue + entreLance).ToString();
                I_Lance.Select();
                I_Lance.ActivateInputField();
                UpdateFile();
            }
            else
            {
                ErrorMessage.text = "⚠Valor do lance deve ser maior que o lance atual.";
                StartCoroutine(ShowError());
                Debug.Log("Valor do lance deve ser maior que o lance atual.");
            }
        }
        catch
        {
            ErrorMessage.text = "⚠Valor do lance inválido.";
            StartCoroutine(ShowError());
            Debug.Log("Valor do lance inválido.");
        }       
    }

    public void desfazerLance()
    {
        LanceValue = float.Parse(LanceAnterior_TXT.text);
        I_Lance.text = (LanceValue + entreLance).ToString();
        UpdateFile();
    }

    public void changeEntreLance()
    {
        entreLance = float.Parse(I_EntreLance.text);
        I_EntreLance.text = entreLance.ToString();
    }
    
    public void Vendido()
    {
        LanceValue = 0f;
        I_Lance.text = (LanceValue + entreLance).ToString();
        UpdateFile();
    }

    private void UpdateFile()
    {
        string dataDir = Application.dataPath + "/LeilaoData";
        if (!Directory.Exists(dataDir))
        {
            Directory.CreateDirectory(dataDir);
        }

        File.WriteAllText(dataDir + "/LanceAtual.txt", LanceValue.ToString());
    }

    IEnumerator ShowError()
    {
        ErrorMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        ErrorMessage.gameObject.SetActive(false);
    }
}
