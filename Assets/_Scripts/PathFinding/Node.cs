using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : AbstractNode, IRightClickable
{
    [Tooltip("List of node neighbors")]
    public List<GameObject> neighborList = new List<GameObject>();
    [Tooltip("To hold the next node after the node")]
    public Node parentNode;
    [Tooltip("To hide the Vector of the node")]
    public Vector2 position;
    [Tooltip("Is the node walkable")]
    public bool isBlocked;
    [Tooltip("Distance to target")]
    public float gValue;
    [Tooltip("Distance to neighbor")]
    public float hValue; 
    public float FValue => gValue + hValue;

    private void OnEnable()
    {
        position = transform.position;
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<AbstractNode>() != null)
        {
            neighborList.Add(other.gameObject);
        }
    }

    public void OnMouseOver()
    {
        if (!isBlocked
            && Input.GetMouseButtonDown(1)
            && GameManager.Instance.startNode != null 
            && GameManager.Instance.gameState == GameManager.GameState.Play)
        {
            GameManager.Instance.pathFinding.NodePathFind(this);
        }
    }
    
    
}