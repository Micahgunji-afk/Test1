using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    public Transform player;        // Reference to the Player
    public float moveSpeed = 5f;    // How fast enemy moves
    public float pushForce = 10f;   // Force applied when hitting player
    public float stopDistance = 1f; // Distance to start pushing

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Direction towards player
        Vector3 direction = (player.position - transform.position);
        direction.y = 0; // keep movement on XZ plane
        float distance = direction.magnitude;

        if (distance > 0.1f)
        {
            Vector3 moveDir = direction.normalized;

            // Move enemy
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        }

        // Apply push if close enough
        if (distance <= stopDistance)
        {
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 pushDir = (player.position - transform.position).normalized;
                pushDir.y = 0; // push horizontally
                playerRb.AddForce(pushDir * pushForce, ForceMode.Impulse);
            }
        }
    }
}
