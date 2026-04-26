using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListaHandler : MonoBehaviour
{
    public List<Lote> lotes = new List<Lote>();
    [SerializeField] InputField searchInput;

    void Start()
    {
        
    }

    [ContextMenu("Adicionar Lote")]
    void AddLote()
    {
        lotes.Add(new Lote
        {
            loteID = 1,
            brinco = 123,
            nome = "Lote 1",
            infoExtras = "Informações extras do lote 1",
            sexo = "Macho",
            sangue = "A+",
            nascimento = "01/01/2020",
            ultimoParto = "01/01/2022",
            producao = "100 litros",
            pai = "Pai do lote 1",
            mae = "Mãe do lote 1"
        }
        );
    }

    public void listSearch()
    {
        int SearchedloteID = int.Parse(searchInput.text);
        Lote foundLote = lotes.Find(l => l.loteID == SearchedloteID);

        if (foundLote != null)
        {
            Debug.Log("Lote encontrado: " + foundLote.nome);
        }
        else
        {
            Debug.Log("Lote não encontrado.");
        }
    }
}

[System.Serializable]
public class Lote
{
    public int loteID;
    public int brinco;
    public string nome;
    public string infoExtras;
    public string sexo;
    public string sangue;
    public string nascimento;
    public string ultimoParto;
    public string producao;
    public string pai;
    public string mae;
}
