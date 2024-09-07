using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float moveSpeed;      
    public float friction;        
    public float rotationSpeed;   

    private Rigidbody2D rb;
    public Joystick joystick;  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (direction.magnitude > 0.1f)
        {
            rb.AddForce(direction.normalized * moveSpeed, ForceMode2D.Force); // Elimina Time.deltaTime

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        rb.linearVelocity *= (1 - friction * Time.deltaTime); // Ajuste la fricci�n en funci�n del tiempo
    }
}
