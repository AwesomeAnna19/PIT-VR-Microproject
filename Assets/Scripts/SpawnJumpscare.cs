using UnityEngine;

public class SpawnJumpscare : MonoBehaviour
{
    public GameObject jumpscarePrefab;
    private float spawnTimer = 30f;
    private bool hasSpawned = false;
    private float startTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTimer = Time.time;

        // Starts with being hidden.
        if (jumpscarePrefab != null)
        {
            jumpscarePrefab.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned && Time.time >= startTimer + spawnTimer)
        {
            if (jumpscarePrefab != null)
            {
                // This moves the prefab to its spawn point.
                jumpscarePrefab.transform.position = transform.position;
                jumpscarePrefab.transform.rotation = transform.rotation;

                // Now the prefab is visible.
                jumpscarePrefab.SetActive(true);

                hasSpawned = true;
            }
        }
    }
}
