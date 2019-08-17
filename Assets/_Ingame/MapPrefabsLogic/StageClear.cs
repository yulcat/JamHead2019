using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClear : ObjectPrefab_Action_Base
{ private bool IsClear;
    public string WinTarget="Head";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsClear) { Debug.Log("You Clear");
            if(SceneManager.GetActiveScene().buildIndex<SceneManager.sceneCountInBuildSettings)SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == WinTarget) IsClear = true;
    }


}
