using UnityEngine;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class OBSConnection : MonoBehaviour
{
    private ClientWebSocket ws;

    async void Start()
    {
        ws = new ClientWebSocket();

        Uri uri = new Uri("ws://127.0.0.1:4455");

        try
        {
            await ws.ConnectAsync(uri, CancellationToken.None);
            Debug.Log("✅ Conectado ao OBS!");

            // iniciar loop de recebimento
            ReceiveLoop();
        }
        catch (Exception e)
        {
            Debug.LogError("❌ Erro: " + e.Message);
        }
    }

    async void ReceiveLoop()
    {
        byte[] buffer = new byte[4096];
        await SendRequest();

        while (ws != null && ws.State == WebSocketState.Open)
        {
            var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                Debug.Log("🛑 Conexão fechada pelo servidor");
                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                break;
            }

            string msg = Encoding.UTF8.GetString(buffer, 0, result.Count);

            Debug.Log("📩 Recebido: " + msg);
        }
    }

    async Task SendRequest()
    {
        // JSON corretamente escapado e com requestData como objeto
        string json = "{" +
            "\"op\": 6," +
            "\"d\": {" +
                "\"requestType\": \"GetSceneList\"," +
                "\"requestId\": \"1\"," +
                "\"requestData\": {\"sceneName\": \"Cena 2\"}" +
            "}" +
        "}";

        byte[] bytes = Encoding.UTF8.GetBytes(json);

        await ws.SendAsync(
            new ArraySegment<byte>(bytes),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None
        );

        Debug.Log("📤 Enviado: " + json);
    }

    private void OnDestroy()
    {
        if (ws != null)
        {
            try
            {
                ws.Abort();
                ws.Dispose();
            }
            catch { }
        }
    }
}