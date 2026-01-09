using UnityEngine;

public class BouncyEnemyCradle : MonoBehaviour
{
    public float bounceForce = 10f;  // The force with which the player is bumped
    public string playerTag = "Player";  // Tag to identify the player

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the enemy collided with an object tagged as "Player"
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Get the player's Rigidbody component
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                // Calculate the direction from the enemy to the player (the collision direction)
                Vector3 directionToPlayer = collision.transform.position - transform.position;

                // Get the normalized direction of the collision, keeping the Y axis intact (we don't want vertical movement)
                Vector3 pushDirection = new Vector3(directionToPlayer.x, 0f, directionToPlayer.z).normalized;

                // Apply force in the direction of the collision to simulate Newton's Cradle effect
                playerRb.AddForce(pushDirection * bounceForce, ForceMode.Impulse);
            }
        }
    }
}
