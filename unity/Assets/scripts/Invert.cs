using UnityEngine;
using System.Collections;

public class Invert : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Mesh sphereMesh = new Mesh();
        sphereMesh = GetComponent<MeshFilter>().mesh;
        
        for (int i = 0; i < sphereMesh.normals.Length; i++)
        {
            sphereMesh.normals[i] = sphereMesh.normals[i] * -1.0f;
        }
        int[] triangles = sphereMesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int t = triangles[i];
            triangles[i] = triangles[i + 2];
            triangles[i + 2] = t;
        }
        sphereMesh.triangles = triangles;
        sphereMesh.RecalculateNormals();
        sphereMesh.RecalculateBounds();
        GetComponent<MeshFilter>().mesh = sphereMesh;
        
        
	}

    // Update is called once per frame
    void Update()
    {
	    
	}
}
