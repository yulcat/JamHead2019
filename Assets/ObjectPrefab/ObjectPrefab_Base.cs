using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPrefab_Base : MonoBehaviour
{
    [Header("※ 게임 시작시 정보")]
    public bool StartState = false;
    [Header("※ 작용이 발생할 하위 객체들")]
    public List<ObjectPrefab_Base> LinkObjects;
    [Header("※ 지금의 작동 상태")]
    public bool CurrentState = false;

    protected virtual void Start()
    {
        SetState(StartState);
    }

    public void SetState(bool _Activity)
    {
        CurrentState = StateChangeEvent(_Activity);
        foreach (var iter in LinkObjects)
            iter.SetState(CurrentState);
    }

    protected abstract bool StateChangeEvent(bool _Activity);


}
