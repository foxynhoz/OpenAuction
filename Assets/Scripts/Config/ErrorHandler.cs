using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ErrorHandler : MonoBehaviour
{
    [SerializeField] Animator Animator;
    public GameObject ErrorTXT;

    private void Start()
    {
        Animator = GetComponentInChildren<Animator>();
    }

    public void showError(string errorCode)
    {
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