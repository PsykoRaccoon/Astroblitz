using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            if (rb != null) rb.bodyType = RigidbodyType2D.Kinematic;
            Destroy(gameObject);
        }
    }
}
