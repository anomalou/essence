using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Node : MonoBehaviour, INode
{
    #region Variables
    private Vector2 _position;
    [HideInInspector]
    public Vector2 position{
        get{
            return new Vector2(_position.x, _position.y);
        }
        set{
            if(_position == Vector2.zero)
                _position = value;
        }
    }

    [HideInInspector]
    public bool isWall;
    private List<GameObject> neighbors;

    #endregion

    #region InspectorMethods
    public GameObject[] GetNeighbors()
    {
        if(neighbors == null)
            throw new UnityException("Invalide node!");
        else
            return neighbors.ToArray();
    }

    public void AddNeighbor(GameObject neighbor){
        neighbors.Add(neighbor);
    }

    public void CreateNeighbor(Vector2 position){
        GameObject node = new GameObject($"Node ({this.position.x + position.x};{this.position.y + position.y})");
        Node nodeComponent = node.AddComponent<Node>();
        nodeComponent.Init();
        nodeComponent.position = this.position + position;
        PathGrid grid = transform.GetComponentInParent<PathGrid>();
        grid.AddNode(node);
        node.transform.SetParent(transform.parent);
        node.transform.localPosition = new Vector3(transform.localPosition.x + position.x, 0, transform.localPosition.y + position.y);
        AddNeighbor(node);
        nodeComponent.AddNeighbor(gameObject);
    }

    public void RemoveNeighbor(GameObject neighbor)
    {
        if(neighbors.Contains(neighbor))
            neighbors.Remove(neighbor);
    }

    private void OnDrawGizmos() {
        if(!isWall)
            Gizmos.DrawIcon(transform.position, "Node.png", true);
        else
            Gizmos.DrawIcon(transform.position, "LockNode.png", true);
    }

    private void OnDrawGizmosSelected() {
        foreach(GameObject neighbor in neighbors){
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }

    public void Init()
    {
        neighbors = new List<GameObject>();
    }

    void OnDestroy() {
        if(!Application.isPlaying){
            PathGrid grid = transform.GetComponentInParent<PathGrid>();
            if(grid != null){
                grid.RemoveNode(gameObject);
                neighbors.ForEach(T => T.GetComponent<Node>().RemoveNeighbor(gameObject));
            }
        }else
            return;
    }

    #endregion
}
