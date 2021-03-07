using System.Collections.Generic;
using UnityEngine;

public class PathGrid : MonoBehaviour, IPathGrid
{
    #region Variables
    private List<GameObject> nodes;

    #endregion

    #region Methods
    public GameObject[] GetNodes()
    {
        return nodes.ToArray();
    }

    public GameObject GetNode(Vector2 position)
    {
        return nodes.Find(T => T.GetComponent<Node>().position == position);
    }

    public void AddNode(GameObject node){
        nodes.Add(node);
    }

    public void RemoveNode(GameObject node)
    {
        if(nodes.Contains(node))
            nodes.Remove(node);
    }

    public void Init()
    {
        nodes = new List<GameObject>();
    }

    public void ConnectNodes(){
        nodes.ForEach((T => {
            Node node = T.GetComponent<Node>();
            GameObject neighbor;
            Vector2 pos = node.position;
            if((neighbor = GetNode(pos + new Vector2(-1, -1))) != null){
                node.AddNeighbor(neighbor);
            }
            if((neighbor = GetNode(pos + new Vector2(0, -1))) != null){
                node.AddNeighbor(neighbor);
            }
            if((neighbor = GetNode(pos + new Vector2(1, -1))) != null){
                node.AddNeighbor(neighbor);
            }
            if((neighbor = GetNode(pos + new Vector2(-1, 0))) != null){
                node.AddNeighbor(neighbor);
            }
            if((neighbor = GetNode(pos + new Vector2(1, 0))) != null){
                node.AddNeighbor(neighbor);
            }
            if((neighbor = GetNode(pos + new Vector2(-1, 1))) != null){
                node.AddNeighbor(neighbor);
            }
            if((neighbor = GetNode(pos + new Vector2(0, 1))) != null){
                node.AddNeighbor(neighbor);
            }
            if((neighbor = GetNode(pos + new Vector2(1, 1))) != null){
                node.AddNeighbor(neighbor);
            }
        }));
    }

    #endregion
}
