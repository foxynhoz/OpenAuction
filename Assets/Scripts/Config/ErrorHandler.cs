using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ErrorHandler : MonoBehaviour
{
    /*
     * Esse script é responsável por exibir mensagens de erro na tela, utilizando uma animação para destacar a mensagem.
     */

    [SerializeField] Animator Animator;
    public GameObject ErrorTXT;

    private void Start()
    {
        Animator = GetComponentInChildren<Animator>();
    }

    public void showError(string errorCode)
    {
        StopAllCoroutines(); // Para evitar que múltiplas mensagens de erro se sobreponham
        Animator.Play("Error_Pop", -1, 0f); // Reinicia a animação
        StartCoroutine(Error(errorCode));
    }

    public IEnumerator Error(string message)
    {
        ErrorTXT.GetComponent<Text>().text = message;
        Animator.Play("Error_Pop");
        yield return null;
    }

    [ContextMenu("TestError")]
    public void TestError()
    {
        StartCoroutine(Error("This is a test error message."));
    }
}