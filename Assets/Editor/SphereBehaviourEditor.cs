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
        if (GUILayout.Button("Select all spheres"))
        {
            var allSphereBehaviour = GameObject.FindObjectsOfType<SphereBehaviour>();
            var allSphereGameObjects = allSphereBehaviour.Select(sphere => sphere.gameObject).ToArray();
            Selection.objects = allSphereGameObjects;
        }
        if (GUILayout.Button("Clear selection"))
        {
            Selection.objects = new Object[] {(target as SphereBehaviour).gameObject};
        }
        EditorGUILayout.EndHorizontal();

        var cachedColor = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Disable/Enable all spheres", GUILayout.Height(40)))
            {
                foreach (var sphere in GameObject.FindObjectsOfType<SphereBehaviour>(true))
                {
                    Undo.RecordObject(sphere.gameObject, "Disable/Enable cube");
                    sphere.gameObject.SetActive(!sphere.gameObject.activeSelf);
                }
            }
        }
    }  
}
