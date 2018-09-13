using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseDisplay : MonoBehaviour {

    public bool autoUpdate;

    public int width;
    public int height;
    [Range(0.001f, 1f)]
    public float scale;

    [Range(1, 8)]
    public int octaves;
    [Range(0, 1f)]
    public float persistance;
    [Range(1f, 10f)]
    public float lacunarity;

    [ContextMenu("Set Noise Map")]
    public void SetNoiseMap()
    {
        Renderer r = GetComponent<Renderer>();
        if (r == null) return;

        // get noise map
        float[,] noiseMap = Noise.GenerateNoiseMap(width, height, scale, octaves, persistance, lacunarity);

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

        r.sharedMaterial.mainTexture = noiseTexture;
    }
}
