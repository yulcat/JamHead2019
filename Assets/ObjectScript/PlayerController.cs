using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Joint2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_DefaultHead;
    [SerializeField] private Rigidbody2D m_CurrentHead;

    private Rigidbody2D m_Rigid;
    private FixedJoint2D m_Joint;
    private Transform JointPosition;
    private ObjectPrefab_CannonHead cannon;
    private Transform rayDirection;

    [SerializeField] private float moveSpeed = 8 ;
    [SerializeField] private float jumpForce = 10;

    [Header("※ 캐릭터 상태")]
    [ReadOnly,SerializeField] private bool isJumping;
    [Header("※ 게임 조작 정보")]
    [SerializeField] private KeyCode Key_Jump = KeyCode.Space;
    [SerializeField] private KeyCode Key_Shot = KeyCode.LeftControl;

    public bool canControl;
    //public bool isShootReady;

    void Start()
    {
        isJumping = false;
        canControl = true;
        //isShootReady = false;
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Joint = GetComponent<FixedJoint2D>();

        JointPosition = transform.GetChild(0);
        cannon = JointPosition.GetComponent<ObjectPrefab_CannonHead>();
        rayDirection = transform.GetChild(1);

    }

    void Update()
    {
        if (!canControl)
        {
            return;
        }

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


        if (Input.GetKeyDown(Key_Shot))
            //StartCoroutine(HeadShoot());
            HeadShoot();


        MouseEvent();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Land")
        {
            if (m_Rigid.velocity.y <0.001f)
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

    //IEnumerator HeadShoot()
    void HeadShoot()
    {
        Debug.Log("공격누름");
        if (m_Joint.connectedBody)
        {
            //isShootReady = true;
            //yield return new WaitForSeconds(0.3f);

            m_Joint.connectedBody.gameObject.tag = "Head";
            m_Joint.connectedBody = null;
            m_Joint.enabled = false;
            cannon.HeadBulletCharge(m_CurrentHead);
            m_CurrentHead = null;
            cannon.SetState(true);
            //isShootReady = false;
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(rayDirection.transform.position, Vector2.down, 10);
            if (hit)
            {
                //Debug.Log(rayDirection.position - transform.position);
                if (hit.transform.CompareTag("Head"))
                {
                    JointObject(hit.collider.gameObject.GetComponent<Rigidbody2D>());
                    hit.collider.gameObject.tag = "GetHead";
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            canControl = false;
            HeadShoot();
        }
    }

}