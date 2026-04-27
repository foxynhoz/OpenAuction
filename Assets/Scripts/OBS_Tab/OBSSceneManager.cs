using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections.Generic;

public class OBSSceneManager : MonoBehaviour
{
    [Header("UI")]
    public Transform containerBotoes;  // O pai onde os botões serão instanciados (ex: um VerticalLayoutGroup)
    public GameObject prefabBotao;     // Seu prefab de botão

    // Chame esse método passando o JSON recebido do WebSocket
    public void OnGetSceneListResponse(string json)
    {
        // Limpa botões anteriores (se houver)
        foreach (Transform filho in containerBotoes)
        {
            Destroy(filho.gameObject);
        }

        // Parseia o JSON
        Root root = JsonConvert.DeserializeObject<Root>(json);
        List<Scene> scenes = root.d.responseData.scenes;

        // Instancia um botão por cena
        foreach (Scene scene in scenes)
        {
            GameObject novoBotao = Instantiate(prefabBotao, containerBotoes);

            // Pega o componente Text ou TMP_Text do botão
            // Se usar TextMeshPro:
            TMPro.TMP_Text textoBotao = novoBotao.GetComponentInChildren<TMPro.TMP_Text>();
            // Se usar UI Text legado:
            // Text textoBotao = novoBotao.GetComponentInChildren<Text>();

            textoBotao.text = scene.sceneName;

            // Adiciona o listener de clique passando o nome da cena
            string nomeCapturado = scene.sceneName; // Captura pro closure
            novoBotao.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnClicarCena(nomeCapturado);
            });
        }
    }

    private void OnClicarCena(string nomeDaCena)
    {
        Debug.Log($"Cena clicada: {nomeDaCena}");
        // Aqui você chama seu método do WebSocket pra trocar de cena no OBS
        // ex: obsWebSocket.SetCurrentScene(nomeDaCena);
    }
}