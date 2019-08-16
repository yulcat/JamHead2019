using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_DefaultHead;

    private Rigidbody2D m_Rigid;
    private FixedJoint2D m_Joint;
    private Transform JointPosition;
    [SerializeField] private float moveSpeed = 8 ;
    [SerializeField] private float jumpForce = 10;

    [Header("※ 캐릭터 상태")]
    [ReadOnly,SerializeField] private bool isJumping;
    [Header("※ 게임 조작 정보")]
    [SerializeField] private KeyCode Key_Jump = KeyCode.Space;
    [SerializeField] private KeyCode Key_Shot = KeyCode.LeftControl;


    float shootForce;



    void Start()
    {
        isJumping = false;
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Joint = GetComponent<FixedJoint2D>();

        JointPosition = transform.GetChild(0);

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

        if (Input.GetKeyDown(KeyCode.Return))
        {
           if(m_Joint.connectedBody)
            {
                m_Joint.connectedBody = null;
                m_Joint.enabled = false;
            }
            else
                JointObject(m_DefaultHead);
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

        Debug.Log("머리 획득");
    }


    void MouseEvent()
    {


    }
}