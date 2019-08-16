using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{ private bool IsClear;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsClear) Debug.Log("Hello");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Head") IsClear = true;
    }
}
