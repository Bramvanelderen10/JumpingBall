using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LandscapeGenerator : MonoBehaviour {

    public GameObject _landscapeMesh;

	// Use this for initialization
	void Start () {
        CreateMesh();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreateMesh()
    {
        GameObject landscapeObject = Instantiate(_landscapeMesh);
        MeshFilter mf = landscapeObject.GetComponent<MeshFilter>();

        Mesh mesh = new Mesh();

        int length = 100;
        int width = 10;
        int totalVertices = (length + 1) * (width + 1);

        int vertexNumber = 0;

        Vector3[] vertices = new Vector3[totalVertices]; //MAYBE OUT OF BOUNDS
        
        /*
        Loop through the width of the plane then in each loop generate a full row in length of vertices
        */
        for (int w = 0; w < width + 1; w++)
        {
            for (int l = 0; l < length + 1; l++)
            {
                //float x = w; //change to sin(l) + w for curve
                float x = w + (Mathf.Sin((float)l / (float)6) * 5);
                float y = 0; 
                float z = l;

                vertices[vertexNumber] = new Vector3(x, y, z);
                vertexNumber++;
            }
        }

        /*
        Find edge numbers
        */
        List<int> vertexNumbersLeft = new List<int>();
        List<int> vertexNumbersRight = new List<int>();

        for (int i = 0; i < length + 1; i++)
        {
            vertexNumbersLeft.Add(i);
            vertexNumbersRight.Add(i + (width * (length + 1)));
        }

        List<int> vertexNumbersTop = new List<int>();
        List<int> vertexNumbersBot = new List<int>();

        for (int i = 0; i < width + 1; i++)
        {
            vertexNumbersBot.Add(i * (length + 1));
            vertexNumbersTop.Add((i * (length + 1)) + length);
        }


        int[] triangles = new int[((width * length) * 2) * 3]; //MAYBE OUT OF BOUNDS
        int triangleNumber = 0;
        for (int i = 0; i < totalVertices; i++)
        {
            if (vertexNumbersTop.Contains(i))
                continue;

            if (!vertexNumbersLeft.Contains(i))
            {
                triangles[triangleNumber] = i;
                triangleNumber++;
                triangles[triangleNumber] = i - (length + 1);
                triangleNumber++;
                triangles[triangleNumber] = i + 1;
                triangleNumber++;
            }

            if (!vertexNumbersRight.Contains(i))
            {
                triangles[triangleNumber] = i;
                triangleNumber++;
                triangles[triangleNumber] = i + 1;
                triangleNumber++;
                triangles[triangleNumber] = i + (length + 1) + 1;
                triangleNumber++;                
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.Optimize();

        mf.mesh = mesh;
    }
}
