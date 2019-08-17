using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPrefab_Cannon : ObjectPrefab_Base
{
    public float shootForce;
    public Vector2 direction;
    PlayerController playerController;
    Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        direction.Normalize();
        playerController = transform.parent.GetComponent<PlayerController>();
    }


    protected override bool StateChangeEvent(bool _Activity)
    {
        if (_Activity)
        {
            //활성화 명령처리
            rb.velocity = shootForce * direction;
            return false;
        }
        else
        {
            //비활성화 명령처리


        }
        return _Activity;
    }

    public void HeadShooting(Rigidbody2D _rb)
    {
        rb = _rb;
    }
}
