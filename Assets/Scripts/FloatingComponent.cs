using UnityEngine;

public abstract class FloatingComponent : MonoBehaviour
{
    public static float MAXRANGE = 10000.0f;

    public double x, y, z;
    public bool isOOB = false;

    protected float translationScale = 1;
    protected double originalScale = 1;
    protected int skyboxLayer;
    protected int normalLayer;

    protected virtual void Awake()
    {
        skyboxLayer = LayerMask.NameToLayer("Skybox");
        normalLayer = gameObject.layer;
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        originalScale = transform.localScale.x;
    }

    public void SetPosition(Vector3 v, double scale)
    {
        SetPosition(v.x * scale, v.y * scale, v.z * scale);
    }

    public void SetPosition(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void SetPosition(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public void Translate(Vector3 v)
    {
        x += v.x;
        y += v.y;
        z += v.z;
    }
}
