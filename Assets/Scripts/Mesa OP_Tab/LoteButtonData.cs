using UnityEngine;

public class LoteButtonData : MonoBehaviour
{
    public LotesHandler lotesHandler;
    public string hashID;
    public string filePath;
    public string fileName;

    public int LoteID;
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

    LotesHandler listaHandler;

    public void SetActiveLote()
    {
        LotesHandler listaHandler = FindObjectOfType<LotesHandler>();
        listaHandler.setLoteManual(LoteID.ToString()); // Define o lote ativo usando o LoteID do botão
        Debug.Log("Botão clicado! Lote ID: " + LoteID);
    }

}
