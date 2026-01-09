using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5f;  // Speed at which the enemy moves
    public float turningSpeed = 5f;  // How quickly the enemy turns to face the player
    public float stoppingForce = 10f;  // How quickly the enemy slows down when not moving towards the player
    public float bumpForce = 15f;  // Force applied to the player when bumped
    public string playerTag = "Player";  // Tag to identify the player object
    public float detectionRange = 10f;  // Range within which the enemy starts following the player

    private Rigidbody enemyRb;
    private Transform player;
    private bool isFollowing = false;

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // If the player is within detection range, start following
            if (distanceToPlayer <= detectionRange)
            {
                isFollowing = true;
            }
            else
            {
                isFollowing = false;
            }

            // If the enemy is following the player, move towards the player
            if (isFollowing)
            {
                MoveTowardsPlayer();
            }
            else
            {
                StopMovement();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        // Calculate the direction from the enemy to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Rotate the enemy towards the player gradually
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turningSpeed * Time.deltaTime);

        // Set the velocity of the enemy to move towards the player
        enemyRb.linearVelocity = directionToPlayer * speed;
    }

    private void StopMovement()
    {
        // Apply stopping force to gradually stop the enemy when not following
        enemyRb.linearVelocity = Vector3.Lerp(enemyRb.linearVelocity, Vector3.zero, stoppingForce * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the enemy collides with the player, apply bump force to the player
        if (collision.gameObject.CompareTag(playerTag))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                // Calculate the direction opposite to where the enemy is facing
                Vector3 bumpDirection = (collision.transform.position - transform.position).normalized;

                // Apply force to the player to bump them away from the enemy
                playerRb.AddForce(bumpDirection * bumpForce, ForceMode.Impulse);
            }
        }
    }
}
