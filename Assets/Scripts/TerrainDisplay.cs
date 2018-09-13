using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDisplay : MonoBehaviour {

    public int width;
    public int height;
    public float noiseScale;
    public float meshScale;
    public float verticalScale;

    public int octaves;
    public float persistance;
    public float lacunarity;

    [ContextMenu("Set Terrain")]
    public void SetTerrain()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf == null) return;

        float[,] noiseMap = Noise.GenerateNoiseMap(width, height, noiseScale, octaves, persistance, lacunarity);

        mf.mesh = MeshGenerator.GenerateTerrainMesh(noiseMap, meshScale, verticalScale);


        Texture2D noiseTexture = new Texture2D(width, height);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        noiseTexture.SetPixels(colorMap);
        noiseTexture.Apply();

        Renderer r = GetComponent<Renderer>();
        if (r == null) return;

        r.sharedMaterial.mainTexture = noiseTexture;

    }
}
