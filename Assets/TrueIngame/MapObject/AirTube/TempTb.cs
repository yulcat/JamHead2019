using System.Collections.Generic;
using UnityEngine;

public class TempTb : MonoBehaviour
{
    Transform startPoint;
    Transform endPoint;
    public float autoMoveSpeed;

    Color originalHeadColor;
    CircleCollider2D circleCollider;
    private List<Rigidbody2D> bodiesOnRail = new List<Rigidbody2D>();

    void Start()
    {
        startPoint = transform.GetChild(0);
        endPoint = transform.GetChild(1);
    }


    void FixedUpdate()
    {
        foreach (var body in bodiesOnRail)
        {
            UpdateAutoMove(body);
        }
    }

    void UpdateAutoMove(Rigidbody2D _rb)
    {
        Vector2 direction = endPoint.position - _rb.transform.position;
        direction.Normalize();
        _rb.velocity = autoMoveSpeed * direction;
    }

    public void AutoMove(Rigidbody2D _rb)
    {
        if (bodiesOnRail.Contains(_rb)) return;
        if (!_rb.gameObject.CompareTag("Head"))
        {
            AutoMoveEnd(_rb);
            return;
        }

        _rb.gravityScale = 0f;
        _rb.gameObject.transform.position = startPoint.position;
        circleCollider = _rb.gameObject.GetComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;

        SpriteRenderer _sr = _rb.gameObject.GetComponent<SpriteRenderer>();
        originalHeadColor = _sr.color;
        _sr.color = new Color(0, 0, 0, 0.5f);
        bodiesOnRail.Add(_rb);
    }

    public void AutoMoveEnd(Rigidbody2D _rb)
    {
        bodiesOnRail.Remove(_rb);
        _rb.gravityScale = 1f;
        _rb.velocity /= 3f;

        SpriteRenderer _sr = _rb.gameObject.GetComponent<SpriteRenderer>();
        _sr.color = originalHeadColor;
        circleCollider.isTrigger = false;
    }
}