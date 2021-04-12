using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private GameObject _productionParent;
    
    #region Builder_Methods

    private void OnEnable()
    {
        GameManager.EscClicked += ClearLastBuild;
    }

    /// <summary>
    /// Creating the Structure Product In the mouse position
    /// </summary>
    /// <param name="mousePosition"></param>
    /// <param name="production"></param>
    /// <param name="size"></param>
    public void BuildCreater(Vector3 mousePosition, Production production, float size)
    {
        if (_productionParent != null)
        {
            ClearLastBuild();
        }
        _productionParent = new GameObject(production.productionName);

        for (int x = 0; x < production.width; x++)
        {
            for (int y = 0; y < production.height; y++)
            {
                var pos = new Vector3(x*size, y*size, -1);
                
                //var tempObject = Instantiate(production.spriteObject, pos, Quaternion.identity);
                
                var tempObject = ObjectPooler.SharedInstance.GetPooledObject(production.spriteObject.tag);
                tempObject.SetActive(true);
                tempObject.transform.position = pos;
                tempObject.transform.localScale = Vector3.one * size;
                tempObject.name = production.productionName + x + "," + y;
                tempObject.transform.parent = _productionParent.transform;
                
            }
        }
        
        List<Transform> childList = new List<Transform>();
        foreach (Transform child in _productionParent.transform)
        {
            childList.Add(child);
        }

        StartCoroutine(BuildController(childList, _productionParent, production));
    }
    
    /// <summary>
    /// Checking if the location where the building was created is the right area
    /// </summary>
    /// <returns></returns>
    IEnumerator BuildController(List<Transform> list, GameObject productionParent, Production production)
    {
        while (GameManager.Instance.gameState == GameManager.GameState.Building)
        {
            if (!GameManager.Instance.MouseDown)
            {
                Control(list);
                productionParent.transform.position = GameObjectHelper.GetMouseWorldPosition();
            }
            else
            {
                Build(list, Control(list), production);
            }

            yield return null;
        }
    }

    /// <summary>
    /// It gives feedback by controlling the nodes of the structure on the grid.
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public bool Control(List<Transform> list)
    {
        bool canBuild = true;
        foreach (Transform child in list)
        {
            RaycastHit2D hit = Physics2D.Raycast(child.position, Vector2.down, Mathf.Infinity, GameManager.Instance.layerMask);
            try
            {
                if (hit.collider.TryGetComponent(out Node node))
                {
                    child.GetComponent<SpriteRenderer>().color = Color.green;
                    if (node.isBlocked)
                    {
                        child.GetComponent<SpriteRenderer>().color = Color.red;
                        canBuild = false;
                    }
                }
            }
            catch(NullReferenceException e)
            {
                return false;
            }
        }
        return canBuild;
    }


    /// <summary>
    /// If the building is in the right place, it is created.
    /// </summary>
    public void Build(List<Transform> list, bool control, Production production)
    {
        if (control)
        {
            GameManager.Instance.gameState = GameManager.GameState.Play;
            var color = production.color;
            foreach (Transform child in list)
            {
                var node = Grid.NodeGet(child);
                
                node.isBlocked = true;
                    
                child.GetComponent<SpriteRenderer>().color = new Color(color.r,color.g,color.b);
                
            }
            print("Build Completed");
            _productionParent = null;
            
            ISpawn spawn = list.First().GetComponent<ISpawn>();
            if (spawn != null)
            {
                StartCoroutine(list.First().GetComponent<ISpawn>().UnitSpawn(production, ClampHelper.MidPoint(list.First().position, list.Last().position)));
            }

        }
        else
        {
            print("Cannot this here build");
        }
    }
    
    
    /// <summary>
    /// for a wrong click or exit with esc
    /// </summary>
    private void ClearLastBuild()
    {
        if (_productionParent != null)
        {
            while (_productionParent.transform.childCount != 0)
            {
               Transform child  = _productionParent.transform.GetChild(0);
               child.parent = null;
               child.gameObject.SetActive(false);
            }

            Destroy(_productionParent);
        }
    }
    
    #endregion
}
