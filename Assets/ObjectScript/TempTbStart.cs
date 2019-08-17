using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTbStart : MonoBehaviour
{
    TempTb tempTb;
    void Start()
    {
        tempTb = transform.parent.gameObject.GetComponent<TempTb>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Head")
        {
            tempTb.AutoMove(collision.gameObject.GetComponent<Rigidbody2D>());
        }
    }
}
