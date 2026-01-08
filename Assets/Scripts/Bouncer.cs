using UnityEngine;

public class PinballBouncer : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float bounceForce = 15f;
    public bool useUpwardBoost = true;
    public float upwardForce = 3f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;

        // Only bounce objects with a Rigidbody
        if (rb == null) return;

        // Direction away from the bouncer
        Vector3 bounceDirection = (collision.transform.position - transform.position).normalized;

        // Optional upward kick (pinball feel)
        if (useUpwardBoost)
        {
            bounceDirection.y += upwardForce;
        }

        rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
    }
}
