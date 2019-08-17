using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPrefab_CannonHead : ObjectPrefab_Base
{
    public float shootForce = 20;
    public Vector2 direction = Vector2.right;
    //PlayerController playerController;
    [SerializeField] protected Rigidbody2D BulletObject;

    protected override void Start()
    {
        base.Start();
        direction.Normalize();
        //playerController = transform.parent.GetComponent<PlayerController>();
    }


    protected override bool StateChangeEvent(bool _Activity)
    {
        if (_Activity)
        {
            Vector2 tempDirection = direction;
            if (transform.lossyScale.x < 0)
                tempDirection.x *= -1;
            BulletObject.velocity = shootForce * tempDirection;
            BulletObject = null;
            return false;
        }
        else
        {

        }
        return _Activity;
    }

    public void HeadBulletCharge(Rigidbody2D _BulletObject)
    {
        BulletObject = _BulletObject;
    }
}