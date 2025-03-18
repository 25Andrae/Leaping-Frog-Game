using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private float timeUntilPowerUpSpawn;
    [SerializeField] private float powerUpSpeed = 10f;
    public float powerUpSpawnTime = 25f;
    private PlayerScript player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    private void Update()
    {
        float distance = player.distance;
        SpawnPowerUpLoop(Mathf.FloorToInt(player.distance));
    }

    private void SpawnPowerUpLoop(int distance)
    {
        timeUntilPowerUpSpawn += Time.deltaTime;
     /*   if ((distance % 100 == 0 && distance != 0) && powerUpSpawnTime > 2)
        {
            powerUpSpawnTime -= 1;
            powerUpSpawnTime = Mathf.Max(2, powerUpSpawnTime); // Ensure powerUpSpawnTime doesn't go below 2
         *//*   Debug.Log("Spawn Time Has Decreased: " + powerUpSpawnTime);*//*
        }*/
        if (timeUntilPowerUpSpawn >= powerUpSpawnTime)
        {
            SpawnPowerUp();
            timeUntilPowerUpSpawn = 0f;
        }
    }

    private void SpawnPowerUp()
    {
        GameObject powerUpToSpawn = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        Vector3 spawnPosition = transform.position;
        spawnPosition.y = Random.Range(-2f, 2f); // Adjust Y position within a range
        GameObject spawnPowerUp = Instantiate(powerUpToSpawn, spawnPosition, Quaternion.identity);
        StartCoroutine(MovePowerUp(spawnPowerUp));
    }

    private IEnumerator MovePowerUp(GameObject powerUp)
    {
        float absorbTimer = 10f; // Timer for absorbing the power-up
        while (powerUp != null && absorbTimer > 0f) // Check if the power-up GameObject is not null and the absorb timer hasn't expired
        {
            powerUp.transform.Translate(Vector3.left * (powerUpSpeed * Time.deltaTime));
            absorbTimer -= Time.deltaTime; // Decrease the absorb timer
            yield return null;
        }

        // Check if the power-up GameObject is still active and the absorb timer has expired
        if (powerUp != null && absorbTimer <= 0f)
        {
            Destroy(powerUp); // Destroy the power-up GameObject
        }
    }

    /*    private IEnumerator MovePowerUp(GameObject powerUp)
        {
            while (powerUp != null) // Check if the power-up GameObject is not null
            {
                powerUp.transform.Translate(Vector3.left * (powerUpSpeed * Time.deltaTime));
                yield return null;
            }

            // Power-up GameObject is destroyed, spawn a new one
           *//* SpawnPowerUp();*//*
        }*/

}
