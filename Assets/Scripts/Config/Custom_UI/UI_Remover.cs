using System.Collections;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class BorderlessWindow : MonoBehaviour
{
#if !UNITY_EDITOR
    [DllImport("user32.dll")] static extern IntPtr GetActiveWindow();
    [DllImport("user32.dll")] static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll")] static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    [DllImport("user32.dll")] static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy, uint uFlags);

    const int GWL_STYLE = -16;
    const int WS_CAPTION = 0x00C00000;
    const int WS_THICKFRAME = 0x00040000;

    const uint SWP_NOMOVE = 0x0002;
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOZORDER = 0x0004;
    const uint SWP_FRAMECHANGED = 0x0020;
#endif

    IEnumerator Start()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        Screen.SetResolution(1300, 880, false);

        yield return new WaitForSeconds(0.1f);

#if !UNITY_EDITOR
        IntPtr hwnd = GetActiveWindow();

        int style = GetWindowLong(hwnd, GWL_STYLE);

        style &= ~WS_CAPTION;
        style &= ~WS_THICKFRAME;

        SetWindowLong(hwnd, GWL_STYLE, style);

        SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0,
            SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
#endif
    }
}