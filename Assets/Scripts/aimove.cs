using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBumperAI : MonoBehaviour
{
    [Header("Movement")]
    public float moveForce = 18f;
    public float maxSpeed = 8f;

    [Header("Safety")]
    public float fallDeathY = -5f;

    Rigidbody rb;
    Transform player;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Ensure correct Rigidbody settings
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void FixedUpdate()
    {
        AcquirePlayer();
        MoveTowardsPlayer();
        LimitSpeed();
        CheckFallDeath();
    }

    void AcquirePlayer()
    {
        if (player != null) return;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.1f) return;

        direction.Normalize();
        rb.AddForce(direction * moveForce, ForceMode.Force);
    }

    void LimitSpeed()
    {
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (horizontalVelocity.magnitude > maxSpeed)
        {
            Vector3 limited = horizontalVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limited.x, rb.linearVelocity.y, limited.z);
        }
    }

    void CheckFallDeath()
    {
        if (transform.position.y < fallDeathY)
        {
            Destroy(gameObject);
        }
    }
}
