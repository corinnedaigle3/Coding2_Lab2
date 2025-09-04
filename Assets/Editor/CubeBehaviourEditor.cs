using UnityEngine;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(CubeBehaviour)), CanEditMultipleObjects]
public class CubeBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Select all cubes"))
        {
            var allCubeBehaviour = GameObject.FindObjectsOfType<CubeBehaviour>();
            var allCubeGameObjects = allCubeBehaviour.Select(cube => cube.gameObject).ToArray();
            Selection.objects = allCubeGameObjects;
        }
        if (GUILayout.Button("Clear selection"))
        {
            Selection.objects = new Object[] {(target as CubeBehaviour).gameObject};
        }
        EditorGUILayout.EndHorizontal();

        var cachedColor = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Disable/Enable all cubes", GUILayout.Height(40)))
            {
                foreach (var cube in GameObject.FindObjectsOfType<CubeBehaviour>(true))
                {
                    Undo.RecordObject(cube.gameObject, "Disable/Enable cube");
                    cube.gameObject.SetActive(!cube.gameObject.activeSelf);
                }
            }
        }
    }  
}
