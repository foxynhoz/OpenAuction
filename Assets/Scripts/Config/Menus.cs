using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Menus : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] GameObject Event_Panel;
    [SerializeField] GameObject Lotes_Panel;
    [SerializeField] GameObject MesaOP_Panel;
    [SerializeField] GameObject OBS_Panel;
    [SerializeField] GameObject Info_Panel;
    public void OnSelect(BaseEventData eventData)
    {
        switch(gameObject.name)
        {
            case "EVENTO_BUTTON":
                Event_Panel.SetActive(true);
                Debug.Log("EVENTO");
                break;
            case "LOTES_BUTTON":
                Lotes_Panel.SetActive(true);
                Debug.Log("Lotes");
                break;
            case "MESAOP_BUTTON":
                MesaOP_Panel.SetActive(true);
                Debug.Log("Configurações");
                break;
            case "OBSCONFIG_BUTTON":
                OBS_Panel.SetActive(true);
                Debug.Log("OBS");
                break;
            case "SOBRE_BUTTON":
                Info_Panel.SetActive(true);
                Debug.Log("Info");
                break;
            default:
                Debug.Log("Outro botão");
                break;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        switch (gameObject.name)
        {
            case "MESAOP_BUTTON":
                MesaOP_Panel.SetActive(false);
                Debug.Log("Configurações");
                break;
            case "EVENTO_BUTTON":
                Event_Panel.SetActive(false);
                Debug.Log("EVENTO");
                break;
            case "LOTES_BUTTON":
                Lotes_Panel.SetActive(false);
                Debug.Log("Lotes");
                break;
            case "OBSCONFIG_BUTTON":
                OBS_Panel.SetActive(false);
                Debug.Log("OBS");
                break;
            case "SOBRE_BUTTON":
                Info_Panel.SetActive(false);
                Debug.Log("Info");
                break;
        }
    }
}
