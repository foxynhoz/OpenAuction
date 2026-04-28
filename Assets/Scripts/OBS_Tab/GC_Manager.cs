using UnityEngine;
using System.IO;
using UnityEditor.ShaderGraph;
using System.Globalization;
using System.Collections;

public class GC_Manager : MonoBehaviour
{
    FileHandler fileHandler = new FileHandler();

    [Range(10, 50)]
    public int fontsize = 20;

    public Color color;

    string HTML = @"
    <!DOCTYPE html>
    <html>
    <head>
    <meta charset=""UTF-8"">

    <style>
    body {
        font-family: 'Arial', sans-serif;
        color: white;

        margin: 0;
        height: 100vh;

        display: flex;
        align-items: flex-end;
        justify-content: center;
    }

    .BottomInfo {
        position: absolute;
        bottom: 20px;

        display: flex;
        flex-wrap: wrap;
        align-items: center;
        justify-content: center;

        width: 1920px;
        font-size: 20px;
    }

    /* caixas */
    .item {
        background-color: 0;
        padding: 0;
        border-radius: 0;
        display: flex;
        align-items: center;
    }

    /* separador (AGORA FORA da caixa) */
    .sep {
	    transform: scale(2.0);
        color: white;
        margin: 0 10px;
        font-size: 25px;
        opacity: 1;
    }
    </style>

    </head>

    <body>

    <div class=""BottomInfo""></div>
    <script>
    async function atualizar() {
        // 🔹 dados (texto)
        const dataResponse = await fetch(""GCLote.json?t="" + Date.now());
        const data = await dataResponse.json();

        // 🔹 estilo (visual)
        const styleResponse = await fetch(""GCStyle.json?t="" + Date.now());
        const style = await styleResponse.json();

        const container = document.querySelector("".BottomInfo"");

        const itens = [
            data.idade,
            data.sexo,
            data.peso,
            data.sangue,
            data.nascimento,
            data.ultimoParto,
            data.prevParto,
            data.producao,
            data.mae,
            data.pai,
            data.infoExtras
        ].filter(v => v && v.trim() !== """");

        container.innerHTML = """";

        itens.forEach((texto, index) => {

            if (index > 0) {
                const sep = document.createElement(""span"");
                sep.className = ""sep"";
                sep.innerText = style.separator || ""•"";
                container.appendChild(sep);
            }

            const div = document.createElement(""div"");
            div.className = ""item"";
            div.innerText = texto;

            // 🎨 aplica estilo vindo do JSON
            div.style.backgroundColor = style.bgColor;
            div.style.borderRadius = style.borderRadius + ""px"";
            div.style.padding = style.paddingY + ""px "" + style.paddingX + ""px"";

            container.appendChild(div);
        });

        // 🎨 estilo geral
        container.style.fontSize = style.fontSize + ""px"";
        container.style.opacity = style.opacity;
    }
    setInterval(atualizar, 500);
    </script>

    </body>
    </html>";

    void Start()
    {
        if (!File.Exists(Application.dataPath + " / LeilaoData/GCLote.html") || !File.Exists(Application.dataPath + "/LeilaoData/GCStyle.json"))
        {
            UpdateHTML_Data();
        }
        StartCoroutine(UpdateStyle());
    }

    [ContextMenu("Update HTML e JSON Style")]
    public void UpdateHTML_Data()
    {

        string GCStyleJSON = @"
        {
            ""fontSize"": " + fontsize + @",
            ""opacity"": 1.0,
            ""paddingX"": 20,
            ""paddingY"": 5,
            ""borderRadius"": 0,
            ""bgColor"": ""rgba(" + (int)(color.r * 255) + "," + (int)(color.g * 255) + "," + (int)(color.b * 255) + "," + color.a + @")"",
            ""separator"": ""•""
        }";

        string json = GCStyleJSON;

        fileHandler.UpdateFile("GCStyle.json", json);
        fileHandler.UpdateFile("GCLote.html", HTML);
        
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