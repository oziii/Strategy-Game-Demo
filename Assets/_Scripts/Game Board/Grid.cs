using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Node nodeObj;
    
    [SerializeField] private int width;
    [SerializeField] private int height;
    
    [SerializeField] private float cellSize;
    
    private Node[,] _gridArray;
    
    
    
    #region Unity_Methods

    private void Awake()
    {
        GridCreate();
    }

    #endregion

    #region Grid_Methods

    private void GridCreate()
    {
        _gridArray = new Node[width,height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _gridArray[x,y] = NodeCreate(nodeObj,x,y,cellSize);
            }
        }
    }

    private Node NodeCreate(Node node, int x, int y, float size)
    {
        var pos = new Vector3(x * size, y * size);
        Node nodeObject = Instantiate(node, pos, Quaternion.identity, transform);
        nodeObject.transform.localScale = Vector3.one * size;

        nodeObject.name = "node " + x + "," + y;
        
        Rigidbody2D nodeRigid = nodeObject.GetComponent<Rigidbody2D>();
        nodeRigid.constraints = RigidbodyConstraints2D.FreezeAll;

        return nodeObject;
    }


    public float CellSize()
    {
        return cellSize;
    }

    /// <summary>
    /// Changes the walkability of the node above it. returns.
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="value"></param>
    public static void NodeBlocked(Transform pos, bool value)
    {
        Node node = NodeGet(pos);
        if (node != null && node.isBlocked != value)
        {
            node.isBlocked = value;
        }
    }

    /// <summary>
    /// Returns the node located above it.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static Node NodeGet(Transform pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos.position, Vector2.down, Mathf.Infinity, GameManager.Instance.layerMask);
        return hit.collider.TryGetComponent(out Node node) ? node : null;
    }
    
    public static Node NodeGet(Vector3 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down, Mathf.Infinity, GameManager.Instance.layerMask);
        return hit.collider.TryGetComponent(out Node node) ? node : null;
    }
    
    #endregion
}
