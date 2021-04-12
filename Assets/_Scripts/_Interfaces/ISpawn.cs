using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawn
{
    IEnumerator UnitSpawn(Production production, Vector3 pos);
    IEnumerator UnitSpawn(Production production);
    
    
}
