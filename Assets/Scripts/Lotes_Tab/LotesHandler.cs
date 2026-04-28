using JetBrains.Annotations;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LotesHandler : MonoBehaviour
{
    FileHandler fileHandler = new FileHandler();

    public List<LoteData> lotes = new List<LoteData>();

    [SerializeField] Text leilACTIVE_TXT;
    public string leilaoAtivo = ""; // Nome do leilão ativo, usado para nomear o arquivo JSON

    [Header("Input Fields")]
    //[SerializeField] InputField fileNameInput;
    //[SerializeField] InputField searchInput; Averiguar depois se é necessário ou se pode ser removido
    
    [SerializeField] InputField loteID_Field;
    [SerializeField] InputField loteName_Field;
    [SerializeField] InputField loteBrinco_Field;
    [SerializeField] InputField loteSangue_Field;
    [SerializeField] InputField loteNascimento_Field;
    [SerializeField] InputField loteIdade_Field;
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
        loteIdade_Field.text = "";
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


    [ContextMenu("Adicionar LoteData")]
    public void AddLote()
    {
        if (leilaoAtivo == "")
        {
            Debug.LogError("Nenhum leilão ativo. Defina o nome do leilão antes de adicionar lotes.");
            return;
        }
        else
        {
            lotes.Add(new LoteData
            {
                loteID = int.TryParse(loteID_Field.text, out int loteID) ? loteID : 0,
                brinco = int.TryParse(loteBrinco_Field.text, out int brinco) ? brinco : 0,
                sangue = loteSangue_Field.text,
                nome = loteName_Field.text,
                sexo = loteSexo_Field.text,
                nascimento = loteNascimento_Field.text,
                idade = loteIdade_Field.text,
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

    public void setLoteManual(string loteID) //Atualiza no OBS o lote atual com os dados do lote encontrado
    {
        int SearchedloteID = int.Parse(loteID);
        LoteData foundLote = lotes.Find(l => l.loteID == SearchedloteID);

        if (foundLote != null)
        {
            Debug.Log("LoteData encontrado: " + foundLote.nome);
            SalvarLoteEncontrado(foundLote);
        }
        else
        {
            Debug.Log("LoteData não encontrado.");
        }
    }

    public void SalvarLoteEncontrado(LoteData foundLote)
    {
        if (foundLote == null)
        {
            Debug.Log("Nada para salvar");
            return;
        }

        SaveIndependentTXTS(foundLote);
        SaveSingleTXT(foundLote);
        SaveToGCLoteJSON(foundLote);

    }

    /*
   public void DeleteLote() // Exclui um lote específico da lista e atualiza o arquivo JSON
   {
       int SearchedloteID = int.Parse(searchInput.text);
       LoteData foundLote = lotes.Find(l => l.loteID == SearchedloteID);
       if (foundLote != null)
       {
           lotes.Remove(foundLote);
           SalvarLista();
           Debug.Log("LoteData excluído: " + foundLote.nome);
       }
       else
       {
           Debug.Log("LoteData não encontrado para exclusão.");
       }
   }
   */

    void SaveIndependentTXTS(LoteData foundLote)
    {
        fileHandler.UpdateFile("/OBS_Stuff/LoteID.txt", foundLote.loteID.ToString());
        fileHandler.UpdateFile("/OBS_Stuff/brinco.txt", foundLote.brinco.ToString());
        fileHandler.UpdateFile("/OBS_Stuff/nome.txt", foundLote.nome ?? "");
        fileHandler.UpdateFile("/OBS_Stuff/infoExtras.txt", foundLote.infoExtras ?? "");
        fileHandler.UpdateFile("/OBS_Stuff/sexo.txt", foundLote.sexo ?? "");
        fileHandler.UpdateFile("/OBS_Stuff/sangue.txt", foundLote.sangue ?? "");

        fileHandler.UpdateFile("/OBS_Stuff/idade.txt",
            string.IsNullOrWhiteSpace(foundLote.idade) ? "" : $"Idade: {foundLote.idade}");

        fileHandler.UpdateFile("/OBS_Stuff/nascimento.txt",
            string.IsNullOrWhiteSpace(foundLote.nascimento) ? "" : $"Nasc: {foundLote.nascimento}");

        fileHandler.UpdateFile("/OBS_Stuff/ultimoParto.txt",
            string.IsNullOrWhiteSpace(foundLote.ultimoParto) ? "" : $"Últm Parto: {foundLote.ultimoParto}");

        fileHandler.UpdateFile("/OBS_Stuff/prevParto.txt",
            string.IsNullOrWhiteSpace(foundLote.prevParto) ? "" : $"Prev. Parto: {foundLote.prevParto}");

        fileHandler.UpdateFile("/OBS_Stuff/producao.txt",
            string.IsNullOrWhiteSpace(foundLote.producao) ? "" : $"Produção: {foundLote.producao}");

        fileHandler.UpdateFile("/OBS_Stuff/peso.txt",
            string.IsNullOrWhiteSpace(foundLote.peso) ? "" : $"Peso: {foundLote.peso}");

        fileHandler.UpdateFile("/OBS_Stuff/pai.txt",
            string.IsNullOrWhiteSpace(foundLote.pai) ? "" : $"Pai: {foundLote.pai}");

        fileHandler.UpdateFile("/OBS_Stuff/mae.txt",
            string.IsNullOrWhiteSpace(foundLote.mae) ? "" : $"Mãe: {foundLote.mae}");

        Debug.Log("Lote salvo em arquivos individuais para OBS");
    }

    void SaveSingleTXT(LoteData foundLote)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(foundLote.loteID.ToString());
        sb.AppendLine(foundLote.brinco.ToString());
        sb.AppendLine(foundLote.nome ?? "");
        sb.AppendLine(foundLote.infoExtras ?? "");
        sb.AppendLine(foundLote.sexo ?? "");
        sb.AppendLine(foundLote.sangue ?? "");
        sb.AppendLine(string.IsNullOrWhiteSpace(foundLote.idade) ? "" : $"Idade: {foundLote.idade}");
        sb.AppendLine(string.IsNullOrWhiteSpace(foundLote.nascimento) ? "" : $"Nasc: {foundLote.nascimento}");
        sb.AppendLine(string.IsNullOrWhiteSpace(foundLote.ultimoParto) ? "" : $"Últm Parto: {foundLote.ultimoParto}");
        sb.AppendLine(string.IsNullOrWhiteSpace(foundLote.prevParto) ? "" : $"Prev. Parto: {foundLote.prevParto}");
        sb.AppendLine(string.IsNullOrWhiteSpace(foundLote.producao) ? "" : $"Produção: {foundLote.producao}");
        sb.AppendLine(string.IsNullOrWhiteSpace(foundLote.peso) ? "" : $"Peso: {foundLote.peso}");
        sb.AppendLine(string.IsNullOrWhiteSpace(foundLote.pai) ? "" : $"Pai: {foundLote.pai}");
        sb.AppendLine(string.IsNullOrWhiteSpace(foundLote.mae) ? "" : $"Mãe: {foundLote.mae}");

        fileHandler.UpdateFile("LoteAtual.txt", sb.ToString());

        Debug.Log("Lote salvo em LoteAtual.txt");
    }

    void SaveToGCLoteJSON(LoteData foundLote)
    {
        string json = JsonUtility.ToJson(foundLote, true);
        fileHandler.UpdateFile("GCLote.json", json);
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

        fileHandler.UpdateFile("Leiloes/" + leilaoAtivo.ToLower() + ".json", json);

        Debug.Log("Lista salva");
    }

    [ContextMenu("Carregar Lista")]
    public void CarregarLista(string filename, string path) // Carrega a lista completa de lotes de um arquivo JSON
    {
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            leilaoAtivo = filename.ToUpper();

            AnimalList wrapper = JsonUtility.FromJson<AnimalList>(json);
            lotes = wrapper.animais;

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
    public List<LoteData> animais;
}

[System.Serializable]
public class LoteData
{
    public int loteID;
    public int brinco;
    public string nome;
    public string infoExtras;
    public string sexo;
    public string idade;
    public string sangue;
    public string nascimento;
    public string ultimoParto;
    public string prevParto;
    public string producao;
    public string peso;
    public string pai;
    public string mae;
}

public class LeilaoInfos
{
    public string nomeLeilao;
    public string dataLeilao;
    public string horarioLeilao;
    public string localLeilao;
    public string organizadorLeilao;
}
