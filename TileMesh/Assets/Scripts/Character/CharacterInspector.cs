using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Character))]
public class CharacterInspector : Editor {

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI ();

        DrawDefaultInspector ();

        //float sizeMultiplier=1f;
        //EditorGUILayout.FloatField("Increase scale by:", sizeMultiplier);

        //string text = "s";
        //GUILayout.TextField(text);


        /*
        if (GUILayout.Button ("Respawn"))
        {
            Character character = (Character)target;
            character.spawn();
        }
        */

    }

}
