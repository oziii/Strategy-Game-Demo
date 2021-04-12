using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectHelper 
{
    public static IEnumerator EnableFalser(GameObject tempGameObject, bool value, float duration)
    {
        yield return new WaitForSeconds(duration);
        tempGameObject.SetActive(value);
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vector3.z = 0;
        return vector3;
    }
}
