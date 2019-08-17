using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Joint2D))]
public class ObjectPrefab_CannonBody : ObjectPrefab_Catcher
{
    [Header("장치 설정")]
    [SerializeField] protected float ShotDelay = 1;
    [Header("장치 설정")]
    [SerializeField] protected ObjectPrefab_CannonHead Cannon;

    [Header("상태")]
    [SerializeField] protected float ShotCooldown = 0;
    [SerializeField] protected float CurrentShotDelay = 0;


    protected override void Start()
    {
        base.Start();


    }


    protected override void Update()
    {
        if(CurrentShotDelay > 0)
        {
            CurrentShotDelay -= Time.deltaTime;
            if(CurrentShotDelay <= 0)
            {
                JointObject(null);
                Cannon.SetState(true);
            }
        }
        else if(ShotCooldown<=0)
            base.Update();
        else
        {
            ShotCooldown -= Time.deltaTime;
            if (ShotCooldown <= 0)
                if(CurrentState)
                    Observer.ChangeState(true);
        }

    }

    protected override bool StateChangeEvent(bool _Activity)
    {
        if (Observer)
            return Observer.ChangeState(_Activity);
        return _Activity;
    }


    protected override Rigidbody2D JointObject(Rigidbody2D _RigidbodyObject)
    {

        if(_RigidbodyObject)
        {
            ShotCooldown = 1;
            CurrentShotDelay = ShotDelay;
            Cannon.HeadBulletCharge(_RigidbodyObject);
            Observer.ChangeState(false);
            Debug.Log("장전");
        }
        return base.JointObject(_RigidbodyObject);
    }



    
}
