using UnityEngine;

public class BalloonPopScript : MonoBehaviour
{
    [Range(0,1000)][SerializeField] float explosionForce = 20f;
    [SerializeField] SphereCollider[] explosionColliders;
    private Vector3[] explosionCollidersPosition;

    private void Awake()
    {
        explosionColliders = GetComponentsInChildren<SphereCollider>();
        explosionCollidersPosition = new Vector3[explosionColliders.Length];

        for (int i = 0; i < explosionColliders.Length; i++) 
        {
            explosionCollidersPosition[i] = explosionColliders[i].transform.localPosition;
        }
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

    private void OnDisable()
    {
        for (int i = 0; i < explosionColliders.Length; i++)
        {
            Rigidbody rb = explosionColliders[i].GetComponent<Rigidbody>();
            rb.transform.localPosition = explosionCollidersPosition[i];

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            explosionColliders[i].enabled = false;
        }
    }
}
