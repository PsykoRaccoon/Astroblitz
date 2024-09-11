using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float thrustForce;
    public float rotationSpeed;
    public float drag;
    public Joystick joystick; 

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = drag; 
    }

    void Update()
    {
        Vector2 inputDirection = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (inputDirection.magnitude > 0.1f) 
        {
            float targetAngle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg - 90f;

            float angle = Mathf.LerpAngle(rb.rotation, targetAngle, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(angle);

            rb.AddForce(inputDirection.normalized * thrustForce * Time.deltaTime);
        }
        else
        {
            rb.linearDamping = drag;
        }
    }
}
