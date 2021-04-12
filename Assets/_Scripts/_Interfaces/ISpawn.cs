using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawn
{
    GameObject Flag { get; set; }
    GameObject FlagAdd(GameObject flag , Vector3 pos);
    IEnumerator UnitSpawn(Production production, Vector3 pos);
    
}
