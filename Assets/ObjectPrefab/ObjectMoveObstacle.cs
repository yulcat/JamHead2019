using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveObstacle : ObjectPrefab_Base
{
    public MoveObstacle MoveObs;
    private Vector2 Ori;
    
    protected override void Start()
    {
        base.Start();Ori = MoveObs.gameObject.transform.position;
          
    }
     
        
    protected override bool StateChangeEvent(bool _Activity)
    {

        if (_Activity) MoveObs.enabled = true;
        else { MoveObs.enabled = false; MoveObs.gameObject.transform.position = Ori; }
        return _Activity;
    }
}
