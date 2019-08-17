using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadShooter : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject body;
    Vector2 bodyPos;
    public float autoShootForce;
    public float chargeSpeed;

    Vector3 mousePos;
    public Camera cam;
    public Slider powerSlider;
    public float minForce;
    public float maxForce;
    float shootForce;

    public float canGetHeadDistance;
    
    Vector3 basicScale;

    enum State
    {
        Idle, Ready, Division
    }

    State state;

    void Start()
    {
        state = State.Idle;
        rb = gameObject.GetComponent<Rigidbody2D>();
        shootForce = minForce;
        powerSlider.gameObject.SetActive(false);
        basicScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (state == State.Idle)
            {
                HeadShootingReady();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (state == State.Ready)
            {
                HeadShooting();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(state == State.Idle)
            {
                AutoHeadShootingForward();
            }
            else if(state == State.Division)
            {
                if(Vector2.Distance(body.transform.position, transform.position) < canGetHeadDistance)
                {
                    GetHead();
                }
            }
        }

        if (state == State.Ready)
        {
            shootForce += Time.deltaTime * chargeSpeed;
            powerSlider.value = shootForce;
        }

        SetPosition();
    }

    void SetPosition()
    {
        if (state == State.Idle)
        {
            transform.localPosition = new Vector2(0, 0.8f);
        }
        if (state == State.Ready)
        {
            bodyPos = body.transform.position;
            transform.position = bodyPos + new Vector2(0, 1.6f);
        }
    }

    void HeadShootingReady()
    {
        state = State.Ready;
        transform.parent = null;
        powerSlider.gameObject.SetActive(true);
        powerSlider.value = minForce;
    }

    void HeadShooting()
    {
        state = State.Division;
        powerSlider.gameObject.SetActive(false);
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootRotate = mousePos - transform.position;
        shootRotate.Normalize();

        rb.velocity = shootRotate * shootForce;
        rb.gravityScale = 1.0f;
    }

    void AutoHeadShootingForward()
    {
        state = State.Division;
        rb.freezeRotation = false;
        transform.parent = null;
        rb.velocity = autoShootForce * Vector2.right;
        rb.gravityScale = 1.0f;
    }

    void GetHead()
    {
        transform.parent = body.transform;
        transform.localScale = basicScale;
        transform.rotation = Quaternion.identity;
        state = State.Idle;
        rb.freezeRotation = true;
    }
}
