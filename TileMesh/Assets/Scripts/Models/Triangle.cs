using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class Triangle : MonoBehaviour {

    public float triangle_height;
    public float triangle_width;
    public float triangle_depth;

    // Use this for initialization
    void Start () {
        buildMesh();

    }

    // Update is called once per frame
    void Update () {

    }


    public void buildMesh()
    {
        int vertices_size = 6;

        Vector3[] vertices = build_vertices ();
        int[] triangles = build_triangles();
        Vector3[] normals = new Vector3[vertices_size];
        Vector2[] uv = new Vector2[vertices_size];

        for (int i = 0; i < vertices_size; ++i)
            uv[i] = new Vector2(0, 0);
        
        for (int i = 0; i < vertices_size; ++i)
        {
            normals [i] = Vector3.up;
        }

        Mesh mesh = new Mesh ();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        MeshCollider mesh_collider = GetComponent<MeshCollider> ();
        MeshRenderer mesh_renderer = GetComponent<MeshRenderer> ();
        MeshFilter mesh_filter = GetComponent<MeshFilter> ();

        mesh_filter.mesh = mesh;
        mesh_collider.sharedMesh = mesh;
    }


    Vector3[] build_vertices()
    {
        float x_center = triangle_width / 2f;

        Vector3[] vertices = new Vector3[6];

        int index = 0;
        vertices[index++] = new Vector3(x_center - triangle_width /2f, 0, 0);
        vertices[index++] = new Vector3(x_center + triangle_width /2f, 0, 0);
        vertices[index++] = new Vector3(x_center, 0, triangle_height);

        vertices[index++] = new Vector3(x_center - triangle_width /2f, triangle_depth, 0);
        vertices[index++] = new Vector3(x_center + triangle_width /2f, triangle_depth, 0);
        vertices[index++] = new Vector3(x_center, triangle_depth, triangle_height);

        return vertices;
    }

    int[] build_triangles()
    {        
        int[] triangles = new int[8 * 3];
        /*
        *     5
        * 3       4
        */
        /*
         *     2
         * 0       1
         */

        int index = 0;
        triangles[index++] = 0;
        triangles[index++] = 1;
        triangles[index++] = 2;

        triangles[index++] = 5;
        triangles[index++] = 4;
        triangles[index++] = 3;

        triangles[index++] = 4;
        triangles[index++] = 0;
        triangles[index++] = 3;

        triangles[index++] = 4;
        triangles[index++] = 1;
        triangles[index++] = 0;

        triangles[index++] = 4;
        triangles[index++] = 5;
        triangles[index++] = 2;

        triangles[index++] = 4;
        triangles[index++] = 2;
        triangles[index++] = 1;

        triangles[index++] = 5;
        triangles[index++] = 3;
        triangles[index++] = 0;

        triangles[index++] = 5;
        triangles[index++] = 0;
        triangles[index++] = 2;

        return triangles;
    }
}
