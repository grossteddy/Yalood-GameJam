using UnityEngine;

public class BalloonScript : MonoBehaviour
{
    [Range(0,100)][SerializeField] float balloonInflation;
    [Range(0.1f, 20)][SerializeField] float deflationSpeed;
    [Range(80, 150)][SerializeField] float balloonPopZone = 100f;
    [SerializeField] GameObject poppedBalloon;
    [SerializeField] GameManagerScript gameManagerScript;
    [SerializeField] GameObject pump;
    [SerializeField] RopeScript rope;
    [SerializeField] GameObject detachedHook;

    private BalloonFloatScript floatScript;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private SpringJoint springJoint;
    private int blendShapeIndex;
    private bool popped = false;
    private float pumpSpring;

    void Awake()
    {
        floatScript = GetComponent<BalloonFloatScript>();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        springJoint = GetComponent<SpringJoint>();
        blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("Inflated");
        pumpSpring = springJoint.spring;
    }
    
    void OnEnable()
    {
        balloonInflation = 0;
        popped = false;
    }

    void Update()
    {
        if (!popped)
        {
            SetInflationInBalloon();
            Deflate();
        }
    }

    public void Inflate(float inflationAir)
    {
        if (!popped)
        {
            balloonInflation += inflationAir;
            if (balloonInflation > balloonPopZone)
            {
                BallooonPop();
            }
        }
    }

    private void SetInflationInBalloon()
    {
        skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, balloonInflation);
        floatScript.SetInflationLevel(Mathf.InverseLerp(0,100, balloonInflation));
    }

    private void BallooonPop()
    {
        poppedBalloon.transform.parent = null;
        poppedBalloon.SetActive(true);


        popped = true;
        balloonInflation = 0;
        skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, balloonInflation);
        floatScript.SetInflationLevel(balloonInflation);
        skinnedMeshRenderer.enabled = false;

        gameManagerScript.HandlePoppedBalloon();
        //gameObject.SetActive(false);
    }

    public float GetDeflationSpeed()
    {
        return deflationSpeed;
    }

    public void SetDeflationSpeed(float newSpeed)
    {
        if (newSpeed > 0)
        {
            deflationSpeed = newSpeed;
        }
    }
    
    private void Deflate()
    {
        if (balloonInflation > 0)
        {
            balloonInflation = balloonInflation - (deflationSpeed * Time.deltaTime);
        }
    }

    public void ResetBalloon()
    {
        skinnedMeshRenderer.enabled = true;
        springJoint.spring = pumpSpring;
        springJoint.connectedBody = pump.GetComponent<Rigidbody>();
        balloonInflation = 0;
        skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, balloonInflation);
        floatScript.SetInflationLevel(balloonInflation);
        popped = false;


        detachedHook.transform.parent = this.transform;
        detachedHook.transform.position = this.transform.position;
        detachedHook.transform.rotation = this.transform.rotation;
        detachedHook.SetActive(false);
        rope.ChangeBalloonHook(GetComponent<CapsuleCollider>());

        poppedBalloon.transform.parent = this.transform;
        poppedBalloon.transform.position = this.transform.position;
        poppedBalloon.transform.rotation = this.transform.rotation;
        poppedBalloon.SetActive(false);

        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //springJoint.autoConfigureConnectedAnchor = true;

    }

    public void OnBankBalloon()
    {
        popped = true;
        detachedHook.SetActive(true);
        rope.ChangeBalloonHook(detachedHook.GetComponent<CapsuleCollider>());
        detachedHook.transform.parent = null;
        springJoint.connectedBody = null;
        springJoint.spring = 0;


    }

    public float GetCurrentSize() => balloonInflation;
}
