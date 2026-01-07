using UnityEngine;

public class FallDeath : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }
}
