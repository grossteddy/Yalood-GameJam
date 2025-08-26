using UnityEngine;

public class BalloonScript : MonoBehaviour
{
    [Range(0,100)][SerializeField] float balloonInflation;
    [Range(0.1f, 20)][SerializeField] float deflationSpeed;
    [Range(100, 300)][SerializeField] float balloonPopZone;

    private BalloonFloatScript floatScript;
    private SkinnedMeshRenderer SkinnedMeshRenderer;
    private int blendShapeIndex;

    void Awake()
    {
        floatScript = GetComponent<BalloonFloatScript>();
        SkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        blendShapeIndex = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("Inflated");
    }
    
    void OnEnable()
    {
        balloonInflation = 0;
    }

    void Update()
    {
        Inflate(0.1f);
        SetInflationInBalloon();
        Deflate();
    }

    public void Inflate(float inflationAir)
    {
        balloonInflation += inflationAir;
        if (balloonInflation > balloonPopZone)
        {
            BallooonPop();
        }
    }

    private void SetInflationInBalloon()
    {
        SkinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, balloonInflation);
        floatScript.SetInflationLevel(Mathf.InverseLerp(0,100, balloonInflation));
    }

    private void BallooonPop()
    {
        //logic for balloonPopping
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
