using JetBrains.Annotations;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ListaHandler : MonoBehaviour
{
    FileHandler fileHandler = new FileHandler();

    public List<Animal> lotes = new List<Animal>();

    [SerializeField] Text leilACTIVE_TXT;
    public string leilaoAtivo = ""; // Nome do leilão ativo, usado para nomear o arquivo JSON

    [Header("Input Fields")]
    [SerializeField] InputField fileNameInput;
    [SerializeField] InputField searchInput;

    [SerializeField] InputField loteID_Field;
    [SerializeField] InputField loteName_Field;
    [SerializeField] InputField loteBrinco_Field;
    [SerializeField] InputField loteSangue_Field;
    [SerializeField] InputField loteNascimento_Field;
    [SerializeField] InputField loteSexo_Field;
    [SerializeField] InputField loteUltimoParto_Field;
    [SerializeField] InputField lotePrevParto_Field;
    [SerializeField] InputField loteProducao_Field;
    [SerializeField] InputField lotePeso_Field;
    [SerializeField] InputField lotePai_Field;
    [SerializeField] InputField loteMae_Field;
    [SerializeField] InputField loteInfoExtras_Field;

    private void Start()
    {
        loteID_Field.text = "";
        loteName_Field.text = "";
        loteBrinco_Field.text = "";
        loteSangue_Field.text = "";
        loteNascimento_Field.text = "";
        loteSexo_Field.text = "";
        loteUltimoParto_Field.text = "";
        lotePrevParto_Field.text = "";
        loteProducao_Field.text = "";
        lotePeso_Field.text = "";
        lotePai_Field.text = "";
        loteMae_Field.text = "";
        loteInfoExtras_Field.text = "";
    }

    private void Update()
    {
        leilACTIVE_TXT.text = "LEILÃO ATIVO: " + leilaoAtivo;
    }


    [ContextMenu("Adicionar Animal")]
    public void AddLote()
    {
        if (leilaoAtivo == "")
        {
            Debug.LogError("Nenhum leilão ativo. Defina o nome do leilão antes de adicionar lotes.");
            return;
        }
        else
        {
            lotes.Add(new Animal
            {
                loteID = int.TryParse(loteID_Field.text, out int loteID) ? loteID : 0,
                brinco = int.TryParse(loteBrinco_Field.text, out int brinco) ? brinco : 0,
                sangue = loteSangue_Field.text,
                nome = loteName_Field.text,
                sexo = loteSexo_Field.text,
                nascimento = loteNascimento_Field.text,
                peso = lotePeso_Field.text,
                ultimoParto = loteUltimoParto_Field.text,
                prevParto = lotePrevParto_Field.text,
                producao = loteProducao_Field.text,
                pai = lotePai_Field.text,
                mae = loteMae_Field.text,
                infoExtras = loteInfoExtras_Field.text,
            });
            SalvarLista();
        }
    }

    public void setLoteManual() //Atualiza no OBS o lote atual com os dados do lote encontrado
    {
        int SearchedloteID = int.Parse(searchInput.text);
        Animal foundLote = lotes.Find(l => l.loteID == SearchedloteID);

        if (foundLote != null)
        {
            Debug.Log("Animal encontrado: " + foundLote.nome);
            SalvarLoteEncontrado(foundLote);
        }
        else
        {
            Debug.Log("Animal não encontrado.");
        }
    }

    public void SalvarLoteEncontrado(Animal foundLote)
    {
        if (foundLote == null)
        {
            Debug.Log("Nada para salvar");
            return;
        }

        StringBuilder sb = new StringBuilder();

        sb.AppendLine(foundLote.loteID.ToString());
        sb.AppendLine(foundLote.brinco.ToString());
        sb.AppendLine(foundLote.nome ?? "");
        sb.AppendLine(foundLote.infoExtras ?? "");
        sb.AppendLine(foundLote.sexo ?? "");
        sb.AppendLine(foundLote.sangue ?? "");
        sb.AppendLine(foundLote.nascimento ?? "");
        sb.AppendLine(foundLote.ultimoParto ?? "");
        sb.AppendLine("Prev. Parto: " + (foundLote.prevParto ?? ""));
        sb.AppendLine(foundLote.producao ?? "");
        sb.AppendLine(foundLote.peso ?? "");
        sb.AppendLine(foundLote.pai ?? "");
        sb.AppendLine(foundLote.mae ?? "");

        fileHandler.UpdateFile("LoteAtual.txt", sb.ToString());

        Debug.Log("Lote salvo");
    }


    public void novoLeilao()
    {
        if (string.IsNullOrEmpty(fileNameInput.text))
        {
            Debug.LogError("Nome do leilão não pode ser vazio.");
            return;
        }
        leilaoAtivo = fileNameInput.text;
        lotes.Clear();
        SalvarLista();
    }

    public void DeleteLote() // Exclui um lote específico da lista e atualiza o arquivo JSON
    {
        int SearchedloteID = int.Parse(searchInput.text);
        Animal foundLote = lotes.Find(l => l.loteID == SearchedloteID);
        if (foundLote != null)
        {
            lotes.Remove(foundLote);
            SalvarLista();
            Debug.Log("Animal excluído: " + foundLote.nome);
        }
        else
        {
            Debug.Log("Animal não encontrado para exclusão.");
        }
    }

    public void ClearLotes() // Limpa a lista de lotes e o arquivo JSON correspondente
    {
        lotes.Clear();
        if (leilaoAtivo != null)
        {
            fileHandler.UpdateFile("Leiloes/" + leilaoAtivo + ".json", JsonUtility.ToJson(new AnimalList { animais = lotes }, true));
            Debug.Log("Lista de lotes limpa");
        }
        else
        {
            Debug.LogError("Nenhum leilão ativo. Defina o nome do leilão antes de limpar os lotes.");
        }
    }


    [ContextMenu("Salvar Lista")]
    public void SalvarLista() // Salva a lista completa de lotes em um arquivo JSON
    {
        if (leilaoAtivo == null)
        {
            Debug.LogError("Nenhum leilão ativo. Defina o nome do leilão antes de salvar a lista.");
            return;
        }

        AnimalList wrapper = new AnimalList();
        wrapper.animais = lotes; // sua lista

        string json = JsonUtility.ToJson(wrapper, true);

        fileHandler.UpdateFile("Leiloes/" + leilaoAtivo + ".json", json);

        Debug.Log("Lista salva");
    }

    [ContextMenu("Carregar Lista")]
    public void CarregarLista() // Carrega a lista completa de lotes de um arquivo JSON
    {
        string path = Application.dataPath + "/LeilaoData/Leiloes/" + fileNameInput.text + ".json";
        

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            leilaoAtivo = fileNameInput.text;

            AnimalList wrapper = JsonUtility.FromJson<AnimalList>(json);

            lotes = wrapper.animais;
            leilaoAtivo = fileNameInput.text;

            Debug.Log("Lista carregada: " + lotes.Count);

        }
        else
        {
            Debug.Log("Arquivo não encontrado");
        }
    }






}

[System.Serializable]
public class AnimalList
{
    public List<Animal> animais;
}

[System.Serializable]
public class Animal
{
    public int loteID;
    public int brinco;
    public string nome;
    public string infoExtras;
    public string sexo;
    public string sangue;
    public string nascimento;
    public string ultimoParto;
    public string prevParto;
    public string producao;
    public string peso;
    public string pai;
    public string mae;
}
