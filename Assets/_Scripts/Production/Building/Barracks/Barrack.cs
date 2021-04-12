using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;


public class Barrack : AbstractBuild, ISpawn, IClickable
{
    [Tooltip(" Search empty node layer")]
    public ContactFilter2D layerMask;

    public float range;

    public float spawnTime;

    private Vector3 _midPoint;
    
    #region Unity_Methods

    private void OnEnable()
    {
        var parent = transform.parent;
        _midPoint = ClampHelper.MidPoint(parent.GetChild(0).position, parent.GetChild(parent.childCount).position);
    }

    /// <summary>
    /// It organizes the information menu where you touch the product.
    /// </summary>
    public void OnMouseDown()
    {
        InformationMenu.Instance.InformationMenuSet(Resources.Load<Production>("Barrack"));
    }
    
    #endregion

    #region Barrack_Methods
    
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
        
        while (Physics2D.OverlapCircle(pos, range, layerMask, nodes) > 1)
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
            yield return new WaitForSeconds(spawnTime);
            
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
    
    #endregion



}
