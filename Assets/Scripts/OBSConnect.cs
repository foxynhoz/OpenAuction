using JetBrains.Annotations;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class OBSWebSocket : MonoBehaviour
{
    ClientWebSocket ws;

    async void Start()
    {
        await Connect();
        await Identify();
    }

    async Task Connect()
    {
        ws = new ClientWebSocket();
        Uri uri = new Uri("ws://127.0.0.1:4455");
        await ws.ConnectAsync(uri, CancellationToken.None);
        Debug.Log("Conectado ao OBS");

        await Receive(); // recebe o Hello
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

    async Task TriggerTransition()
    {
        string json = @"{
            ""op"": 6,
            ""d"": {""requestType"": ""TriggerStudioModeTransition"",""requestId"": ""1""
            }
        }";

        await Send(json);
        Debug.Log("Transição enviada");
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
        Debug.Log("Recebido: " + msg);
        return msg;
    }

    //FUNÇOES DOS BOTOES
    public int delay = 3000;
    public void Transition()
    {
        _ = TransitionTask(delay);
    }

    async Task TransitionTask(int delay)
    {
        await SetCena("Transicao");

        await Task.Delay(delay);

        await SetCena("Cena B");
    }

    async Task SetCena(string nome)
    {
        string json = $@"{{
        ""op"": 6,
        ""d"": {{
            ""requestType"": ""SetCurrentProgramScene"",
            ""requestId"": ""setScene"",
            ""requestData"": {{
                ""sceneName"": ""{nome}""
            }}
        }}
    }}";

        await Send(json);
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