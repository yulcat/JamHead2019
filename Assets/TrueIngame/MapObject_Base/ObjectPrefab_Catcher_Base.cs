using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPrefab_Catcher_Base : ObjectPrefab_Base
{
    protected Rigidbody2D m_Rigid;
    protected Joint2D m_Joint;
    protected ObjectPrefab_Observer_Base Observer;

    [Header("장치 설정")]
    [SerializeField] protected bool ResetMode_Rigidbody = true;
    [SerializeField] protected Transform CatchPosition;
    [Header("붙잡은 오브젝트")]
    [SerializeField] protected Rigidbody2D CatchObject;

    protected override void Start()
    {
        base.Start();
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Joint = GetComponent<Joint2D>();
        Observer = transform.GetChild(0).GetComponent<ObjectPrefab_Observer_Base>();
        if (Observer)
            Observer.ChangeState(CurrentState);

    }

    protected virtual void Update()
    {
        if (Observer && Observer.enabled)
        {
            if (Observer.Observing())
            {
                Debug.Log("옵저빙판단성공");
                JointObject(Observer.Observing().GetComponent<Rigidbody2D>());
            }
        }
    }

    protected override bool StateChangeEvent(bool _Activity)
    {
        if (Observer)
        {
            if (!_Activity)
                JointObject(null);
            return Observer.ChangeState(_Activity);
        }
        return _Activity;
    }


    protected virtual Rigidbody2D JointObject(Rigidbody2D _RigidbodyObject)
    {
        Debug.Log("조인트설정");
        if (_RigidbodyObject)
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
        if (CatchObject)
        {
            CatchObject.velocity = Vector2.zero;
            CatchObject.angularVelocity = 0;
        }

    }


}
