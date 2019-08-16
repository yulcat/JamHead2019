using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMoving : MonoBehaviour
{
    public Vector2 Laspos
        ;
    private Vector2 OriPos;
    public float Velocity;
    public GetTouch ButtonTouch; // 입력받을 버튼의 스크립트입니다
    // Start is called before the first frame update
    void Start()
    {
        OriPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(ButtonTouch.IsTouch)Move(Laspos);
        else if(ButtonTouch.IsTouch==false) Move(OriPos);
    }
    private void Move(Vector2 delta_pos)
    {
        Vector2 temp = new Vector2(delta_pos.x - transform.position.x, delta_pos.y - transform.position.y);
        temp.Normalize();
        if (Vector2.Distance(transform.position, delta_pos) > 0.05f)
        {
            transform.Translate(temp * Velocity * Time.deltaTime);
        }
    }
}
