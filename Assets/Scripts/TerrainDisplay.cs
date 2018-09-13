using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDisplay : MonoBehaviour {

    public bool autoUpdate;

    public int width;
    public int height;
    public int seed;
    public Vector2 offset;

    [Range(0.001f, 1f)]
    public float noiseScale;
    public float meshScale;
    public float verticalScale;
    public AnimationCurve heightCurve;

    [Range(1, 8)]
    public int octaves;
    [Range(0, 1f)]
    public float persistance;
    [Range(1f, 10f)]
    public float lacunarity;

    public FilterMode filterMode;

    public TerrainType[] terrain;

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }

    [ContextMenu("Set Terrain")]
    public void SetTerrain()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf == null) return;

        float[,] noiseMap = Noise.GenerateNoiseMap(width, height, noiseScale, octaves, persistance, lacunarity, seed, offset);

        mf.mesh = MeshGenerator.GenerateTerrainMesh(noiseMap, meshScale, verticalScale, heightCurve);


        Texture2D noiseTexture = new Texture2D(width, height);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);

                for (int i = 0; i < terrain.Length; i++)
                {
                    if (noiseMap[x, y] >= terrain[i].height)
                    {
                        colorMap[y * width + x] = terrain[i].color;
                    }
                }
            }
        }


        noiseTexture.filterMode = filterMode;
        noiseTexture.SetPixels(colorMap);
        noiseTexture.Apply();

        Renderer r = GetComponent<Renderer>();
        if (r == null) return;

        r.sharedMaterial.mainTexture = noiseTexture;

    }
}
