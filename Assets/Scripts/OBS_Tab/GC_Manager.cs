using UnityEngine;
using System.IO;
using System.Globalization;
using System.Collections;
using UnityEngine.UI;
using NUnit.Framework.Constraints;

public class GC_Manager : MonoBehaviour
{
    GCStyle gcStyle = new GCStyle();
    FileHandler fileHandler = new FileHandler();

    [SerializeField] Slider FontSize_Slider;
    [SerializeField] Slider GCwidth_Slider;
    [SerializeField] Slider BorderRadius_Slider;
    [SerializeField] Slider PaddingX_Slider;
    [SerializeField] Slider PaddingY_Slider;
    [SerializeField] Slider SepSize_Slider;

    [SerializeField] Slider R_Slider;
    [SerializeField] Slider G_Slider;
    [SerializeField] Slider B_Slider;
    [SerializeField] Slider A_Slider;


    public int fontsize = 20;
    public int GCwidth = 1000;
    public int borderRadius = 0;
    public int paddingX = 0;
    public int paddingY = 0;
    public int sepSize = 20;

    public Color color;

    // Variáveis para armazenar o conteúdo do HTML e do JSON
    string HTML;
    string GCStyleJSON;

    void Start()
    {
        HTML = File.ReadAllText(Application.streamingAssetsPath + "/GCLote.html");

        if (!File.Exists(fileHandler.GetFolderPath("Data") + "GCStyle.json") || !File.Exists(fileHandler.GetFolderPath("Data") + "GCLote.html"))
        {
            UpdateHTML_and_JSON_Data();
        }

        LoadGCStyle();

        UpdateHTML_and_JSON_Data();
        StartCoroutine(UpdateStyle());
    }

    void Update()
    {
        fontsize = (int)FontSize_Slider.value;
        GCwidth = (int)GCwidth_Slider.value;
        borderRadius = (int)BorderRadius_Slider.value;
        paddingX = (int)PaddingX_Slider.value;
        paddingY = (int)PaddingY_Slider.value;
        sepSize = (int)SepSize_Slider.value;
        color = new Color(R_Slider.value, G_Slider.value, B_Slider.value,A_Slider.value);
    }

    [ContextMenu("Update HTML e JSON Style")]
    public void UpdateHTML_and_JSON_Data()
    {
        HTML = File.ReadAllText(Application.streamingAssetsPath + "/GCLote.html");

        GCStyleJSON = @"
    {
        ""fontSize"": " + fontsize + @",
        ""opacity"": 1.0,
        ""GCwidth"": " + GCwidth + @",

        ""paddingX"": " + paddingX + @",
        ""paddingY"": " + paddingY + @",
        ""borderRadius"": " + borderRadius + @",        
        ""boxColor"": ""rgba("+ color.r + "," + color.g + "," + color.b + "," + color.a.ToString(CultureInfo.InvariantCulture) + @")"",
            
        ""separator"": ""•"",
        ""sepSize"": " + sepSize + @"
    }";


        fileHandler.UpdateFile("GCStyle.json", GCStyleJSON, "Data", false);
        fileHandler.UpdateFile("GCLote.html", HTML, "Data", false);
        
    }

    private void OnApplicationQuit()
    {
        SaveGCStyle();
    }
    IEnumerator UpdateStyle()
    {
        while (true)
        {
            UpdateHTML_and_JSON_Data();
            yield return new WaitForSeconds(0.2f);
        }
    }
    void LoadGCStyle()
    {
        if (File.Exists(fileHandler.GetFolderPath("Data") + "GCStyle.json"))
        {
            string json = File.ReadAllText(fileHandler.GetFolderPath("Data") + "GCStyle.json");
            gcStyle = JsonUtility.FromJson<GCStyle>(json);
                FontSize_Slider.value = gcStyle.fontSize;
                PaddingX_Slider.value = gcStyle.paddingX;
                PaddingY_Slider.value = gcStyle.paddingY;
                BorderRadius_Slider.value = gcStyle.borderRadius;
                GCwidth_Slider.value = gcStyle.GCwidth;
                SepSize_Slider.value = gcStyle.sepSize;
                    string[] rgba = gcStyle.boxColor.Replace("rgba(", "").Replace(")", "").Split(',');
                    if (rgba.Length == 4)
                    {
                        R_Slider.value = float.Parse(rgba[0]);
                        G_Slider.value = float.Parse(rgba[1]);
                        B_Slider.value = float.Parse(rgba[2]);
                        A_Slider.value = float.Parse(rgba[3], CultureInfo.InvariantCulture);
                    }
        }
        else
        {
            Debug.LogWarning("GCStyle.json não encontrado. Usando valores padrão.");
                FontSize_Slider.value = fontsize;
                PaddingX_Slider.value = paddingX;
                PaddingY_Slider.value = paddingY;
                BorderRadius_Slider.value = borderRadius;
                GCwidth_Slider.value = GCwidth;
                SepSize_Slider.value = sepSize;
                R_Slider.value = color.r;
                G_Slider.value = color.g;
                B_Slider.value = color.b;
                A_Slider.value = color.a;
        }
    }
    void SaveGCStyle()
    {
        gcStyle.fontSize = (int)FontSize_Slider.value;
        gcStyle.paddingX = (int)PaddingX_Slider.value;
        gcStyle.paddingY = (int)PaddingY_Slider.value;
        gcStyle.borderRadius = (int)BorderRadius_Slider.value;
        gcStyle.GCwidth = (int)GCwidth_Slider.value;
        gcStyle.sepSize = (int)SepSize_Slider.value;
        gcStyle.boxColor = "rgba(" + color.r + "," + color.g + "," + color.b + "," + color.a.ToString(CultureInfo.InvariantCulture) + ")";
        string json = JsonUtility.ToJson(gcStyle, true);
        fileHandler.UpdateFile("GCStyle.json", json, "Data", false);
    }
}

public class GCStyle
{
    public int fontSize;
    public float opacity;
    public int paddingX;
    public int paddingY;
    public int borderRadius;
    public int GCwidth;
    public string boxColor;
    public string separator;
    public int sepSize;
}