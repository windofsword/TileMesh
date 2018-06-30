using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Triangle))]
public class TriangleInspector : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector ();

        if (GUILayout.Button ("Regenerate"))
        {
            Triangle triangle = (Triangle)target;
            triangle.buildMesh();
        }
    }

}
