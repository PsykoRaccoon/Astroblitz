using UnityEngine;

public class AsteroidsManager : MonoBehaviour
{
    public GameObject[] objectsToSpawn; 
    public GameObject target; 
    public float spawnInterval;
    public float launchForce;
    public float driftAmount;
    public float objectLifetime;

    private void Start()
    {
        InvokeRepeating("SpawnObject", spawnInterval, spawnInterval);
    }

    private void SpawnObject()
    {
        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        GameObject selectedObject = objectsToSpawn[randomIndex];

        GameObject spawnedObject = Instantiate(selectedObject, transform.position, Quaternion.identity);

        Vector2 directionToTarget = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;

        Vector2 randomOffset = new Vector2(Random.Range(-driftAmount, driftAmount), Random.Range(-driftAmount, driftAmount));

        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        rb.AddForce((directionToTarget + randomOffset) * launchForce, ForceMode2D.Impulse);

        Destroy(spawnedObject, objectLifetime);
    }
}
