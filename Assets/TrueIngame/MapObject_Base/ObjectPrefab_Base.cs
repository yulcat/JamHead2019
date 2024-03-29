﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPrefab_Base : MonoBehaviour
{
    [Header("※ 게임시작과 / 기본설정")]
    public bool StartState = false;
    public bool ReversePower = false;

    [Header("※ 작용이 발생할 하위 객체들")]
    public List<ObjectPrefab_Base> LinkObjects;
    [Header("※ 지금의 작동 상태[수정해도 효과x]")]
    public bool CurrentState = false;

    protected virtual void Start()
    {
        SetState(StartState);
    }

    /// <summary>
    /// 일단 호출을 한다면 체인지 이벤트를 호출하나, 기존과 상태가 같다면, 하위 개체들에게 명령을 보내지 않는다.
    /// </summary>
    public void SetState(bool _Activity)
    {
        bool nextState = StateChangeEvent(_Activity);
        if(nextState != CurrentState)
        {
            CurrentState = nextState;
            if(ReversePower)
                Debug.Log(gameObject + "역변환됨" + !CurrentState);
            else
            Debug.Log(gameObject+"변환됨" + CurrentState);

            foreach (var iter in LinkObjects)
                iter.SetState(ReversePower?!CurrentState : CurrentState);
        }
    }

    /// <summary>
    /// 바뀔 값을 판정하는 것으로, 바뀌는 것은 원래 상태와 다를때 SetState()에서 처리됨
    /// </summary>
    protected abstract bool StateChangeEvent(bool _Activity);

    protected virtual void ReturnObject(ref ObjectPrefab_Base _object)
    {
    }

}
