using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuild : AbstractBuild, ISpawn
{
    [Tooltip(" Search empty node layer")]
    public ContactFilter2D layerMask;

    public float range;
    
    public bool selected;

    public Vector3 flagPos;


    public GameObject Flag { get; set; }

    public GameObject FlagAdd(GameObject flag, Vector3 pos)
    {
        if(flag == null)
        {
            flag = Instantiate(Resources.Load<GameObject>("Flag"));
        }
        
        var node = Grid.NodeGet(pos);
        if (node == null || node.isBlocked)
        {
            print("Cannot this here flag");
            FlagAdd(flag, GameObjectHelper.GetMouseWorldPosition());
        }
        flagPos = node.position;

        flag.transform.position = flagPos;
        return flag;
    }
    
    /// <summary>
    ///  Character spawn on empty nodes around the building.
    /// </summary>
    /// <param name="production"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public IEnumerator UnitSpawn(Production production, Vector3 pos)
    {
        List<Collider2D> nodes = new List<Collider2D>();
        HashSet<Node> spawnNodeList = new HashSet<Node>();
        
        if (Physics2D.OverlapCircle(pos, range, layerMask, nodes) > 1)
        {
            foreach (var nodeVar in nodes)
            {
                if (nodeVar.TryGetComponent(out Node node))
                {
                    if (!node.isBlocked)
                    {
                        spawnNodeList.Add(node);
                    }
                }
            }
            
            if (spawnNodeList.Count != 0) { Spawn(spawnNodeList, production.unitSprite.RandomItem()); }
            
            yield return null;
        }
    }
    
    /// <summary>
    /// A unit was created on a random node from the list of empty nodes.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="unit"></param>
    public void Spawn(HashSet<Node> list, Production unit)
    {
        var pos = list.RandomRemoveHashItem().gameObject.transform.position;
        //object pool
        var soldier = ObjectPooler.SharedInstance.GetPooledObject(TAGS.Soldier);
        soldier.SetActive(true);
        pos.z = -1;
        soldier.transform.position = pos;
        soldier.name = unit.productionName + " " + pos.x + ","+pos.y;
        Grid.NodeBlocked(soldier.transform, true);
    }
    
    
}
