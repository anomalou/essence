using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, Serializable]
public class Node : MonoBehaviour
{
    #region Variables
    [SerializeField]
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
    [SerializeField]
    public bool isWall;
    [HideInInspector]
    [SerializeField]
    private List<GameObject> _neighbors;
    public GameObject[] neighbors{
        get{
            return _neighbors.ToArray();
        }
    }

    #endregion

    #region InspectorMethods

    public void AddNeighbor(GameObject neighbor){
        if(_neighbors == null)
            _neighbors = new List<GameObject>();
        _neighbors.Add(neighbor);
    }

    public GameObject CreateNeighbor(Vector2 position){
        PathGrid grid = transform.GetComponentInParent<PathGrid>();
        GameObject node = PathManager.CreateNode(this.position + position, new Vector2(transform.localPosition.x + position.x * grid.nodeIdent, transform.localPosition.z + position.y * grid.nodeIdent), grid);
        AddNeighbor(node);
        node.GetComponent<Node>().AddNeighbor(gameObject);
        return node;
    }

    public void RemoveNeighbor(GameObject neighbor)
    {
        if(_neighbors.Contains(neighbor))
            _neighbors.Remove(neighbor);
    }

    private void OnDrawGizmos() {
        if(!isWall)
            Gizmos.DrawIcon(transform.position, "Node.png", true);
        else
            Gizmos.DrawIcon(transform.position, "LockNode.png", true);
    }

    private void OnDrawGizmosSelected() {
        foreach(GameObject neighbor in _neighbors){
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }

    void OnDestroy() {
        if(!Application.isPlaying){
            PathGrid grid = transform.GetComponentInParent<PathGrid>();
            if(grid != null){
                grid.RemoveNode(gameObject);
                _neighbors.ForEach(T => T.GetComponent<Node>().RemoveNeighbor(gameObject));
            }
        }else
            return;
    }

    #endregion
}
