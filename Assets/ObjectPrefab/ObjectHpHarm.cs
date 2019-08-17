using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHpHarm : ObjectPrefab_Base
{
    public HpHarm HpDamage;

   protected override void Start()
    {
        base.Start();
    }
    protected override bool StateChangeEvent(bool _Activity)
    {
        if (_Activity) HpDamage.enabled = true;
        else if (_Activity) HpDamage.enabled = false;
        return _Activity;
    }


}
