using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestMeshGenerator : MonoBehaviour {

    public GameObject _prefab;

    public int _length, _width;
    public float _curveDepth = 0f, _curveLength = 0f, _steepness = 1f, _downCurveStrength = 0f;
    private GameObject landscapeObject;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CreateMesh(_length, _width, _curveDepth, _curveLength, _steepness, _downCurveStrength);
    }

    void CreateMesh(int length, int width, float curveDepth = 0f, float curveLength = 0f, float steepness = 1f, float downCurveStrength = 0f)
    {
        if (!landscapeObject)
            landscapeObject = Instantiate(_prefab);


        landscapeObject.SetActive(true);
        landscapeObject.tag = "env";
        MeshFilter mf = landscapeObject.GetComponent<MeshFilter>();

        Mesh mesh = new Mesh();

        int totalVertices = (length + 1) * (width + 1);

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

        int vertexNumber = 0;
        Vector3[] vertices = new Vector3[totalVertices];
        /*
        Loop through the width of the plane then in each loop generate a full row in length of vertices
        */
        for (int w = 0; w < width + 1; w++)
        {
            for (int l = 0; l < length + 1; l++)
            {
                //float x = w; //change to sin(l) + w for curve
                float x = w + (Mathf.Sin((float)l / (float)curveLength) * curveDepth);
                float y = -((float)l * steepness) / 4f;
                float z = l;

                vertices[vertexNumber] = new Vector3(x, y, z);

                /*
                Save last point to place the next generated mesh on the correct position
                */
                vertexNumber++;
            }
        }

        int[] triangles = new int[((width * length) * 2) * 3];
        int triangleNumber = 0;
        for (int i = 0; i < totalVertices; i++)
        {

            /*
            If no vertices above the current vertex so no more triangles
            */
            if (vertexNumbersTop.Contains(i))
                continue;

            /*
            Create triangle to the left of the current vertex
            */
            if (!vertexNumbersLeft.Contains(i))
            {
                triangles[triangleNumber] = i;
                triangleNumber++;
                triangles[triangleNumber] = i - (length + 1);
                triangleNumber++;
                triangles[triangleNumber] = i + 1;
                triangleNumber++;
            }

            /*
            Create triangle to the right of the current vertex
            */
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
        landscapeObject.GetComponent<MeshCollider>().sharedMesh = mf.mesh;
    }
}
