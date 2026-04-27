using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class WindowControls : MonoBehaviour
{
#if !UNITY_EDITOR
    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    const int SW_MINIMIZE = 6;
#endif

    public void Minimize()
    {
#if !UNITY_EDITOR
        IntPtr hwnd = GetActiveWindow();
        ShowWindow(hwnd, SW_MINIMIZE);
#endif
    }
}
