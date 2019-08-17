using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTwoMoveOnly : ObjectPrefab_Base {
    // Start is called before the first frame update
    public TwoMoveOnly TwoOnly;
    protected override void Start()
    {
        base.Start();
        TwoOnly.enabled = false;
    }
    protected override bool StateChangeEvent(bool _Activity)
    {
        if (_Activity) TwoOnly.enabled = true;
        else TwoOnly.enabled = false;
        return _Activity;
    }
}
