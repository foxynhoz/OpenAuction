using UnityEngine;

public class LoteButtonData : MonoBehaviour
{
    public int LoteID;
    LotesHandler listaHandler;

    public void SetActiveLote()
    {
        LotesHandler listaHandler = FindObjectOfType<LotesHandler>();
        listaHandler.setLoteManual(LoteID.ToString()); // Define o lote ativo usando o LoteID do botão
        Debug.Log("Botão clicado! Lote ID: " + LoteID);
    }

}
