using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPrefab_CannonHead : ObjectPrefab_Base
{
    public float shootForce = 20;
    public Vector2 direction = Vector2.right;
    [SerializeField] protected Rigidbody2D BulletObject;
    protected Transform CannonRail;

    protected override void Start()
    {
        base.Start();
        direction.Normalize();
        Debug.Log(direction);

        if (transform.childCount>0)
        {
            CannonRail = transform.GetChild(0);
            if (CannonRail)
            {
                CannonRail.rotation = LookAt2d(Vector3.zero, direction);
            }
        }

    }


    protected override bool StateChangeEvent(bool _Activity)
    {
        if (_Activity)
        {
            Vector2 tempDirection = direction;
            if (transform.lossyScale.x < 0)
                tempDirection.x = -1* direction.x;
            if(BulletObject)
            {
                BulletObject.velocity = shootForce * tempDirection;
                BulletObject = null;
            }
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


    protected Quaternion LookAt2d(Vector3 src, Vector3 desc)
    {
        desc.x = desc.x - src.x;
        desc.y = desc.y - src.y;
        float angle = Mathf.Atan2(desc.y, desc.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}