using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IPointerDownHandler
{
#if !UNITY_EDITOR
    [DllImport("user32.dll")]
    static extern bool ReleaseCapture();

    [DllImport("user32.dll")]
    static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    const int WM_NCLBUTTONDOWN = 0xA1;
    const int HTCAPTION = 0x2;
#endif

    public void OnPointerDown(PointerEventData eventData)
    {
#if !UNITY_EDITOR
        ReleaseCapture();
        SendMessage(GetActiveWindow(), WM_NCLBUTTONDOWN, HTCAPTION, 0);
#endif
    }
}