using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShooter : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject body;
    Vector2 bodyPos;
    public PlayerMove playerMove;
    public float speed;
    public float shootPower;

    Vector3 mousePos;
    public Camera cam;

    enum State
    {
        Idle, Ready, Division
    }

    State state;

    void Start()
    {
        state = State.Idle;
        rb = gameObject.GetComponent<Rigidbody2D>();
        shootPower = 0f;
    }

    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            if(state == State.Idle)
            {
                HeadShootingReady();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(state == State.Ready)
            {

                HeadShooting();
            }
        }

        if(state == State.Ready)
        {
            shootPower += Time.deltaTime;
            Debug.Log(shootPower);
        }

        //SetPosition();
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
            transform.position = bodyPos + new Vector2(0, 1.7f);
        }
    }

    void HeadShootingReady()
    {
        state = State.Ready;
        //playerMove.canMove = false;
        transform.parent = null;
        
    }

    void HeadShooting()
    {
        state = State.Division;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //playerMove.canMove = true;
        Vector2 shootRotate = mousePos - transform.position;
        shootRotate.Normalize();

        rb.velocity = shootRotate * shootPower;
        Debug.Log(shootRotate * shootPower);
        rb.gravityScale = 1.0f;
    }

    void GetHead()
    {

    }
}
