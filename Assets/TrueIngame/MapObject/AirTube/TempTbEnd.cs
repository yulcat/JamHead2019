using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTbEnd : MonoBehaviour
{
    TempTb tempTb;
    void Start()
    {
        tempTb = transform.parent.gameObject.GetComponent<TempTb>();
    }
    
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Head")
        {
            tempTb.AutoMoveEnd(collision.gameObject.GetComponent<Rigidbody2D>());
        }
    }
}
