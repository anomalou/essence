using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Node)), CanEditMultipleObjects]
public class NodeInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Node node = (Node)target;

        EditorGUILayout.Vector2Field("Node position", node.position);
        node.isWall = EditorGUILayout.Toggle("Solid wall", node.isWall);
    
        EditorGUILayout.LabelField("Add new node");

        if(GUILayout.Button("Up")){
            node.CreateNeighbor(new Vector2(-1, 0));
        }
        if(GUILayout.Button("Left")){
            node.CreateNeighbor(new Vector2(0, -1));
        }
        if(GUILayout.Button("Right")){
            node.CreateNeighbor(new Vector2(0, 1));
        }
        if(GUILayout.Button("Down")){
            node.CreateNeighbor(new Vector2(1, 0));
        }

        EditorGUILayout.Separator();

        if(GUILayout.Button("Connect")){
            List<GameObject> nodes = new List<GameObject>();
            nodes.AddRange(Selection.gameObjects);

            nodes.ForEach((T) => {
                Node nodeComponent = T.GetComponent<Node>();
                foreach(GameObject neighbor in nodes){
                    if(T != node){
                        nodeComponent.AddNeighbor(neighbor);
                    }
                }
            });
        }
        
        if(GUILayout.Button("Disconnect")){
            List<GameObject> nodes = new List<GameObject>();
            nodes.AddRange(Selection.gameObjects);

            nodes.ForEach((T) => {
                Node nodeComponent = T.GetComponent<Node>();
                foreach(GameObject neighbor in nodes){
                    if(T != neighbor)
                        nodeComponent.RemoveNeighbor(neighbor);
                }
            });
        }
    }
}
