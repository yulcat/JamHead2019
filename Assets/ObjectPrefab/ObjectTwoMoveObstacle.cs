using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTwoMoveObstacle : ObjectPrefab_Base
{ public TwoMoveObstacle TwoObs;
    protected override void Start()
    {
        base.Start();
        TwoObs.enabled = false;
    }
    protected override bool StateChangeEvent(bool _Activity)
    {
        if (_Activity) TwoObs.enabled = true;
        else TwoObs.enabled = false;
        return _Activity;
    }
}
