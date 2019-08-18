using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

#pragma warning disable 0649

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Joint2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_DefaultHead;
    [SerializeField] private Rigidbody2D m_CurrentHead;

    private Rigidbody2D m_Rigid;
    private BoxCollider2D m_Collider;
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

    RaycastHit2D[] raycastHits = new RaycastHit2D[10];
    //public bool isShootReady;

    [SerializeField]GameObject SoundW;
    [SerializeField] GameObject SoundT;
    [SerializeField] GameObject SoundD;


    void Start()
    {
        isJumping = false;
        canControl = true;
        //isShootReady = false;
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<BoxCollider2D>();
        m_Joint = JointPosition.GetComponent<FixedJoint2D>();

        cannon = JointPosition.GetComponent<ObjectPrefab_CannonHead>();
        CharacterAnimation.OnThrow = ThrowHead;
        CharacterAnimation.OnJump = Jump;

        sideContactFilter = new ContactFilter2D
        {
            layerMask = LayerMask.GetMask("Default", "Platform", "Interactable"),
            useLayerMask = true,
            useTriggers = false,
        };
        bottomContactFilter = new ContactFilter2D
        {
            layerMask = LayerMask.GetMask("Default", "Platform", "Interactable", "OneWayPlatform"),
            useLayerMask = true,
            useTriggers = false,
        };
    }

    protected Vector2 CurrentAxis = Vector2.zero;

    void Update()
    {
        CurrentAxis.x = Input.GetAxisRaw("Horizontal");
        CurrentAxis.y = Input.GetAxisRaw("Vertical");

        if (!canControl)
        {
            return;
        }


        if (Mathf.Abs(CurrentAxis.x) > 0.5f)
        {
            SoundW.SetActive(true);
            var scaleX = -Mathf.Sign(CurrentAxis.x);
            transform.localScale = new Vector3(scaleX, 1, 1);
        }
        else
            SoundW.SetActive(false);

        if (CharacterAnimation.TryMove(CurrentAxis.x))
        {
            TryMoveRigidbody();
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

    private void FixedUpdate() => CheckFall();

    private void TryMoveRigidbody()
    {
        var vector = CurrentAxis.x * Time.fixedDeltaTime * moveSpeed * Vector2.right;
        var hits = m_Collider.Cast(vector, sideContactFilter, raycastHits, vector.magnitude);
        if (hits == 0)
            m_Rigid.velocity = new Vector2(CurrentAxis.x * moveSpeed, m_Rigid.velocity.y);
    }

    private void CheckFall()
    {
        if (m_Rigid.velocity.y > -1f || m_Collider.Cast(Vector2.down, sideContactFilter, raycastHits, 0.1f) > 0) return;
        isJumping = true;
        CharacterAnimation.SetGround(false);
    }


    private void Jump()
    {
        m_Rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
        CharacterAnimation.SetGround(false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts.All(c => c.normal.y < 0.5f)) return;
        if (m_Rigid.velocity.y < 0.001f
            && m_Collider.Cast(Vector2.down, bottomContactFilter, raycastHits, 0.1f) > 0)
        {
            isJumping = false;
            CharacterAnimation.SetGround(true);
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
            SoundT.SetActive(false);
            SoundT.SetActive(true);
            CharacterAnimation.TryThrow();
        }
        else if (TryPickupHead())
        {
            CharacterAnimation.PickUp();
        }
    }

    string TempTagSaver = "Head";
    private bool TryPickupHead()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayDirection.transform.position, Vector2.down, 10);
        if (!hit) return false;
        if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Head"))
            return false;
        JointObject(hit.collider.gameObject.GetComponent<Rigidbody2D>());
        TempTagSaver = hit.collider.gameObject.tag;
        hit.collider.gameObject.tag = "GetHead";
        m_Collider.offset = Vector2.up * 1.25f;
        m_Collider.size = new Vector2(1, 2.5f);
        return true;
    }

    Vector2 ThrowDir_Up = Vector2.up;
    Vector2 ThrowDir_Normal = new Vector2(-1, 1);
    Vector2 ThrowDir_Forward = Vector2.left;
    Vector2 ThrowDir_Down = Vector2.down;
    private ContactFilter2D sideContactFilter;
    private ContactFilter2D bottomContactFilter;

    private void ThrowHead()
    {
        //        m_Joint.connectedBody = null;
        //        m_Joint.enabled = false;
        //cannon
        // CurrentAxis
        m_Collider.offset = Vector2.up * 0.6f;
        m_Collider.size = new Vector2(1, 1.2f);

        if (CurrentAxis.y > 0.2f)
        {
            cannon.direction = ThrowDir_Up;
        }
        else if (CurrentAxis.y < -0.2f)
        {
            cannon.direction = ThrowDir_Down;

            m_CurrentHead.transform.SetParent(null);
            m_CurrentHead.gameObject.tag = TempTagSaver;
            m_CurrentHead.simulated = true;
            m_CurrentHead = null;
            return;
        }
        else
        {
            if (Mathf.Abs(CurrentAxis.x) > 0.2f)
            {
                cannon.direction = ThrowDir_Forward;
            }
            else
            {
                cannon.direction = ThrowDir_Normal;
            }
        }


        m_CurrentHead.transform.SetParent(null);
        m_CurrentHead.gameObject.tag = TempTagSaver;
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
            SoundD.SetActive(true);
            OnFireInput();
        }
    }
}