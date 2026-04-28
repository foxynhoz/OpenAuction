using UnityEngine;

public class LoteButtonData : MonoBehaviour
{
    public int LoteID;
    ListaHandler listaHandler;

    public void SetActiveLote()
    {
        ListaHandler listaHandler = FindObjectOfType<ListaHandler>();
        listaHandler.setLoteManual(LoteID.ToString()); // Define o lote ativo usando o LoteID do botão
        Debug.Log("Botão clicado! Lote ID: " + LoteID);
    }

}
