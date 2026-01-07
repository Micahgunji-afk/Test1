using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveForce = 15f;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 force = new Vector3(h, 0f, v);
        rb.AddForce(force * moveForce, ForceMode.Force);
    }
}
