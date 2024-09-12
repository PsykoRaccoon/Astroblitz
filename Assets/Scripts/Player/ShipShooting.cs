using System.Collections;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public Transform[] shootPoints; // Array de puntos de disparo
    public GameObject projectilePrefab; // Prefab del proyectil
    public float fireRate; // Tiempo entre disparos en segundos
    public float projectileSpeed; // Velocidad del proyectil
    public Joystick joystick; // Referencia al joystick virtual

    private float nextFireTime = 0f; // Tiempo en el que se puede disparar de nuevo

    void Update()
    {
        // Verifica si no hay input del joystick
        if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            // Si no hay input, dispara continuamente según el fireRate
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate; // Actualiza el tiempo para el próximo disparo
            }
        }
    }

    void Shoot()
    {
        // Dispara desde cada punto de disparo
        foreach (Transform shootPoint in shootPoints)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = shootPoint.up * projectileSpeed; // Aplica la velocidad en la dirección del shootPoint

            // Destruir el proyectil después de un tiempo si no es visible
            StartCoroutine(DestroyIfNotVisible(projectile, 3f));
        }
    }

    IEnumerator DestroyIfNotVisible(GameObject projectile, float delay)
    {
        // Espera un tiempo antes de verificar si el proyectil sigue siendo visible
        yield return new WaitForSeconds(delay);

        if (!projectile.GetComponent<Renderer>().isVisible) // Verifica si el proyectil es visible en la cámara
        {
            Destroy(projectile);
        }
    }
}
