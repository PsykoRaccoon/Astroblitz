using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float destructionDelay; 
    public Animator animator; 
    private Rigidbody2D rb;
    private Collider2D col;
    private Points pointsSystem;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        GameObject gameController = GameObject.FindWithTag("GameController");
        if (gameController != null)
        {
            pointsSystem = gameController.GetComponent<Points>();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (rb != null) rb.bodyType = RigidbodyType2D.Kinematic;

            if (pointsSystem != null)
            {
                pointsSystem.AddPoints(100);
            }

            StartCoroutine(DestroyAsteroid());
        }
    }

    IEnumerator DestroyAsteroid()
    {
        if (col != null) col.enabled = false;

        if (animator != null)
        {
            Handheld.Vibrate();
            animator.SetTrigger("Destroy"); 
        }

        yield return new WaitForSeconds(destructionDelay);

        Destroy(gameObject);
    }
}
