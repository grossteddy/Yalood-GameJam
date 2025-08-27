using UnityEngine;

public class BalloonPopScript : MonoBehaviour
{
    [Range(0,1000)][SerializeField] float explosionForce = 20f;
    [SerializeField] SphereCollider[] explosionColliders;

    private void Awake()
    {
        explosionColliders = GetComponentsInChildren<SphereCollider>();
    }

    private void OnEnable()
    {
        PopExplosion();
    }

    private void PopExplosion()
    {
        foreach (var col in explosionColliders)
        {
            col.enabled = true;
            Rigidbody rb = col.GetComponent<Rigidbody>();
            rb.AddExplosionForce(explosionForce, transform.position, 2f);
        }
    }
}
