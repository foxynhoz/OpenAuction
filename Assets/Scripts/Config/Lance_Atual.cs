using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Lance_Atual : MonoBehaviour
{
    public Text LanceAtual_TXT;
    public Text EntreLance_TXT;

    public float entreLance = 10f;
    public float LanceValue = 0f;

    void Start()
    {
        File.WriteAllText("LanceAtual.txt", LanceValue.ToString());
    }

    void Update()
    {
        EntreLance_TXT.text = entreLance.ToString();
        LanceAtual_TXT.text = LanceValue.ToString();
    }

    public void addLance()
    {
        LanceValue += entreLance;
        File.WriteAllText("LanceAtual.txt", LanceValue.ToString());

    }
    public void subLance()
    {
        if (LanceValue > 0f)
        {
            LanceValue -= entreLance;
            File.WriteAllText("LanceAtual.txt", LanceValue.ToString());
            if (LanceValue < 0f)
            {
                LanceValue = 0f;
            }
        }
    }

    public void changeEntreLance()
    {
        entreLance = float.Parse(EntreLance_TXT.text);
    }
}
