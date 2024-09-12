using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float destructionDelay = 1f; 
    public Animator animator; 
    private Rigidbody2D rb;
    private Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (rb != null) rb.bodyType = RigidbodyType2D.Kinematic;

            StartCoroutine(DestroyAsteroid());
        }
    }

    IEnumerator DestroyAsteroid()
    {
        if (col != null) col.enabled = false;

        if (animator != null)
        {
            animator.SetTrigger("Destroy"); 
        }

        yield return new WaitForSeconds(destructionDelay);

        Destroy(gameObject);
    }
}
