using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;


public class Barrack : SpawnBuild, IClickable, ISpawnButton
{
    #region Unity_Methods
    
    /// <summary>
    /// It organizes the information menu where you touch the product.
    /// </summary>
    public void OnMouseDown()
    {
        selected = true;
        GameManager.Instance.startNode = null;
        GameManager.Instance.selectedUnit = null;
        GameManager.Instance.selectedSpawnBuild = transform.parent.GetChild(0).gameObject;
        InformationMenu.Instance.InformationMenuSet(Resources.Load<Production>("Barrack"));
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && GameManager.Instance.selectedSpawnBuild == gameObject)
        {
            var parent = transform.parent;
            parent.GetChild(0).GetComponent<ISpawn>().Flag = FlagAdd(parent.GetChild(0).GetComponent<ISpawn>().Flag, GameObjectHelper.GetMouseWorldPosition());
            selected = false;
        }
    }
    
    
    #endregion

    #region Barrack_Methods

    public void Spawn()
    {
        var production = Resources.Load<Production>("Barrack");
        StartCoroutine( transform.parent.GetChild(0).GetComponent<ISpawn>().UnitSpawn(production, GetComponent<ISpawn>().Flag.transform.position));
    }

    #endregion
    
}
