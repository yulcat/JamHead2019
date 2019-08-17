using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : ObjectPrefab_Action_Base
{
    public bool IsEnter;
    public List<Vector2> MovePath;
    private int MoveIndex;
    public float Velocity;
    public GameObject Target;
    public Vector2 OriPos;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        Moving();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Head")
        {
            IsEnter = true; Target = collision.gameObject; OriPos = Target.transform.position;
        }
    }
    private void Moving()
    {  if (IsEnter != true) return;
        Vector2 CurPos = Target.transform.position;
        if (MoveIndex == MovePath.Count) return;
        if (MoveIndex < MovePath.Count)
        { float step = Velocity * Time.deltaTime;
            Target.transform.position = Vector2.MoveTowards(CurPos, OriPos + MovePath[MoveIndex], step);
            
            
        }
        if (Vector2.Distance(Target.transform.position, OriPos + MovePath[MoveIndex]) == 0) MoveIndex++;
    }
}
