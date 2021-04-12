using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : AbstractBuild, IClickable
{
    public void OnMouseDown()
    {
        //menu information getter
        InformationMenu.Instance.InformationMenuSet(Resources.Load<Production>("PowerPlant"));
    }
}
