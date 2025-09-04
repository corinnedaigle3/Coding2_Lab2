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

        //Creates button to select all the cubes
        if (GUILayout.Button("Select all cubes"))
        {
            var allCubeBehaviour = GameObject.FindObjectsOfType<CubeBehaviour>();
            var allCubeGameObjects = allCubeBehaviour.Select(cube => cube.gameObject).ToArray();
            Selection.objects = allCubeGameObjects;
        }
        //Creates button to clear the selection 
        if (GUILayout.Button("Clear selection"))
        {
            Selection.objects = new Object[] {(target as CubeBehaviour).gameObject};
        }

        EditorGUILayout.EndHorizontal();

        var cachedColor = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;

        using (new EditorGUILayout.HorizontalScope())
        {
            //Creates button to disable/enable all the cubes
            if (GUILayout.Button("Disable/Enable all cubes", GUILayout.Height(40)))
            {
                //Loop that controls the actual enabling/disabling of the game objects
                foreach (var cube in GameObject.FindObjectsOfType<CubeBehaviour>(true))
                {
                    Undo.RecordObject(cube.gameObject, "Disable/Enable cube");
                    cube.gameObject.SetActive(!cube.gameObject.activeSelf);
                }
            }
        }
    }  
}
