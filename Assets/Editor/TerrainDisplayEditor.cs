using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainDisplay))]
public class TerrainDisplayEditor : Editor
{

    public override void OnInspectorGUI()
    {
        TerrainDisplay script = (TerrainDisplay)target;

        if (DrawDefaultInspector() && script.autoUpdate)
        {
            script.SetTerrain();
        }

        if (GUILayout.Button("Generate Terrain"))
        {
            script.SetTerrain();
        }
    }

}
