using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : AbstractUnit, IClickable
{
    private Rigidbody2D _rigid;

    public float walkSpeed;
    private void OnEnable()
    {
        _rigid= GetComponent<Rigidbody2D>();
        _rigid.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    
    public void OnMouseDown()
    {
       
        if (GameManager.Instance.gameState == GameManager.GameState.Play)
        {
            //Selected soldier       
            GameManager.Instance.startNode = Grid.NodeGet(transform);;
            GameManager.Instance.selectedUnit = this;
            GameManager.Instance.selectedSpawnBuild = null;
        }
        
        //İnformation menu
        InformationMenu.Instance.InformationMenuSet(Resources.Load<Production>("Soldier"));
    }
    
    public override IEnumerator Movement(List<Node> pathList)
    {
        Node startNode = Grid.NodeGet(transform);
        foreach (var path in pathList)
        {
            if (path.isBlocked)
            {
                Node node;
                if (GameManager.Instance.pathFinding.LastWalkableNode(pathList, out node))
                {
                    GameManager.Instance.pathFinding.NodePathFind(node, startNode);
                    yield break;
                }
                else
                {
                    print("new path");
                    break;
                }
            }
            else
            {
                startNode  = path;
                Vector3 newPos = path.position;
                Grid.NodeBlocked(transform, false);
                newPos.z = -1;
                transform.position = newPos;
                Grid.NodeBlocked(transform, true);
            }
            yield return new WaitForSeconds(.7f);
        }
    }
}
