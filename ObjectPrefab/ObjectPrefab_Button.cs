using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPrefab_Button : ObjectPrefab_Base
{
    protected override void Start()
    {
        base.Start();
    }

    protected override bool StateChangeEvent(bool _Activity)
    {
        if(_Activity)
        {
            //활성화 명령처리


        }
        else
        {
            //비활성화 명령처리


        }
        return _Activity;
    }

    //함수로 눌리면
    //SetState(ture); 호출해주면됨


}
