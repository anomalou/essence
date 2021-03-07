using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathManager))]
public class PathManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PathManager pathManager = (PathManager)target;

        EditorGUILayout.LabelField("Create path grid");
        pathManager.gridName = EditorGUILayout.TextField("Grid name", pathManager.gridName);
        pathManager.x = EditorGUILayout.IntField("X", pathManager.x);
        pathManager.z = EditorGUILayout.IntField("Z", pathManager.z);
        pathManager.level = EditorGUILayout.IntField("Level", pathManager.level);
        pathManager.width = EditorGUILayout.IntField("Grid width", pathManager.width);
        pathManager.height = EditorGUILayout.IntField("Grid height", pathManager.height);
        // pathManager.widthPadding = EditorGUILayout.FloatField("Path node width", pathManager.widthPadding);
        // pathManager.heightPadding = EditorGUILayout.FloatField("Path node height", pathManager.heightPadding);

        if(GUILayout.Button("Create grid")){
            pathManager.CreateGrid();
        }
    }
}
