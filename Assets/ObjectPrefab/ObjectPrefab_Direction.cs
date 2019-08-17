using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPrefab_Direction : ObjectPrefab_Base
{
    public float rightDirection;
    Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }


    protected override bool StateChangeEvent(bool _Activity)
    {
        if (_Activity)
        {
            transform.localScale = new Vector2(rightDirection, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(-rightDirection, transform.localScale.y);
        }
        return _Activity;
    }

    private void Update()
    {
        if (rb.velocity.x > 0.1f)
        {
            SetState(true);
        }
        else if (rb.velocity.x < -0.1f) 
        {
            SetState(false);
        }
    }
}
