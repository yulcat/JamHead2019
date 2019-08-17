using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPrefab_Dead : ObjectPrefab_Base
{
    private Vector2 deadPos;
    public bool isDead;

    protected override void Start()
    {
        base.Start();
        isDead = false;
    }


    protected override bool StateChangeEvent(bool _Activity)
    {
        if (_Activity)
        {
            
            //활성화 명령처리
            isDead = true;
            deadPos = transform.position;
           ;
        }
        else
        {


        }
        return _Activity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SetState(true);
        }
    }

    private void Update()
    {
        if (isDead)
        {
            transform.position = deadPos;
        }
    }
}
