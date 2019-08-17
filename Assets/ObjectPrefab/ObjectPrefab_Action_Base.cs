using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPrefab_Action_Base : MonoBehaviour
{
    public virtual bool ChangeState(bool _Activity)
    {
        this.enabled = _Activity;
        return _Activity;
    }
}
