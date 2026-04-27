using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class OBSWebSocket : MonoBehaviour
{
    ClientWebSocket ws;
    [SerializeField] InputField IPfield;
    [SerializeField] Text statusText;

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

    async Task Connect(string ip)
    {
        ws = new ClientWebSocket();
        Uri uri = new Uri(ip);
        await ws.ConnectAsync(uri, CancellationToken.None);
        Debug.Log("Conectado ao OBS");

        await Receive(); // recebe o Hello
        await Identify(); // envia Identify e recebe Identified
    }

    async Task Disconect()
    {
        if (ws != null && ws.State == WebSocketState.Open)
        {
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Desconectando", CancellationToken.None);
            Debug.Log("Desconectado do OBS");
        }
    }

    async Task Identify()
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


    async Task Send(string message)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(message);
        await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    async Task<string> Receive()
    {
        var buffer = new byte[1024];
        var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        string msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
        Debug.Log(JObject.Parse(msg));
        return msg;
    }

    //FUNÇOES DOS BOTOES

    [ContextMenu("Toggle Studio Mode")]
    public void TriggerToggleStudioMode()
    {
        _ = ToggleStudioMode();
    }

    public int delay = 3000;


    public void ConnectButton()
    {
        if (string.IsNullOrEmpty(IPfield.text))
        {
            Debug.LogError("IP do OBS não pode ser vazio");
            return;
        }
        if (ws == null || ws.State == WebSocketState.Closed)
        {
            _ = Connect(IPfield.text);
        }
        else
        {
            _ = Disconect();
        }

    }
    ////////////////////////////////////////////////////////

    async Task TriggerTransition()
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
    async Task GetSceneList()
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

    async Task ToggleStudioMode()
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



    public async Task ToggleSource(string sceneName, string sourceName, bool visible)
    {
        // 1. Pega lista de itens da cena
        string getListJson = $@"{{
        ""op"": 6,
        ""d"": {{
            ""requestType"": ""GetSceneItemList"",
            ""requestId"": ""getItems"",
            ""requestData"": {{
                ""sceneName"": ""{sceneName}""
            }}
        }}
    }}";

        await Send(getListJson);
        string resposta = await Receive();

        // 2. Procura o ID da source pelo nome
        int sceneItemId = -1;

        string[] split = resposta.Split('{');

        foreach (var part in split)
        {
            if (part.Contains($@"""sourceName"":""{sourceName}"""))
            {
                int idIndex = part.IndexOf("sceneItemId");
                int start = part.IndexOf(":", idIndex) + 1;
                int end = part.IndexOf(",", start);

                string idStr = part.Substring(start, end - start).Trim();
                int.TryParse(idStr, out sceneItemId);
                break;
            }
        }

        if (sceneItemId == -1)
        {
            Debug.LogError("Source não encontrada");
            return;
        }

        // 3. Liga/Desliga
        string toggleJson = $@"{{
        ""op"": 6,
        ""d"": {{
            ""requestType"": ""SetSceneItemEnabled"",
            ""requestId"": ""toggle"",
            ""requestData"": {{
                ""sceneName"": ""{sceneName}"",
                ""sceneItemId"": {sceneItemId},
                ""sceneItemEnabled"": {visible.ToString().ToLower()}
            }}
        }}
    }}";

        await Send(toggleJson);

    }
}


//MAGIA NEGRA
public class Root
{
    public D d { get; set; }
}

public class D
{
    public ResponseData responseData { get; set; }
}

public class ResponseData
{
    public List<Scene> scenes { get; set; }
}

public class Scene
{
    public int sceneIndex { get; set; }
    public string sceneName { get; set; }
    public string sceneUuid { get; set; }
}