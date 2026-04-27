using UnityEngine;
using System.IO;

public class DeleteEvent : MonoBehaviour
{
    public void Delete()
    {
        EventButtonData data = GetComponent<EventButtonData>();
        if (data == null)
        {
            Debug.LogError("EventButtonData component not found on the GameObject.");
            return;
        }

        string filePath = data.filePath;

        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
                Debug.Log($"File deleted successfully: {filePath}");
                try
                {
                    File.Delete(filePath + ".meta");
                }
                catch (IOException ex)
                {
                    Debug.LogError($"There is no Meta File to be deleted:");
                }
            }
            catch (IOException ex)
            {
                Debug.LogError($"Error deleting file: {ex.Message}");
            }
        }
        else
        {
            Debug.LogWarning($"File not found: {filePath}");
        }

        Destroy(data.gameObject);
    }
}
