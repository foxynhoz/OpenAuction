using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class OBSWebSocket : MonoBehaviour
{
    public ClientWebSocket ws;
    [SerializeField] public InputField IPfield;
    [SerializeField] public Text statusText;

    private void Update()
    {
        if (ws != null && ws.State == WebSocketState.Open)
        {
            statusText.text = $"OBS STATUS: Conectado";
            statusText.color = Color.green;
        }
        else
        {
            statusText.text = "OBS STATUS: Desconectado" + "🔴";
            statusText.color = Color.red;
        }
    }

    public async Task Connect(string ip)
    {
        ws = new ClientWebSocket();
        Uri uri = new Uri(ip);
        await ws.ConnectAsync(uri, CancellationToken.None);
        Debug.Log("Conectado ao OBS");

        await Receive(); // recebe o Hello
        await Identify(); // envia Identify e recebe Identified
    }

    public async Task Disconect()
    {
        if (ws != null && ws.State == WebSocketState.Open)
        {
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Desconectando", CancellationToken.None);
            Debug.Log("Desconectado do OBS");
        }
    }

    public async Task Identify()
    {
        string json = @"{
            ""op"": 1,
            ""d"": {
                ""rpcVersion"": 1
            }
        }";

        await Send(json);
        await Receive(); // recebe Identified
    }


    public async Task Send(string message)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(message);
        await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    public async Task<string> Receive()
    {
        var buffer = new byte[1024];
        var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        string msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
        Debug.Log(JObject.Parse(msg));
        return msg;
    }


    public async Task TriggerTransition()
    {
        string json = @"{
            ""op"": 6,
            ""d"": {""requestType"": ""TriggerStudioModeTransition"",
            ""requestId"": ""1""
            }
        }";

        await Send(json);
        Debug.Log("Transição enviada");
    }

    [ContextMenu("Get Scene List")]
    public async Task GetSceneList()
    {
        string json = @"{
            ""op"": 6,
            ""d"": {""requestType"": ""GetSceneList"",
            ""requestId"": ""1""
            }
        }";

        await Send(json);
        Debug.Log("Get Scene List enviado");
        await Receive(); // recebe a lista de cenas
        
    }

    public async Task ToggleStudioMode()
    {
        string json = @"{
          ""op"": 6,
          ""d"": {
            ""requestType"": ""SetStudioModeEnabled"",
            ""requestId"": ""1"",
            ""requestData"": {
                ""studioModeEnabled"": true
            }
          }
        }";
        await Send(json);
        Debug.Log("Toggle Studio Mode enviado");
    }
}
