using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPrefab_Object : ObjectPrefab_Base
{
    private ObjectPrefab_Action_Base Action_Script;

    protected override void Start()
    {
        base.Start();
        Action_Script = transform.GetComponent<ObjectPrefab_Action_Base>();
        if (Action_Script)
            Action_Script.ChangeState(CurrentState);
    }

    protected override bool StateChangeEvent(bool _Activity)
    {
        if(Action_Script)
            return Action_Script.ChangeState(_Activity);
        return _Activity;
    }
}