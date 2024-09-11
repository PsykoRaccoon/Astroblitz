using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float thrustForce;
    public float rotationSpeed;
    public float drag;
    public Joystick joystick; // Referencia al joystick virtual

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = drag; // Asignamos la resistencia inicial para simular el "derrape"
    }

    void Update()
    {
        // Obtén la entrada del joystick
        Vector2 inputDirection = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (inputDirection.magnitude > 0.1f) // Solo se mueve si la magnitud del input es significativa
        {
            // Calcula el ángulo de rotación deseado basado en la entrada del joystick
            float targetAngle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg - 90f;

            // Gira suavemente la nave hacia el ángulo objetivo usando Time.deltaTime
            float angle = Mathf.LerpAngle(rb.rotation, targetAngle, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(angle);

            // Aplica una fuerza en la dirección del joystick, ajustada por Time.deltaTime
            rb.AddForce(inputDirection.normalized * thrustForce * Time.deltaTime);
        }
        else
        {
            // Aumenta el valor del drag para que la nave desacelere más rápidamente cuando no hay entrada
            rb.linearDamping = drag;
        }
    }

}
