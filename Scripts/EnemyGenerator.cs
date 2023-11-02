using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class EnemyGenerator : MonoBehaviour
{
    [Header("Settings")]
    public float checkPlayerDistance = 15f;      // distance within which to check for player
    public int requiredNearbyEnemies = 5;        // number of enemies to maintain nearby
    public float spawnInterval = 10f;            // time interval between spawns

    [Header("References")]
    public List<GameObject> enemyPrefabs;        // list of enemy prefabs to choose from
    private Transform player;                    // reference to player transform
    public List<GameObject> initialEnemies;      // list of initial enemies set in the inspector

    private float spawnTimer;                    // timer for spawning new enemies

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject)
        {
            player = playerObject.transform;
        }
        else
        {
            UnityEngine.Debug.Log("Player GameObject with tag 'Player' not found!");
        }
    }

    private void Update()
    {
        if (player == null) 
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject)
            {
                player = playerObject.transform;
            }
        }

        if (player == null) return;

        // Check distance to player
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= checkPlayerDistance)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;
                CheckAndSpawnEnemies();
            }
        }
        else
        {
            spawnTimer = 0f; // Reset timer if player is out of range
        }
    }

    private void CheckAndSpawnEnemies()
    {
        int nearbyEnemyCount = 0;
        foreach (GameObject enemy in initialEnemies)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
                if (distanceToEnemy <= checkPlayerDistance)
                {
                    nearbyEnemyCount++;
                }
            }
        }

        if (nearbyEnemyCount < requiredNearbyEnemies)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Count == 0) return;

        int randomIndex = UnityEngine.Random.Range(0, enemyPrefabs.Count);
        GameObject randomEnemyPrefab = enemyPrefabs[randomIndex];
        GameObject newEnemy = Instantiate(randomEnemyPrefab, transform.position, Quaternion.identity);
        initialEnemies.Add(newEnemy);
    }
}
