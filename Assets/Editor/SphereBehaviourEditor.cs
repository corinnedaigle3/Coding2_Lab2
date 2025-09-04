using UnityEngine;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(SphereBehaviour)), CanEditMultipleObjects]
public class SphereBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

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

        var cachedColor = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;

        using (new EditorGUILayout.HorizontalScope())
        {
            //Creates button to disable/enable all the spheres
            if (GUILayout.Button("Disable/Enable all spheres", GUILayout.Height(40)))
            {
                //Loop that controls the actual enabling/disabling of the game objects
                foreach (var sphere in GameObject.FindObjectsOfType<SphereBehaviour>(true))
                {
                    Undo.RecordObject(sphere.gameObject, "Disable/Enable cube");
                    sphere.gameObject.SetActive(!sphere.gameObject.activeSelf);
                }
            }
        }
    }  
}
