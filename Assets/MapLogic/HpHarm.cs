using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpHarm : MonoBehaviour
{
    private bool IsHarm;
    public string HarmTarget="Player";
    private GameObject Character;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Harm();
    }
    private void Harm()
    {
        if (IsHarm == false) return;
        /*Character.GetComponent<PlayerMove>().Hp -= 1;*/
        IsHarm = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag ==HarmTarget) IsHarm = true;
    }
}
