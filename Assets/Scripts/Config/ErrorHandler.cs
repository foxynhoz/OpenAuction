using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ErrorHandler : MonoBehaviour
{
    public GameObject ErrorTXT;

    public void showError(string errorCode)
    {
        StartCoroutine(Error(errorCode));
    }

    public IEnumerator Error(string message)
    {
        ErrorTXT.GetComponent<Text>().text = "⚠" + message;
        ErrorTXT.SetActive(true);
        yield return new WaitForSeconds(4f);
        ErrorTXT.SetActive(false);
    }
}