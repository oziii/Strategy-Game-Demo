using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClampHelper
{
    #region ClampFields
    
    #region RotationClamper

    public static void ClamperRotaX(GameObject clampObject, float tempMinRotation, float tempMaxRotation)
    {
        Vector3 currentRotation = clampObject.transform.localRotation.eulerAngles;
        currentRotation.x = Mathf.Clamp(GetInspectorRotaValue(currentRotation.x), tempMinRotation, tempMaxRotation);
        clampObject.transform.localRotation = Quaternion.Euler (currentRotation);
    }
    
    public static void ClamperRotaY(GameObject clampObject, float tempMinRotation, float tempMaxRotation)
    {
        Vector3 currentRotation = clampObject.transform.localRotation.eulerAngles;
        currentRotation.y = Mathf.Clamp(GetInspectorRotaValue(currentRotation.y), tempMinRotation, tempMaxRotation);
        clampObject.transform.localRotation = Quaternion.Euler (currentRotation);
    }
    
    public static void ClamperRotaZ(GameObject clampObject, float tempMinRotation, float tempMaxRotation)
    {
        Vector3 currentRotation = clampObject.transform.localRotation.eulerAngles;
        currentRotation.z = Mathf.Clamp(GetInspectorRotaValue(currentRotation.z), tempMinRotation, tempMaxRotation);
        clampObject.transform.localRotation = Quaternion.Euler (currentRotation);
    }

    #endregion

    #region PositionClamper

    public static void ClamperPosX(GameObject clampObject, float tempMinPosition, float tempMaxPosition)
    {
        Vector3 currentPosition = clampObject.transform.localPosition;
        currentPosition.x = Mathf.Clamp(currentPosition.x, tempMinPosition, tempMaxPosition);
        clampObject.transform.localPosition = currentPosition;
    }
    
    public static void ClamperPosY(GameObject clampObject, float tempMinPosition, float tempMaxPosition)
    {
        Vector3 currentPosition = clampObject.transform.localPosition;
        currentPosition.y = Mathf.Clamp(currentPosition.y, tempMinPosition, tempMaxPosition);
        clampObject.transform.localPosition = currentPosition;
    }
    
    public static void ClamperPosZ(GameObject clampObject, float tempMinPosition, float tempMaxPosition)
    {
        Vector3 currentPosition = clampObject.transform.localPosition;
        currentPosition.z = Mathf.Clamp(currentPosition.z, tempMinPosition, tempMaxPosition);
        clampObject.transform.localPosition = currentPosition;
    }

    #endregion
    
    public static float GetInspectorRotaValue(float angle)
    {
        angle%=360;
        if(angle >180)
            return angle - 360;
        return angle;
    }
    
    public static Vector3 MidPoint(Vector3 a, Vector3 b)
    {
        Vector3 vector3 = Vector3.zero;
        vector3.x = (a.x + b.x) / 2;
        vector3.y = (a.y + b.y) / 2;
        vector3.z = (a.z + b.z) / 2;
        return vector3;
    }
    
    #endregion
}
