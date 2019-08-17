using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Joint2D))]
public class ObjectPrefab_Catcher : ObjectPrefab_Base
{

    protected Rigidbody2D m_Rigid;
    protected Joint2D m_Joint;
    protected ObjectPrefab_Observer_Base Observer;

    [Header("장치 설정")]
    [SerializeField] protected bool ResetMode_Rigidbody = true;
    [SerializeField] protected Transform CatchPosition;
    [Header("붙잡은 오브젝트")]
    [SerializeField] protected Rigidbody2D CatchObject;


    [Header("자체 정보들")]
    [SerializeField] protected LineRenderer LineRender;
    [SerializeField] protected Transform StartPointer;
    [SerializeField] protected Transform EndPointer;

    [SerializeField] protected GameObject DisableImage;
    [SerializeField] protected GameObject EnableImage;

    protected override void Start()
    {
        base.Start();
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Joint = GetComponent<Joint2D>();
        Observer = transform.GetChild(0).GetComponent<ObjectPrefab_Observer_Base>();
        if (Observer)
            Observer.ChangeState(CurrentState);



        StartPointer = transform.GetChild(0);
        LineRender = transform.GetChild(1).GetComponent<LineRenderer>();
        EndPointer = transform.GetChild(2);



        DisableImage = StartPointer.GetChild(0).gameObject;
        EnableImage = StartPointer.GetChild(1).gameObject;


        LineRender.SetPosition(0, StartPointer.localPosition);
        LineRender.SetPosition(1, EndPointer.localPosition);

        SetImage(StartState);
    }


    protected virtual void Update()
    {
        if (Observer && Observer.enabled )
        {
            if(Observer.Observing())
            {
                Debug.Log("옵저빙판단성공");
                JointObject(Observer.Observing().GetComponent<Rigidbody2D>());
            }
        }
    }

    protected override bool StateChangeEvent(bool _Activity)
    {
        if(Observer)
        {
            if (!_Activity)
                JointObject(null);
            return Observer.ChangeState(_Activity);
        }
        if(StartPointer)
        SetImage(_Activity);
        return _Activity;
    }


    protected virtual Rigidbody2D JointObject(Rigidbody2D _RigidbodyObject)
    {
        Debug.Log("조인트설정");
        if(_RigidbodyObject)
        {
            Debug.Log("연결");
            CatchObject = _RigidbodyObject;
            m_Joint.enabled = true;
            ResetCatch(ResetMode_Rigidbody);
            if (CatchPosition)
            {
                //_RigidbodyObject.transform.rotation = Quaternion.identity;
                _RigidbodyObject.transform.position = CatchPosition.position;
            }

            m_Joint.connectedBody = _RigidbodyObject;
        }
        else
        {
            m_Joint.enabled = false;
            m_Joint.connectedBody = null;
            CatchObject = null;
        }
        return _RigidbodyObject;
    }


    private void ResetCatch(bool _set)
    {
        if(CatchObject)
        {
            CatchObject.velocity = Vector2.zero;
            CatchObject.angularVelocity = 0;
        }

    }

    void SetImage(bool _Activity)
    {
        DisableImage.SetActive(!_Activity);
        EnableImage.SetActive(_Activity);
    }

}