using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Overlay_Updaters : MonoBehaviour
{
    

    [SerializeField] Text LoteNumber;
    [SerializeField] Text LoteName;
    [SerializeField] Text LanceValue;
    FileHandler fileHandler = new FileHandler();

    private void Start()
    {
        
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "OBS_Overlay")
        {
            LoteNumber.text = File.ReadAllText(Application.dataPath + "/LeilaoData/OBS_Stuff/LoteID.txt");
            LoteName.text = File.ReadAllText(Application.dataPath + "/LeilaoData/OBS_Stuff/nome.txt");
            LanceValue.text = File.ReadAllText(Application.dataPath + "/LeilaoData/LanceAtual.txt");
        }

        if(SceneManager.GetActiveScene().name == "Main_Window")
        {

        }

    }

    public void ChangeToOverlay()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        Screen.SetResolution(1920, 1080, false);
        SceneManager.LoadScene("OBS_Overlay");
    }
}
