using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public int SceneIndex;
    private void Start()
    {
        transform.GetChild(0).GetComponent<Text>().text = SceneIndex.ToString();
    }

    public void SceneLoad()
    {if (SceneManager.GetSceneAt(SceneIndex) == null) return;
        SceneManager.LoadScene(SceneIndex+1);

    }
}
