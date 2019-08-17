using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoMoveObstacle : ObjectPrefab_Action_Base
{ public List<GameObject> OnLis = new List<GameObject>();
    public Vector2 DeltaPos;

    // Start is called before the first frame update
    public float Velocity;
    private Vector2 LastPos;
    private Vector2 OriPos;
    public bool IsArrive;


    void Start()
    {
        LastPos = new Vector2(DeltaPos.x + transform.position.x, DeltaPos.y + transform.position.y);
        OriPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move(LastPos);
        MoveBack(OriPos);
    }
    private void Move(Vector2 delta_pos)
    {   if (IsArrive) return;
        Vector2 temp = new Vector2(delta_pos.x - transform.position.x, delta_pos.y - transform.position.y);
        Vector2 temp_reverse = new Vector2(-1 * temp.x, -1 * temp.y);
        temp.Normalize();
        if (Vector2.Distance(transform.position, delta_pos) > 0.05f)
        {
            transform.Translate(temp * Velocity * Time.deltaTime);


        }
        else IsArrive = true;


    }
    private void MoveBack(Vector2 delta_pos)
    {   if (IsArrive==false) return;

        Vector2 temp = new Vector2(delta_pos.x - transform.position.x, delta_pos.y - transform.position.y);
        Vector2 temp_reverse = new Vector2(-1 * temp.x, -1 * temp.y);
        temp.Normalize();
        if (Vector2.Distance(transform.position, delta_pos) > 0.05f)
        {
            transform.Translate(temp * Velocity * Time.deltaTime);

        }
        else    IsArrive = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnLis.Add(collision.gameObject);
        collision.gameObject.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        OnLis.Remove(collision.gameObject);
        collision.gameObject.transform.SetParent(null);
    }
}
