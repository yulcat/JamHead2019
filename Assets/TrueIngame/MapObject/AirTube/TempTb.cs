using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTb : MonoBehaviour
{
    Transform startPoint;
    Transform endPoint;
    public float autoMoveSpeed;

    Color originalHeadColor;
    CircleCollider2D circleCollider;
    private GameObject Use;

    void Start()
    {
        startPoint = transform.GetChild(0);
        endPoint = transform.GetChild(1);
    }

    
    void Update()
    {
        
    }

    public void AutoMove(Rigidbody2D _rb)
    {  
        _rb.gameObject.transform.position = startPoint.position;
        Vector2 direction = endPoint.position - startPoint.position;
        direction.Normalize();
        _rb.velocity = autoMoveSpeed * direction;
        _rb.gravityScale = 0f;

        circleCollider = _rb.gameObject.GetComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;

        SpriteRenderer _sr = _rb.gameObject.GetComponent<SpriteRenderer>();
        originalHeadColor = _sr.color;
        _sr.color = new Color(0, 0, 0, 0.5f);
    }

    public void AutoMoveEnd(Rigidbody2D _rb)
    {
        _rb.gravityScale = 1f;
        _rb.velocity /= 3f;

        SpriteRenderer _sr = _rb.gameObject.GetComponent<SpriteRenderer>();
        _sr.color = originalHeadColor;
        circleCollider.isTrigger = false;
    }
}
