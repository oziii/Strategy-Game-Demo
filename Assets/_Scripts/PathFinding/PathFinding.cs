using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    [HideInInspector] public Node nCurrentNode;
    [HideInInspector] public List<Node> lnCalculatedList;
    [HideInInspector] public HashSet<Node> lnNotCalculatedList;
    
    private bool _findedPath;
    
    #region A* Algorithm
    
    /// <summary>
    /// H Value Calculator
    /// </summary>
    /// <param name="currentNode"></param>
    /// <param name="targetNode"></param>
    /// <returns></returns>
    private float HvalueReturn(Node currentNode, Node targetNode)
    {
        var currentPosition = currentNode.transform.position;
        var targetPosition = targetNode.transform.position;
        
        currentNode.position = new Vector2(currentPosition.x, currentPosition.y);
        targetNode.position = new Vector2(targetPosition.x, targetPosition.y);

        currentNode.hValue = Vector2.Distance(currentNode.position, targetNode.position);

        return currentNode.GetComponent<Node>().hValue;
    }

    
    /// <summary>
    /// The A * algorithm draws the shortest path through the nodes by adding the distance g relative to the neighbors and the distance h to the target node and determining the most optimal neighbor node of f.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="target"></param>
    private void FindPath(Node start, Node target)
    {
        lnCalculatedList = new List<Node>();
        lnNotCalculatedList = new HashSet<Node>();
        lnCalculatedList.Add(start);

        while (lnCalculatedList.Count > 0)
        {
            //The node to be calculated.
            nCurrentNode = lnCalculatedList[0];
            
           
            for (int i = 1; i < lnCalculatedList.Count; i++)
            {
                //If a wrong neighbor node goes away (like the next step goes to a deadlock), the node to be well calculated is chosen to come back at the end of the loop.
                if (lnCalculatedList[i].FValue < nCurrentNode.FValue 
                    || lnCalculatedList[i].FValue == nCurrentNode.FValue 
                    && lnCalculatedList[i].hValue < nCurrentNode.hValue)
                {
                    nCurrentNode = lnCalculatedList[i];
                }
            }
            
            lnCalculatedList.Remove(nCurrentNode);
            lnNotCalculatedList.Add(nCurrentNode);
            
            //When we reach the goal, we create the path and break the cycle.
            if (nCurrentNode == target)
            {
                _findedPath = true;
                GameManager.Instance.selectedUnit.Path = GetPath(start, target);
                break;
            }
            
            foreach (var neighborNodeObject in nCurrentNode.neighborList)
            {
                var neighborNode = neighborNodeObject.GetComponent<Node>();

                //Not walkable and not calculated contiune
                if (neighborNode.isBlocked
                    || lnNotCalculatedList.Contains(neighborNode))
                {
                    continue;
                }

                //Neighbor departure and neighbour's destination value
                var newPathNode = neighborNode.gValue + HvalueReturn(nCurrentNode, neighborNode);

                if (newPathNode < neighborNode.gValue || !lnCalculatedList.Contains(neighborNode))
                {
                    neighborNode.gValue = newPathNode;
                    neighborNode.hValue = HvalueReturn(neighborNode, target);
                    neighborNode.parentNode = nCurrentNode;

                    if (!lnCalculatedList.Contains(neighborNode))
                    {
                        lnCalculatedList.Add(neighborNode);
                    }
                }
            }
        }
    }

    /// <summary>
    /// We keep the path in the list by adding neighboring nodes that will come after it to the list.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    private List<Node> GetPath(Node start, Node target)
    {
        var path = new List<Node>();
        var currentNodeTemp = target;

        while (currentNodeTemp != null && currentNodeTemp != start)
        {
            path.Add(currentNodeTemp);
            currentNodeTemp = currentNodeTemp.parentNode;
        }
        
        path.Reverse();
        
        return path;
    }

    #endregion
    public void NodePathFind(Node targetNode)
    {
        _findedPath = false;

        if (!targetNode.isBlocked)
        {
            Node startNode = Grid.NodeGet(GameManager.Instance.selectedUnit.transform);
            FindPath(startNode, targetNode);

            if (!_findedPath)
            {
                print("yol bulunamadı");
            }
        }
    }

    public void NodePathFind(Node targetNode, Node startNode)
    {
        _findedPath = false;

        if (!targetNode.isBlocked)
        {
            FindPath(startNode, targetNode);

            if (!_findedPath)
            {
                print("path not found");
            }
        }
    }
    
    /// <summary>
    /// When a character moving in its path comes across an obstacle, it turns the node closest to the target and continues on its way.
    /// </summary>
    /// <param name="pathList"></param>
    /// <param name="outNode"></param>
    /// <returns></returns>
    public bool LastWalkableNode(List<Node> pathList , out Node outNode)
    {
        pathList.Reverse();
        foreach (var pathNode in pathList)
        {
            if (!pathNode.isBlocked)
            {
                outNode = pathNode;
                return true;
            }
        }
        outNode = null;
        return false;
    }
}
