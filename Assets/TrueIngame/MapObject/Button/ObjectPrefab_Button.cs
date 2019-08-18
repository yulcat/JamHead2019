using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPrefab_Button : ObjectPrefab_Base
{
    protected override void Start()
    {
        base.Start();
    }

    float ChangeSpeed = 4.0f;

    float Timer = 0;
    private void Update()
    {   if (transform.childCount==0) return;
        if(Timer!=0)
        {
            if(CurrentState)
            {
                if (0 < (Timer += Time.deltaTime* ChangeSpeed))
                    Timer = 0;
                transform.GetChild(0).localPosition = Vector3.Lerp(Vector3.zero, Vector3.down * 0.5f, 1+Timer);
            }
            else
            {
                if (0 > (Timer -= Time.deltaTime * ChangeSpeed))
                    Timer = 0;
                transform.GetChild(0).localPosition = Vector3.Lerp(Vector3.zero, Vector3.down * 0.5f, Timer);
            }
        }
    }


    protected override bool StateChangeEvent(bool _Activity)
    {
        if (CurrentState != _Activity)
            Timer = _Activity ? -1 : 1;
        return _Activity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Head"))
        {
            Debug.Log("트루호출" + CurrentState);
            SetState(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Head"))
        {
            SetState(false);
        }
    }


}