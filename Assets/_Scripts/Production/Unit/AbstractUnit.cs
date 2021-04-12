using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUnit : MonoBehaviour
{

    /// <summary>
    /// To stop a pre-planned path of the character and give it a new path.
    /// </summary>
    public virtual List<Node> Path
    {
        get => _path;
        set
        {
            _path = value;
            
            StopCoroutine(nameof(Movement));
            StartCoroutine(nameof(Movement), _path);
        }
    }
    private List<Node> _path;
    
    /// <summary>
    /// The unit moves to the node path between two points.
    /// </summary>
    /// <param name="pathList"></param>
    /// <returns></returns>
    public abstract IEnumerator Movement(List<Node> pathList);
}
