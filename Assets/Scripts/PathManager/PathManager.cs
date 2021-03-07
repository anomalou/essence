using UnityEngine;

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
    // [HideInInspector]
    // public float widthPadding;
    // [HideInInspector]
    // public float heightPadding;

    public virtual void CreateGrid(){
        GameObject gridGO = new GameObject(gridName);
        PathGrid grid = gridGO.AddComponent<PathGrid>();
        grid.Init();
        gridGO.transform.position = new Vector3(x, level, z);
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                CreateNode(new Vector2(i, j), grid);
                // GameObject node = new GameObject($"Node ({i};{j})");
                // node.transform.SetParent(gridGO.transform);
                // grid.AddNode(node);
                // node.transform.localPosition = new Vector3(i * widthPadding, 0, j * heightPadding);
                // Node nodeComponent = node.AddComponent<Node>();
                // nodeComponent.Init();
                // nodeComponent.position = new Vector2(i, j);
            }
        }
        grid.ConnectNodes();
    }

    public static GameObject CreateNode(Vector2 position, PathGrid grid){
        GameObject node = new GameObject($"Node ({position.x};{position.y})");
        node.transform.SetParent(grid.transform);
        grid.AddNode(node);
        node.transform.localPosition = new Vector3(position.x , 0, position.y);
        Node nodeComponent = node.AddComponent<Node>();
        nodeComponent.Init();
        nodeComponent.position = new Vector2(position.x, position.y);
        return node;
    }
}
