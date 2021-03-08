using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathGrid))]
public class PathGridInspector : Editor
{
    public override void OnInspectorGUI() {
        PathGrid pathGrid = (PathGrid)target;

        pathGrid.nodeIdent = EditorGUILayout.FloatField("Node ident", pathGrid.nodeIdent);

        if(GUILayout.Button("Set ident")){
            foreach (GameObject node in pathGrid.GetNodes()){
                Node nodeComponent = node.GetComponent<Node>();
                node.transform.localPosition = new Vector3(nodeComponent.position.x * pathGrid.nodeIdent, node.transform.localPosition.y, nodeComponent.position.y * pathGrid.nodeIdent);
            }
        }

        if(GUILayout.Button("Reconnect nodes")){
            pathGrid.ConnectNodes();
        }
    }
}
