using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InformationMenu : MonoBehaviour
{
    public static InformationMenu Instance;

    public Text informationName;
    public Image informationSprite;
    public Text productionName;
    public Image productionSprite;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Information menu generation
    /// </summary>
    /// <param name="production"></param>
    public void InformationMenuSet(Production production)
    {
        informationName.text = production.productionName;
        informationSprite.sprite = production.structureSprite;
        productionSprite.gameObject.SetActive(false);
        
        if (production.unitSprite.Any())
        {
            productionName.text = production.unitSprite[0].productionName;
            productionSprite.sprite = production.unitSprite[0].structureSprite;
            productionSprite.gameObject.SetActive(true);
            // productionSprite.GetComponent<Button>().onClick.AddListener(delegate
            // {
            //     production.unitSprite[0].spriteObject.GetComponent<Barrack>().SS("ss");
            // });     
            productionSprite.GetComponent<Button>().onClick.AddListener(() =>
            {
                production.unitSprite[0].spriteObject.GetComponent<Barrack>().SS(5);
            }); 
        }
        
    }
}
