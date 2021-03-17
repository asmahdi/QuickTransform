using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ScatterTool))]
public class ScatterToolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ScatterTool st = (ScatterTool) target; 
        if (GUILayout.Button("Generate"))
        {
            st.GenerateObjects();
        }
        if (GUILayout.Button("Bake"))
        {
            st.BakeObjects();
        }

        if (GUILayout.Button("Clear"))
        {
            st.ClearObjects();
        }
        
    }
}
