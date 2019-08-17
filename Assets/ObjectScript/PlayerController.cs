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
    [SerializeField] private Transform JointPosition;
    [SerializeField] private CharacterAnimation CharacterAnimation;
    private ObjectPrefab_CannonHead cannon;
    [SerializeField] private Transform rayDirection;

    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float jumpForce = 10;

    [Header("※ 캐릭터 상태")] [ReadOnly, SerializeField]
    private bool isJumping;

    [Header("※ 게임 조작 정보")] [SerializeField]
    private KeyCode Key_Jump = KeyCode.Space;

    [SerializeField] private KeyCode Key_Shot = KeyCode.LeftControl;

    public bool canControl;
    //public bool isShootReady;

    void Start()
    {
        isJumping = false;
        canControl = true;
        //isShootReady = false;
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Joint = JointPosition.GetComponent<FixedJoint2D>();

        cannon = JointPosition.GetComponent<ObjectPrefab_CannonHead>();
        CharacterAnimation.OnThrow = ThrowHead;
        CharacterAnimation.OnJump = Jump;
    }

    void Update()
    {
        if (!canControl)
        {
            return;
        }

        float inputX = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(inputX) > 0.5f)
        {
            var scaleX = -Mathf.Sign(inputX);
            transform.localScale = new Vector3(scaleX, 1, 1);
        }

        if (CharacterAnimation.TryMove(inputX))
        {
            m_Rigid.velocity = new Vector2(inputX * moveSpeed, m_Rigid.velocity.y);
        }
        else
        {
            m_Rigid.velocity = new Vector2(0, m_Rigid.velocity.y);
        }

        if (!isJumping)
        {
            if (Input.GetKeyDown(Key_Jump))
            {
                CharacterAnimation.TryJump();
            }
        }


        if (Input.GetKeyDown(Key_Shot))
        {
            OnFireInput();
        }


        MouseEvent();
    }

    private void Jump()
    {
        m_Rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
        CharacterAnimation.SetGround(false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Land")
        {
            if (m_Rigid.velocity.y < 0.001f)
            {
                isJumping = false;
                CharacterAnimation.SetGround(true);
            }
        }
    }


    void JointObject(Rigidbody2D _RigidbodyObject)
    {
        _RigidbodyObject.transform.SetParent(JointPosition);
        _RigidbodyObject.transform.localRotation = Quaternion.identity;
        _RigidbodyObject.transform.localPosition = Vector3.zero;
        _RigidbodyObject.transform.localScale = Vector3.one;
        _RigidbodyObject.simulated = false;
        m_CurrentHead = _RigidbodyObject;
    }


    void MouseEvent()
    {
    }

    void OnFireInput()
    {
        Debug.Log("공격누름");
        if (m_CurrentHead != null)
        {
            CharacterAnimation.TryThrow();
        }
        else if (TryPickupHead())
        {
            CharacterAnimation.PickUp();
        }
    }

    private bool TryPickupHead()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayDirection.transform.position, Vector2.down, 10);
        if (!hit) return false;
        if (!hit.transform.CompareTag("Head")) return false;
        JointObject(hit.collider.gameObject.GetComponent<Rigidbody2D>());
        hit.collider.gameObject.tag = "GetHead";
        return true;
    }

    private void ThrowHead()
    {
//        m_Joint.connectedBody = null;
//        m_Joint.enabled = false;
        m_CurrentHead.transform.SetParent(null);
        m_CurrentHead.gameObject.tag = "Head";
        m_CurrentHead.simulated = true;
        cannon.HeadBulletCharge(m_CurrentHead);
        m_CurrentHead = null;
        cannon.SetState(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            canControl = false;
            OnFireInput();
        }
    }
}