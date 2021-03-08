using UnityEngine;
using UnityEditor;

public class PathManager : MonoBehaviour
{
    [HideInInspector]
    public string gridName;
    [HideInInspector]
    public int x;
    [HideInInspector]
    public int z;
    [HideInInspector]
    public int level;
    [HideInInspector]
    public int width;
    [HideInInspector]
    public int height;
    [HideInInspector]
    public float nodeIdent;

    public virtual void CreateGrid(){
        GameObject gridGO = new GameObject(gridName);
        PathGrid grid = gridGO.AddComponent<PathGrid>();
        grid.nodeIdent = nodeIdent;
        gridGO.transform.position = new Vector3(x, level, z);
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                CreateNode(new Vector2(i, j), new Vector2(i * nodeIdent, j * nodeIdent), grid);
            }
        }
        grid.ConnectNodes();
    }

    public static GameObject CreateNode(Vector2 position, Vector2 realPosition, PathGrid grid){
        GameObject node = new GameObject($"Node ({position.x};{position.y})");
        node.transform.SetParent(grid.transform);
        grid.AddNode(node);
        Node nodeComponent = node.AddComponent<Node>();
        nodeComponent.position = new Vector2(position.x, position.y);
        node.transform.localPosition = new Vector3(realPosition.x, 0, realPosition.y);
        return node;
    }
}
