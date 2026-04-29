using UnityEngine;
using System.IO;
using System.Globalization;
using System.Collections;
using UnityEngine.UI;
using NUnit.Framework.Constraints;

public class GC_Manager : MonoBehaviour
{
    FileHandler fileHandler = new FileHandler();

    [SerializeField] Slider FontSize_Slider;
    [SerializeField] Slider GCwidth_Slider;
    [SerializeField] Slider BorderRadius_Slider;
    [SerializeField] Slider PaddingX_Slider;
    [SerializeField] Slider PaddingY_Slider;

    [SerializeField] Slider R_Slider;
    [SerializeField] Slider G_Slider;
    [SerializeField] Slider B_Slider;
    [SerializeField] Slider A_Slider;

    [Range(10, 50)]
    public int fontsize = 20;

    [Range(100, 2000)]
    public int GCwidth = 1000;
    
    [Range(0, 100)]
    public int borderRadius = 0;
    
    [Range(0, 100)]
    public int paddingX = 0;

    [Range(0, 100)]
    public int paddingY = 0;

    public Color color;

    string HTML = File.ReadAllText(Application.streamingAssetsPath + "/GCLote.html");

    void Start()
    {

        if (!File.Exists(Application.streamingAssetsPath + "/GCLote.html") || !File.Exists(Application.streamingAssetsPath + "/GCStyle.json"))
        {
            UpdateHTML_Data();
        }
        StartCoroutine(UpdateStyle());

    }

    void Update()
    {
        fontsize = (int)FontSize_Slider.value;
        GCwidth = (int)GCwidth_Slider.value;
        borderRadius = (int)BorderRadius_Slider.value;
        paddingX = (int)PaddingX_Slider.value;
        paddingY = (int)PaddingY_Slider.value;
        color = new Color(R_Slider.value, G_Slider.value, B_Slider.value,A_Slider.value);
    }

    [ContextMenu("Update HTML e JSON Style")]
    public void UpdateHTML_Data()
    {

        string GCStyleJSON = @"
        {
            ""fontSize"": " + fontsize + @",
            ""opacity"": 1.0,
            ""paddingX"": " + paddingX + @",
            ""paddingY"": " + paddingY + @",
            ""borderRadius"": " + borderRadius + @",
            ""GCwidth"": " + GCwidth + @",
            ""bgColor"": ""rgba("+ color.r + "," + color.g + "," + color.b + "," + color.a.ToString(CultureInfo.InvariantCulture) + @")"",
            ""separator"": ""•""
        }";


        fileHandler.UpdateFile("GCStyle.json", GCStyleJSON, "Data", false);
        fileHandler.UpdateFile("GCLote.html", HTML, "Data", false);
        
    }

    IEnumerator UpdateStyle()
    {
        while (true)
        {
            UpdateHTML_Data();
            yield return new WaitForSeconds(0.2f);
        }
    }
}