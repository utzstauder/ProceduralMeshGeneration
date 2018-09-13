using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NoiseDisplay))]
public class NoiseDisplayEditor : Editor {

    public override void OnInspectorGUI()
    {
        NoiseDisplay script = (NoiseDisplay)target;

        if (DrawDefaultInspector() && script.autoUpdate)
        {
            script.SetNoiseMap();
        }

        if (GUILayout.Button("Generate Noisemap"))
        {
            script.SetNoiseMap();
        }
    }

}
