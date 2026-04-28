using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Tab_Manager : MonoBehaviour
{
    /* Esse script é responsável por gerenciar a ativação e desativação 
     * dos painéis de cada aba.
     * 
     * 0 - Eventos
     * 1 - Lotes
     * 2 - MesaOP
     * 3 - OBS
     * 4 - Info
    */

    [SerializeField] GameObject Event_Panel;
    [SerializeField] GameObject Lotes_Panel;
    [SerializeField] GameObject MesaOP_Panel;
    [SerializeField] GameObject OBS_Panel;
    [SerializeField] GameObject Info_Panel;
    
  
    public void activatePanel(int panelIndex)
    {
        switch(panelIndex)
        {
            case 0:
                Event_Panel.SetActive(true);
                Lotes_Panel.SetActive(false);
                MesaOP_Panel.SetActive(false);
                OBS_Panel.SetActive(false);
                Info_Panel.SetActive(false);
                break;
            case 1:
                Event_Panel.SetActive(false);
                Lotes_Panel.SetActive(true);
                MesaOP_Panel.SetActive(false);
                OBS_Panel.SetActive(false);
                Info_Panel.SetActive(false);
                break;
            case 2:
                Event_Panel.SetActive(false);
                Lotes_Panel.SetActive(false);
                MesaOP_Panel.SetActive(true);
                OBS_Panel.SetActive(false);
                Info_Panel.SetActive(false);
                break;
            case 3:
                Event_Panel.SetActive(false);
                Lotes_Panel.SetActive(false);
                MesaOP_Panel.SetActive(false);
                OBS_Panel.SetActive(true);
                Info_Panel.SetActive(false);
                break;
            case 4:
                Event_Panel.SetActive(false);
                Lotes_Panel.SetActive(false);
                MesaOP_Panel.SetActive(false);
                OBS_Panel.SetActive(false);
                Info_Panel.SetActive(true);
                break;
        }
    }
}

