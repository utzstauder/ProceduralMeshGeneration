 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator {

    public static Mesh GenerateTerrainMesh(float[,] heightMap, float scale, float verticalScale, AnimationCurve heightCurve)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        Mesh mesh = GenerateMesh(width, height, scale);

        Vector3[] vertices = mesh.vertices;
        // set height
        int i = 0;
        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                if (x > 0 && y > 0 && y < height &&  x < width)
                {
                    vertices[i].y = heightCurve.Evaluate(heightMap[x, y]) * verticalScale;
                } 
                i++;
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        return mesh;
    }

    public static Mesh GenerateMesh(int width, int height, float scale)
    {
        Mesh mesh = new Mesh();

        // data
        Vector3[] vertices = new Vector3[(width + 1) * (height + 1)];
        int[] triangles = new int[width * height * 2 * 3];
        Vector2[] uvs = new Vector2[(width + 1) * (height + 1)];

        // set vertex positions
        int i = 0;
        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                vertices[i] = new Vector3(
                    x * scale - width/2f,
                    0,
                    y * scale - height/2f);

                uvs[i] = new Vector2(
                        (float)x / width,
                        (float)y / height
                    );

                i++;
            }
        }

        // set triangles
        i = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // top triangle
                triangles[i + 0] = y * (width + 1) + x;         // +
                triangles[i + 1] = (y + 1) * (width + 1) + x;   // +
                triangles[i + 2] = triangles[i + 0] + 1;        // +

                // bottom triangle
                triangles[i + 3] = triangles[i + 0] + 1;            // +
                triangles[i + 4] = (y + 1) * (width + 1) + x;       // +
                triangles[i + 5] = (y + 1) * (width + 1) + (x + 1); // +

                i += 6;
            }
        }

        // apply data
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        return mesh;
    }

}
