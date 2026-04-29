using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tab_Manager))]
public class TesteEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Eventos Tab"))
        {
            Tab_Manager.FindAnyObjectByType<Tab_Manager>().activatePanel(0);
        }
        if (GUILayout.Button("Lotes Tab"))
        {
            Tab_Manager.FindAnyObjectByType<Tab_Manager>().activatePanel(1);
        }
        if (GUILayout.Button("Mesa OP Tab"))
        {
            Tab_Manager.FindAnyObjectByType<Tab_Manager>().activatePanel(2);
        }
        if (GUILayout.Button("OBS Config Tab"))
        {
            Tab_Manager.FindAnyObjectByType<Tab_Manager>().activatePanel(3);
        }
        if (GUILayout.Button("Overlay Tab"))
        {
            Tab_Manager.FindAnyObjectByType<Tab_Manager>().activatePanel(5);
        }
        if (GUILayout.Button("Info Tab"))
        {
            Tab_Manager.FindAnyObjectByType<Tab_Manager>().activatePanel(4);
        }
        
    }
}