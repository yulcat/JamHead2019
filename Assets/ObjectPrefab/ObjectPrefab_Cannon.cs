using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPrefab_Cannon : ObjectPrefab_Base
{
    protected override void Start()
    {
        base.Start();
    }


    protected override bool StateChangeEvent(bool _Activity)
    {
        if (_Activity)
        {
            //활성화 명령처리


        }
        else
        {
            //비활성화 명령처리


        }
        return _Activity;
    }


}
