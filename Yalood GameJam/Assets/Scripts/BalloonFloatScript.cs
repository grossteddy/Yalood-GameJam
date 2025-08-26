using UnityEngine;

public class BalloonFloatScript : MonoBehaviour
{
    [Range(0.1f, 10)][SerializeField] float liftForce = 5f;
    [Range(0, 1)][SerializeField] float InflationLevel = 0f;

    private Rigidbody rb;
    private SphereCollider balloonCollider;

    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        balloonCollider = GetComponent<SphereCollider>();
    }

    void FixedUpdate()
    {
        float currentLift = liftForce * InflationLevel;
        rb.AddForce(Vector3.up * currentLift, ForceMode.Force);
    }

    public void SetInflationLevel(float level)
    {
        if (level > 1)
        {
            level = 1f;
        }
        InflationLevel = level;
        balloonCollider.radius = Mathf.Lerp(0.001f, 0.01f, level);
    }
}
