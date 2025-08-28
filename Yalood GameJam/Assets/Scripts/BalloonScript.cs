using UnityEngine;

public class BalloonScript : MonoBehaviour
{
    [Range(0,100)][SerializeField] float balloonInflation;
    [Range(0.1f, 20)][SerializeField] float deflationSpeed;
    [Range(80, 150)][SerializeField] float balloonPopZone = 100f;
    [SerializeField] GameObject poppedBalloon;

    private BalloonFloatScript floatScript;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private int blendShapeIndex;
    private bool popped = false;

    void Awake()
    {
        floatScript = GetComponent<BalloonFloatScript>();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("Inflated");
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
}
