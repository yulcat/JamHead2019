using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Joint2D))]
public class ObjectPrefab_Catcher : ObjectPrefab_Catcher_Base
{



    [Header("자체 정보들")]
    [SerializeField] protected LineRenderer LineRender;
    [SerializeField] protected Transform StartPointer;
    [SerializeField] protected Transform EndPointer;

    [SerializeField] protected GameObject DisableImage;
    [SerializeField] protected GameObject EnableImage;

    protected override void Start()
    {
        base.Start();


        StartPointer = transform.GetChild(0);
        LineRender = transform.GetChild(1).GetComponent<LineRenderer>();
        EndPointer = transform.GetChild(2);

        DisableImage = StartPointer.GetChild(0).gameObject;
        EnableImage = StartPointer.GetChild(1).gameObject;

        LineRender.SetPosition(0, StartPointer.localPosition);
        LineRender.SetPosition(1, EndPointer.localPosition);

        SetImage(StartState);
    }




    protected override bool StateChangeEvent(bool _Activity)
    {
        base.StateChangeEvent(_Activity);
        if (StartPointer)
            SetImage(_Activity);
        return _Activity;
    }



    void SetImage(bool _Activity)
    {
        DisableImage.SetActive(!_Activity);
        EnableImage.SetActive(_Activity);
    }

}