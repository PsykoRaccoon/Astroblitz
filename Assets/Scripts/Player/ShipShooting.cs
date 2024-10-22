using System.Collections;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public Transform[] shootPoints; 
    public GameObject projectilePrefab; 
    public float fireRate; 
    public float projectileSpeed; 
    public Joystick joystick; 
    private float nextFireTime = 0f;

    public AudioClip soundClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundClip;
    }

    void Update()
    {
        if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate; 
            }
        }
    }

    void Shoot()
    {
        foreach (Transform shootPoint in shootPoints)
        {
            audioSource.Play();
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = shootPoint.up * projectileSpeed; 

            StartCoroutine(Destroy(projectile, 1f));
        }
    }

    IEnumerator Destroy(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(projectile);
    }
}
