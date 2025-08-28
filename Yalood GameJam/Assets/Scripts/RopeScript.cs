using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class RopeScript : MonoBehaviour
{
    [SerializeField] CapsuleCollider balloonHook;
    [SerializeField] Transform pumpHook;
    [Range(2,20)][SerializeField] int segmantCount = 12;
    [Range(0, 10)][SerializeField] float sagAmout = 0.5f;

    private LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segmantCount;
    }

    private void Update()
    {
        if (balloonHook != null && pumpHook != null)
        {
            Vector3 start = pumpHook.position;
            Vector3 end = balloonHook.transform.TransformPoint(balloonHook.center);

            //Stright line direction
            Vector3 diff = end - start;

            for (int i = 0; i < segmantCount; i++)
            {
                float t = i / (float)(segmantCount - 1); // normalized 0-1
                Vector3 point = Vector3.Lerp(start, end, t);

                //Add sag (parabola: max in middle, zero at ends)
                float sagFactor = (t - 0.5f) * (t - 0.5f) * -4 + 1; // parabola 0->1->0
                point.y -= sagFactor * sagAmout;

                line.SetPosition(i, point);
            }
        }
    }
}
