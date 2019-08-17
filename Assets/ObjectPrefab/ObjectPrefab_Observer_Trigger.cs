using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ObjectPrefab_Observer_Trigger : ObjectPrefab_Observer_Base
{
    private void OnEnable()
    {
            LastFindObject = null;
    }
    private void OnDisable()
    {
            LastFindObject = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!LastFindObject)
        {
            if ((collision.gameObject.layer | FindLayerMask) != 0)
            {
                LastFindObject = collision.gameObject;
                Debug.Log("trigger성공" + LastFindObject);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!LastFindObject)
        {
            if ((collision.gameObject.layer | FindLayerMask) != 0)
            {
                LastFindObject = collision.gameObject;
                Debug.Log("trigger성공" + LastFindObject);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LastFindObject)
        {
            if (collision.GetHashCode() == LastFindObject.gameObject.GetHashCode())
            {
                Debug.Log("나감");
                LastFindObject = null;
            }
        }


    }
}
