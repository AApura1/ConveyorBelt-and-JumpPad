
using UnityEngine;

public class Conveyorbelt : MonoBehaviour
{
    [SerializeField] float beltSpeed = 3f;
    [SerializeField] Vector3 beltDirection = Vector3.right;

    void OnCollisionStay(Collision c)
    {
        Rigidbody rb = c.rigidbody;

        if (rb == null) return;

        // Convert the local belt direction to world direction
        Vector3 direction =
            transform.TransformDirection(beltDirection).normalized;

        // Prevent upward or downward movement from the belt
        direction.y = 0f;
        direction.Normalize();

        // Measure current speed along the belt
        float currentSpeed = Vector3.Dot(
            rb.linearVelocity, direction
        );

        float missing = beltSpeed - currentSpeed;

        rb.AddForce(
            direction * missing,
            ForceMode.VelocityChange
        );
    }
}
