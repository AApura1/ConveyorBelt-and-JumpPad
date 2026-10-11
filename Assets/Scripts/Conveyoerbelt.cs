
using UnityEngine;

public class Conveyorbelt : MonoBehaviour
{
    [SerializeField] float beltSpeed = 3f;
    [SerializeField] Vector3 beltDirection = Vector3.right;

    void OnCollisionStay(Collision c)
    {
        Rigidbody rb = c.rigidbody;

        if (rb == null) return;

        Vector3 direction =
            transform.TransformDirection(beltDirection).normalized;

        direction.y = 0f;
        direction.Normalize();

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
