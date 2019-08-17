using UnityEngine;

[ExecuteAlways]
public class BackgroundQuad : MonoBehaviour
{
    public Mesh Quad;
    public Material Material;

    private void OnEnable() => Camera.onPreCull += DrawBackgroundQuad;
    private void OnDisable() => Camera.onPreCull -= DrawBackgroundQuad;

    private void DrawBackgroundQuad(Camera cam)
    {
        if (!Quad || !Material) return;
        var position = cam.transform.TransformPoint(Vector3.forward);
        Graphics.DrawMesh(Quad, position, Quaternion.identity, Material, 0, cam);
    }
}