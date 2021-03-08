using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Node)), CanEditMultipleObjects]
public class NodeInspector : Editor
{
    enum ConnectionType{
        Star,
        Cross,
        All
    }

    Node node;
    ConnectionType connectionType = ConnectionType.All;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        node = (Node)target;

        EditorGUILayout.Vector2Field("Node position", node.position);
        node.isWall = EditorGUILayout.Toggle("Solid wall", node.isWall);
    
        EditorGUILayout.LabelField("Add new node");

        if(GUILayout.Button("-x")){
            Selection.activeGameObject = node.CreateNeighbor(new Vector2(-1, 0));
        }
        if(GUILayout.Button("+x")){
            Selection.activeGameObject = node.CreateNeighbor(new Vector2(1, 0));
        }
        if(GUILayout.Button("-z")){
            Selection.activeGameObject = node.CreateNeighbor(new Vector2(0, -1));
        }
        if(GUILayout.Button("+z")){
            Selection.activeGameObject = node.CreateNeighbor(new Vector2(0, 1));
        }

        EditorGUILayout.Separator();

        connectionType = (ConnectionType)EditorGUILayout.EnumPopup("Connection type", connectionType);

        if(GUILayout.Button("Connect")){
            List<GameObject> nodes = new List<GameObject>();
            nodes.AddRange(Selection.gameObjects);

            switch(connectionType){
                case ConnectionType.Star:
                    StarConnect(nodes);
                break;
                case ConnectionType.Cross:
                    CrossConnect(nodes);
                break;
                case ConnectionType.All:
                    AllConnect(nodes);
                break;
            }
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

    private void StarConnect(List<GameObject> nodes){
        //in development
    }

    private void CrossConnect(List<GameObject> nodes){
        //in development
    }

    private void AllConnect(List<GameObject> nodes){
        nodes.ForEach((T) => {
            Node nodeComponent = T.GetComponent<Node>();
            List<GameObject> currentNeighbors = new List<GameObject>();
            currentNeighbors.AddRange(nodeComponent.neighbors);
            foreach(GameObject neighbor in nodes){
                if(T != neighbor){
                    if(!currentNeighbors.Contains(neighbor))
                        nodeComponent.AddNeighbor(neighbor);
                }
            }
        });
    }
}
