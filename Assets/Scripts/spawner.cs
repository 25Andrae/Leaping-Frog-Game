using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    private float timeUntilObstancleSpawn = 1f;

    private float obstacleSpeed = 20f;
    public float obstacleSpawnTime = 7f;

    PlayerScript player;
    private float timeAlive;
    private int randomSeed = 0;
    private float randomObstacleSpawnTime;
    [SerializeField] private float _obstacleSpeed = 0f;
    [SerializeField] private float _obstacleSpawnTime = 10f;

    [Range(0, 1)] public float obstacleSpawnTimeFactor = 0.1f;
    [Range(0, 1)] public float obstacleSpeedFactor = 0.2f;


    // Update is called once per frame
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        timeAlive = 1f;
    }


    void Update()
    {
        float distance = player.distance;
        if (FroggyColliderDeath.isFroggyAlive) {
            timeAlive+=Time.deltaTime;
            spawnLoop();
            obstacleFactor();
        }
        /*Debug.Log("This is obstacle SpawnTime: " + _obstacleSpawnTime + '\n' + " This is the Obstacle Speed Factor: " + _obstacleSpeed);*/
    }

    private void spawnLoop()
    {
        timeUntilObstancleSpawn += Time.deltaTime;
        float temp = obstacleSpawnTime;
        randomObstacleSpawnTime = Random.Range(temp/2, temp);
        if (!obstacleDespawner.isTheFirstObjectHasBeenDestroyed)
        {
            if (timeUntilObstancleSpawn >= randomObstacleSpawnTime)
            {
                spawn();
                timeUntilObstancleSpawn = 0f;
            }
        }
        else {
            if (timeUntilObstancleSpawn >= Random.Range(_obstacleSpawnTime/2, _obstacleSpawnTime))
            {
                spawn();
                timeUntilObstancleSpawn = 0f;
            }
        }
    }


    private void obstacleFactor() {
        _obstacleSpawnTime = obstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        _obstacleSpeed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
    }

    private void spawn()
    {
       
        if (randomSeed == 0)
        {
            GameObject obstableToSpawn = obstaclePrefabs[randomSeed];
            GameObject spawnObstacles = Instantiate(obstableToSpawn, transform.position, Quaternion.identity);
            Vector3 newPositon = spawnObstacles.transform.position;
            newPositon.y = -6.25f;
            spawnObstacles.transform.position = newPositon;
            Rigidbody2D obstableRb = spawnObstacles.GetComponent<Rigidbody2D>();
            Vector2 prefabPos = spawnObstacles.transform.position;
            obstableRb.AddForce(Vector2.left * _obstacleSpeed, ForceMode2D.Impulse);
        }
        else {
            GameObject obstableToSpawn = obstaclePrefabs[randomSeed];
            GameObject spawnObstacles = Instantiate(obstableToSpawn, transform.position, Quaternion.identity);
            Vector3 newPositon = spawnObstacles.transform.position;
            newPositon.y = 3.0f;
            spawnObstacles.transform.position = newPositon;
            Rigidbody2D obstableRb = spawnObstacles.GetComponent<Rigidbody2D>();
            Vector2 prefabPos = spawnObstacles.transform.position;
            obstableRb.AddForce(Vector2.left * _obstacleSpeed, ForceMode2D.Impulse);
        }
        randomSeed = Random.Range(0, obstaclePrefabs.Length);
    }
}
