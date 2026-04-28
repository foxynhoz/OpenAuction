using System.Net.WebSockets;
using UnityEngine;

public class OBSActionManager : MonoBehaviour
{
    OBSWebSocket obs;

    private void Start()
    {
        obs = GetComponent<OBSWebSocket>();
    }

    void Update()
    {
        
    }

    [ContextMenu("Toggle Studio Mode")]
    public void TriggerToggleStudioMode()
    {
        _ = obs.ToggleStudioMode();
    }

    public int delay = 3000;


    public void ConnectButton()
    {
        if (string.IsNullOrEmpty(obs.IPfield.text))
        {
            Debug.LogError("IP do OBS não pode ser vazio");
            return;
        }
        if (obs.ws == null || obs.ws.State == WebSocketState.Closed)
        {
            _ = obs.Connect(obs.IPfield.text);
        }
        else
        {
            _ = obs.Disconect();
        }

    }
}

