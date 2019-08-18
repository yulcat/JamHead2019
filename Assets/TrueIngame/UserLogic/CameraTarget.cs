using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraTarget : MonoBehaviour
{
    private GameObject target;
    [SerializeField] GameObject MenuSelection;

    void Start()
    {
        var withHead = GameObject.FindWithTag("GetHead");
        target = withHead ? withHead : GameObject.FindWithTag("Head");
        MenuSelection.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10f);
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            MenuSelection.SetActive(true);
        }
    }
}