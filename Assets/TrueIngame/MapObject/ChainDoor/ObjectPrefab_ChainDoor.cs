using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPrefab_ChainDoor : ObjectPrefab_Base
{
    [Header("스텟 설정")]
    [SerializeField] protected float ChainEndTimer = 1;


    [Header("자체 정보들")]
    [SerializeField] protected float CurrentTime = 0;
    [SerializeField,ReadOnly] protected bool StartChainState = true;


    [SerializeField] protected Transform StartPointer;
    [SerializeField] protected Transform EndPointer;
    [SerializeField] protected Transform ColliderSizer;

    [SerializeField] protected LineRenderer LineRender;
    [SerializeField] protected Vector3 ChainEndPoint = Vector3.zero;

    private Material m_Material;
    private int m_ShaderVariableId;

    Vector2 OffsetValue = Vector2.zero;
    WaitForSeconds m_WaitCoroutine = new WaitForSeconds(0.1f);

    protected override void Start()
    {
        base.Start();
        EndPointer = transform.GetChild(2);
        StartPointer = transform.GetChild(1);
        ColliderSizer = StartPointer.GetChild(0);
        LineRender = transform.GetChild(0).GetComponent<LineRenderer>();

        ColliderSizer.rotation = LookAt2d(StartPointer.localPosition, EndPointer.localPosition);


        m_Material = LineRender.transform.GetComponent<Renderer>().material;
        m_ShaderVariableId = Shader.PropertyToID("_MainTex");//"Particle Texture"

        StartCoroutine(UpdateColliderSizer());
        if(StartState)
            CurrentTime = 0;
        else
            CurrentTime = ChainEndTimer;

    }

    private void Update()
    {
        if(StartChainState)
        {
            if (CurrentState&(!ReversePower))
            {
                CurrentTime -= Time.deltaTime;
                if(CurrentTime <= 0)
                {
                    CurrentTime = 0;
                    StartChainState = true;
                    ChainEndPoint = StartPointer.localPosition;
                    SetChainRangeData();
                    updateSize();
                }
                else
                    SetChainRange();
            }
            else
            {
                CurrentTime += Time.deltaTime;
                if (CurrentTime >= ChainEndTimer)
                {
                    CurrentTime = ChainEndTimer;
                    StartChainState = true;
                    ChainEndPoint = EndPointer.localPosition;
                    SetChainRangeData();
                    updateSize();
                }
                else
                    SetChainRange();
            }
        }

    }

    private void SetChainRange()
    {
        ChainEndPoint = Vector3.Lerp(StartPointer.localPosition, EndPointer.localPosition, CurrentTime / ChainEndTimer);
        SetChainRangeData();
    }
    Vector3 tempVector3 = Vector3.one;
    private void SetChainRangeData()
    {
        OffsetValue.x = ChainEndPoint.magnitude;
        m_Material.SetTextureOffset(m_ShaderVariableId, -OffsetValue);
        tempVector3.y = Mathf.Max(0.01f, OffsetValue.x);
        LineRender.SetPosition(1,ChainEndPoint);
    }

    protected override bool StateChangeEvent(bool _Activity)
    {
        if (CurrentState != _Activity)
            StartChainState = true;
        return _Activity;
    }

    IEnumerator UpdateColliderSizer()
    {
        while(true)
        {
            if(StartChainState)
                updateSize();
            yield return m_WaitCoroutine;
        }

    }
    void updateSize()
    {
        ColliderSizer.localScale = tempVector3;
    }


    Quaternion LookAt2d(Vector3 src, Vector3 desc)
    {
        desc.x = desc.x - src.x;
        desc.y = desc.y - src.y;
        float angle = Mathf.Atan2(desc.y, desc.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(new Vector3(0, 0, angle-90));
    }


}
