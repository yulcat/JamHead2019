using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStageClear :ObjectPrefab_Base
{
    public StageClear StageCl;
    protected override void Start()
    {
        base.Start();
        
    }
    protected override bool StateChangeEvent(bool _Activity)
    {
        if (_Activity) StageCl.enabled = true;
        else StageCl.enabled = false;
        return _Activity;
    }
}
