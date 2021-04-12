using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class Production : ScriptableObject
{
    [Header("Production Fields")]
    public string productionName;
    public int width;
    public int height;
    public Sprite sprite;
    public Color color;
    public GameObject spriteObject;

    [Header("Information")]
    public Sprite structureSprite;

    [Header("Unit")] 
    public List<Production> unitSprite;
}
