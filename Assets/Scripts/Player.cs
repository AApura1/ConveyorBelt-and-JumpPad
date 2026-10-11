
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 7f;

    [Header("Ground Check")]
    [SerializeField] float groundCheckDistance = 0.15f;

    Rigidbody rb;
    Collider playerCollider;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
    }

    void Update()
    {
        CheckGround();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(
                Vector3.up * jumpForce,
                ForceMode.Impulse
            );
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(h, 0f, v).normalized;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = move.x * moveSpeed;
        velocity.z = move.z * moveSpeed;

        rb.linearVelocity = velocity;
    }

    void CheckGround()
    {
        if (playerCollider == null) return;

        Vector3 origin = playerCollider.bounds.center;
        float halfHeight = playerCollider.bounds.extents.y;

        isGrounded = Physics.Raycast(
            origin,
            Vector3.down,
            halfHeight + groundCheckDistance,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore
        );
    }
}
