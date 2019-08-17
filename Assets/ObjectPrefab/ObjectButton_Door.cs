using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectButton_Door : ObjectPrefab_Base
{ public DoorMoving Door;
    protected override void Start()
    {
        base.Start();
        Door.enabled = false;

    }
    protected override bool StateChangeEvent(bool _Activity)
    {
        if (_Activity) { Door.ForTest = false; Door.IsTouch = true; Door.enabled = true; }
        else { Door.IsTouch = false; Door.ForTest = false; }
            return _Activity;
    }
}
