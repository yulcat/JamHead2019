using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpPower;
    bool isJumping;
    public bool canMove;
    GameObject head;
    public GameObject targetPoint;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        isJumping = false;
        canMove = true;
        head = transform.Find("Head").gameObject;
    }
    
    void Update()
    {
        if (!canMove)
        {
            //return;
        }

        float inputX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(inputX * speed, 0f);

        if (Input.GetKey(KeyCode.Space))
        {
            if (!isJumping)
            {
                isJumping = true;
                rb.AddForce(new Vector2(0, jumpPower));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Land")
        {
            isJumping = false;
        }
    }
}
