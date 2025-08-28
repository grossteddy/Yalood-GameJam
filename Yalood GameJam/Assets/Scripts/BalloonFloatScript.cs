using UnityEngine;

public class BalloonFloatScript : MonoBehaviour
{
    [Range(0.1f, 10)][SerializeField] float liftForce = 5f;
    [Range(0, 1)][SerializeField] float InflationLevel = 0f;
    [SerializeField] Transform deflatedAnchor;
    [SerializeField] Transform InflatedAnchor;

    private Rigidbody rb;
    private SphereCollider balloonCollider;
    private CapsuleCollider balloonAnchor;
    private float balloonRadius;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        balloonCollider = GetComponent<SphereCollider>();
        balloonAnchor = GetComponent<CapsuleCollider>();
        balloonRadius = balloonCollider.radius;
    }

    void FixedUpdate()
    {
        float currentLift = liftForce * InflationLevel;
        rb.AddForce(Vector3.up * currentLift, ForceMode.Force);
        balloonCollider.radius = Mathf.Lerp(balloonRadius, balloonRadius * 10, InflationLevel);
        balloonAnchor.center = Vector3.Lerp(deflatedAnchor.localPosition, InflatedAnchor.localPosition, InflationLevel);
    }

    public void SetInflationLevel(float level)
    {
        if (level > 1)
        {
            level = 1f;
        }
        InflationLevel = level;
    }
}
