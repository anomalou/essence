using UnityEngine;

public interface INode
{
    void Init();
    Vector2 position{get;set;}
    GameObject[] GetNeighbors();
    void RemoveNeighbor(GameObject neighbor);
}
