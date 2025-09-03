using UnityEngine;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(CubeBehaviour)), CanEditMultipleObjects]
public class CubeBehaviour : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Select all cubes"))
        {
            var allCubeBehaviour = GameObject.FindObjectsOfType<CubeBehaviour>();
            var allCubeGameObjects = allCubeBehaviour.Select(cube => cube.gameObject).ToArray();
            Selection.objects = allCubeGameObjects;
        }
    }
}