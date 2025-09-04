using UnityEngine;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(SphereBehaviour)), CanEditMultipleObjects]
public class SphereBehaviourEditor : Editor
{
    private bool disableEnable = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //size the objects
        serializedObject.Update();
        var size = serializedObject.FindProperty("size");

        EditorGUILayout.PropertyField(size);
        serializedObject.ApplyModifiedProperties();

        // applies new size to all spheres
        foreach (var sphere in GameObject.FindObjectsOfType<SphereBehaviour>())
        {
            sphere.size = size.floatValue;
            sphere.transform.localScale = new Vector3(sphere.size, sphere.size, sphere.size);
        }

        //spheres can not be bigger than 2
        if (size.floatValue > 2f)
        {
            EditorGUILayout.HelpBox("The spheres' sizes cannot be bigger than 2!", MessageType.Warning);
        }

        EditorGUILayout.BeginHorizontal();

        //Creates button to select all the spheres
        if (GUILayout.Button("Select all spheres"))
        {
            var allSphereBehaviour = GameObject.FindObjectsOfType<SphereBehaviour>();
            var allSphereGameObjects = allSphereBehaviour.Select(sphere => sphere.gameObject).ToArray();
            Selection.objects = allSphereGameObjects;
        }
        //Creates button to clear the selection 
        if (GUILayout.Button("Clear selection"))
        {
            Selection.objects = new Object[] {(target as SphereBehaviour).gameObject};
        }
        
        EditorGUILayout.EndHorizontal();

        //set defualt color for button
        var cachedColor = GUI.backgroundColor;
        GUI.backgroundColor = disableEnable ? Color.red : Color.green;


        using (new EditorGUILayout.HorizontalScope())
        {
            //Creates button to disable/enable all the spheres
            if (GUILayout.Button("Disable/Enable all spheres", GUILayout.Height(40)))
            {
                disableEnable = !disableEnable;

                //Loop that controls the actual enabling/disabling of the game objects
                foreach (var sphere in GameObject.FindObjectsOfType<SphereBehaviour>(true))
                {
                    Undo.RecordObject(sphere.gameObject, "Disable/Enable sphere");
                    sphere.gameObject.SetActive(!sphere.gameObject.activeSelf);
                }
            }
        }
    }  
}