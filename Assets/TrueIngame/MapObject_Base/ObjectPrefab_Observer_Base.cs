using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPrefab_Observer_Base : ObjectPrefab_Action_Base
{
    [SerializeField] protected LayerMask FindLayerMask;
    [SerializeField] protected GameObject LastFindObject = null;

    public override bool ChangeState(bool _Activity)
    {
        this.enabled = _Activity;
            LastFindObject = null;
        return _Activity;
    }

    public virtual GameObject Observing()
    {
        return LastFindObject;
    }
}