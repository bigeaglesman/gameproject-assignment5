using System.Collections;
using System.Collections.Generic;

using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Player;
    private Transform playerTransform;
    public float startTime;
    public float initialSpawnRate = 2f;
    public float minSpawnRate = 0.4f;
    public float spawnRateDecreaseAmount = 0.1f;
	public float minSpawnRadius = 10.0f;
    public float spawnRadius = 20f;
    public float spawnAngleOffset = 60f;
	public float playtime = 50f;
    private float currentSpawnRate;
    private static System.Random random = new System.Random();
    private bool spawningActive = true;
	Terrain terrain;
	Vector3 terrainPosition;
	Vector3 terrainSize;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        playerTransform = Player.transform;
        StartCoroutine(SpawnEnemyWave());
        StartCoroutine(StopSpawningAfterTime(playtime));
		terrain = Terrain.activeTerrain;
		terrainPosition = terrain.transform.position;
		terrainSize = terrain.terrainData.size;
    }

    IEnumerator SpawnEnemyWave()
    {
        yield return new WaitForSeconds(startTime);

        while (spawningActive)
        {
            Spawn();

            yield return new WaitForSeconds(currentSpawnRate);

            if (currentSpawnRate > minSpawnRate)
            {
                currentSpawnRate -= spawnRateDecreaseAmount;
            }
        }
    }

    void Spawn()
    {
        Vector3 spawnDirection = Quaternion.Euler(0, Random.Range(-spawnAngleOffset, spawnAngleOffset), 0) * playerTransform.forward;
        Vector3 spawnPosition = playerTransform.position + spawnDirection * Mathf.Max(random.Next() % spawnRadius, minSpawnRadius);
		spawnPosition.x = Mathf.Clamp(spawnPosition.x, terrainPosition.x, terrainPosition.x + terrainSize.x);
		spawnPosition.z = Mathf.Clamp(spawnPosition.z, terrainPosition.z, terrainPosition.z + terrainSize.z);

        GameObject spawnedEnemy = Instantiate(Enemy, spawnPosition, Quaternion.identity);
        spawnedEnemy.transform.LookAt(playerTransform.position);
    }

    IEnumerator StopSpawningAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        spawningActive = false;
		SceneManager.LoadScene(1);
    }

    void Update()
    {
        
    }
}
