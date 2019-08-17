using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMoving : MonoBehaviour
{
    private Vector2 LastPos;
    public Vector2 DeltaPos;
    public GameObject Button;
    public List<GameObject> TriggerLis = new List<GameObject>();
   public bool IsTouch;
    private Vector2 OriPos;
    public float Velocity;
    public bool ForTest=true;
  
    // Start is called before the first frame update
    void Start()
    {
        OriPos = transform.position;
        LastPos = OriPos + DeltaPos;
    }

    // Update is called once per frame
    void Update()
    {
       if(ForTest) ButtonDown();
        if(IsTouch)Move(LastPos);
        else if(IsTouch==false) Move(OriPos);
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
    private void ButtonDown()
    { int count = 0;
        Collider2D col = Button.GetComponent<Collider2D>();
        foreach(var element in TriggerLis)
        {
            if (col.IsTouching(element.GetComponent<Collider2D>())){
                IsTouch = true; Debug.Log(count);
            }
            else count += 1;
        }
        if (count == TriggerLis.Count) IsTouch = false;
    }
}
