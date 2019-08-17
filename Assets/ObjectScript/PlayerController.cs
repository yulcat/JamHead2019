using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_DefaultHead;

    [SerializeField] private Rigidbody2D m_CurrentHead;


    private Rigidbody2D m_Rigid;
    private FixedJoint2D m_Joint;
    private Transform JointPosition;
    private ObjectPrefab_Cannon cannon;
    private Transform rayDirection;

    [SerializeField] private float moveSpeed = 8 ;
    [SerializeField] private float jumpForce = 10;

    [Header("※ 캐릭터 상태")]
    [ReadOnly,SerializeField] private bool isJumping;
    [Header("※ 게임 조작 정보")]
    [SerializeField] private KeyCode Key_Jump = KeyCode.Space;
    [SerializeField] private KeyCode Key_Shot = KeyCode.LeftControl;


    void Start()
    {
        isJumping = false;
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Joint = GetComponent<FixedJoint2D>();

        JointPosition = transform.GetChild(0);
        cannon = JointPosition.GetComponent<ObjectPrefab_Cannon>();
        rayDirection = transform.GetChild(1);

    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        m_Rigid.velocity = new Vector2(inputX * moveSpeed, m_Rigid.velocity.y);
        if (!isJumping)
        {
            if (Input.GetKeyDown(Key_Jump))
            {
                m_Rigid.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
                isJumping = true;
            }
        }


        if ( Input.GetKeyDown(Key_Shot) )
        {
            Debug.Log("공격누름");
            if (m_Joint.connectedBody)
            {
                m_Joint.connectedBody = null;
                m_Joint.enabled = false;
                cannon.HeadShooting(m_CurrentHead);
                m_CurrentHead = null;
                cannon.SetState(true);
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(rayDirection.transform.position, Vector2.down ,10);
                Debug.Log("레이 발사");
                if (hit)
                {
                    Debug.Log("찾음");
                    Debug.Log(rayDirection.position - transform.position);
                    if (hit.transform.CompareTag("Head"))
                    {
                        JointObject(hit.collider.gameObject.GetComponent<Rigidbody2D>());
                    }
                }
            }
        }

        MouseEvent();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Land")
        {
            if (m_Rigid.velocity.y == 0f)
            {
                isJumping = false;
            }
        }
    }


    void JointObject(Rigidbody2D _RigidbodyObject)
    {
        m_Joint.enabled = true;
        _RigidbodyObject.transform.rotation = Quaternion.identity;
        _RigidbodyObject.transform.position = JointPosition.position;
        m_Joint.connectedBody = _RigidbodyObject;
        m_CurrentHead = _RigidbodyObject;
    }


    void MouseEvent()
    {


    }
}