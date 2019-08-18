using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ObjectPrefab_Lever : ObjectPrefab_Base
{
    GameObject BasicImage;
    GameObject ActiveImage;

    [SerializeField]GameObject SoundA;


    protected override void Start()
    {
        base.Start();
        BasicImage = transform.GetChild(0).gameObject;
        ActiveImage = transform.GetChild(1).gameObject;
        CallChange(StartState);
    }

    protected override bool StateChangeEvent(bool _Activity)
    {
        if (false ==CurrentState)
            CallChange(_Activity);
        return _Activity;
    }

    void CallChange(bool _Activity)
    {
        if(BasicImage&& ActiveImage)
        {
            BasicImage.SetActive(!_Activity);
            ActiveImage.SetActive(_Activity);
            SoundA.SetActive(true);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SetState(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SetState(false);
        }
    }
}
