using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Lance_Atual : MonoBehaviour
{
    FileHandler fileHandler = new FileHandler();
    ErrorHandler ErrorHandler;
    
    public Text LanceAtual_TXT;
    public Text LanceAnterior_TXT;

    public InputField I_Lance; 
    public InputField I_EntreLance;

    public float entreLance = 10f;
    public float LanceValue = 0f;

    void Start()
    {
        ErrorHandler = GetComponent<ErrorHandler>();
        I_Lance.text = (LanceValue + entreLance).ToString();
        I_EntreLance.text = entreLance.ToString();

        fileHandler.UpdateFile("LanceAtual.txt", LanceValue.ToString());
    }

    void Update()
    {
        LanceAtual_TXT.text = LanceValue.ToString();      
    }

    public void changeLance() //trocar o lance atual pelo valor do input field
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
                fileHandler.UpdateFile("LanceAtual.txt", LanceValue.ToString());
            }
            else
            {
                ErrorHandler.showError("Valor do lance deve ser maior que o lance atual.");
                Debug.Log("Valor do lance deve ser maior que o lance atual.");
            }
        }
        catch
        {
            ErrorHandler.showError("Valor do lance inválido.");
            Debug.Log("Valor do lance inválido.");
        }       
    }

    public void desfazerLance() //trocar o lance atual pelo lance anterior
    {
        LanceValue = float.Parse(LanceAnterior_TXT.text);
        I_Lance.text = (LanceValue + entreLance).ToString();
        fileHandler.UpdateFile("LanceAtual.txt", LanceValue.ToString());
    }

    public void changeEntreLance() //trocar o valor do entre lance
    {
        entreLance = float.Parse(I_EntreLance.text);
        I_EntreLance.text = entreLance.ToString();
    }
    
    public void Vendido()
    {
        LanceValue = 0f;
        I_Lance.text = (LanceValue + entreLance).ToString();
        fileHandler.UpdateFile("LanceAtual.txt", LanceValue.ToString());
    }

    
}