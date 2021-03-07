using UnityEngine;

public interface IPathGrid
{
    void Init();
    GameObject[] GetNodes();
    void RemoveNode(GameObject node);
    GameObject GetNode(Vector2 position);
}
