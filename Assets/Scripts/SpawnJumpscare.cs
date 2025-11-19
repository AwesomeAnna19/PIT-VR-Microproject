using UnityEngine;

public class SpawnJumpscare : MonoBehaviour
{
    // The jumpscare prefab to be spawned.
    public GameObject jumpscarePrefab;

    // Time in seconds before the jumpscare spawns.
    private float spawnTimer = 30f;

    // Boolean to track if the jumpscare has been spawned.
    private bool hasSpawned = false;

    // Timer to track the start time.
    private float startTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Records the start time in seconds.
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
        // Checks if the jumpscare has not been spawned 
        // and if the spawn timer has elapsed/passed the spawn timer.
        if (!hasSpawned && Time.time >= startTimer + spawnTimer)
        {
            // If conditions are met, it spawns the jumpscare.
            if (jumpscarePrefab != null)
            {
                // This moves the prefab to its spawn point.
                jumpscarePrefab.transform.position = transform.position;
                jumpscarePrefab.transform.rotation = transform.rotation;

                // Now the prefab is visible.
                jumpscarePrefab.SetActive(true);
                
                // The jumpscare has been spawned.
                hasSpawned = true;
            }
        }
    }
}
