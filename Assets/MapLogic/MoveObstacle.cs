using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    public List<GameObject> OnLis = new List<GameObject>();
    // Start is called before the first frame update
    public float Velocity;
    public Vector2 LastPos;
   
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move(LastPos);
    }
    private void Move(Vector2 delta_pos)
    { Vector2 temp = new Vector2(delta_pos.x - transform.position.x, delta_pos.y - transform.position.y);
        temp.Normalize();
        if (Vector2.Distance(transform.position, delta_pos) > 0.05f)
        {
            transform.Translate(temp*Velocity*Time.deltaTime);
        }
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
