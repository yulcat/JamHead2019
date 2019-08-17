using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private GameObject target;

    void Start()
    {
        var withHead = GameObject.FindWithTag("GetHead");
        target = withHead ? withHead : GameObject.FindWithTag("Head");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10f);
    }
}