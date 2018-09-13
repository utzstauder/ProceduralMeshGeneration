using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDisplay : MonoBehaviour {

    public int width;
    public int height;
    public float scale;

    [ContextMenu("Set Mesh")]
    public void SetMesh()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf == null) return;

        mf.mesh = MeshGenerator.GenerateMesh(width, height, scale);
    }
}
