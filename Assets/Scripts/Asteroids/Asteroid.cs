using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float destructionDelay = 1f; // Tiempo de espera antes de destruir el asteroide
    public Animator animator; // Referencia al Animator para la animación de destrucción
    private Rigidbody2D rb;
    private Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto que colisiona tiene el tag "Bullet"
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Cambia el tipo de cuerpo a Kinematic para que el asteroide no reaccione a la fuerza de la bala
            if (rb != null) rb.bodyType = RigidbodyType2D.Kinematic;

            StartCoroutine(DestroyAsteroid());
        }
    }

    IEnumerator DestroyAsteroid()
    {
        // Desactiva el Collider para evitar más colisiones
        if (col != null) col.enabled = false;

        // Inicia la animación de destrucción
        if (animator != null)
        {
            animator.SetTrigger("Destroy"); // Asegúrate de que la animación tenga un trigger llamado "Destroy"
        }

        // Espera por el tiempo definido antes de destruir el asteroide
        yield return new WaitForSeconds(destructionDelay);

        // Destruye el asteroide
        Destroy(gameObject);
    }
}
