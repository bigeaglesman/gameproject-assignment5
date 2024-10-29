using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombSpawner : MonoBehaviour
{
	public GameObject bomb;

	public float playtime = 50f;


	public float startTime;

	public float spawnRate;

	public float spawnRadius = 20f;
	public float spawnMinRadius = 5f;

	private static System.Random random = new System.Random();
	private bool spawningActive = true;

	Terrain terrain;
	Vector3 terrainPosition;
	Vector3 terrainSize;
	public GameObject Player;
	private Transform playerTransform;

	void Start()
	{
		playerTransform = Player.transform;
		StartCoroutine(SpawnBombWave());
		StartCoroutine(StopSpawningBomb(playtime));
		terrain = Terrain.activeTerrain;
		terrainPosition = terrain.transform.position;
		terrainSize = terrain.terrainData.size;
	}

	IEnumerator SpawnBombWave()
	{
		yield return new WaitForSeconds(startTime);

		while (spawningActive)
		{
			SpawnBomb();

			yield return new WaitForSeconds(spawnRate);
		}
	}

	void SpawnBomb()
	{
		Vector3 spawnDirection = Quaternion.Euler(0, Random.Range(0f, 360f), 0) * playerTransform.forward;
		Vector3 spawnPosition = playerTransform.position + spawnDirection * Random.Range(spawnMinRadius, spawnRadius);
		spawnPosition.x = Mathf.Clamp(spawnPosition.x, terrainPosition.x, terrainPosition.x + terrainSize.x);
		spawnPosition.z = Mathf.Clamp(spawnPosition.z, terrainPosition.z, terrainPosition.z + terrainSize.z);
		spawnPosition.y += 15.0f;
		GameObject spawnedbomb = Instantiate(bomb,spawnPosition, Quaternion.identity);
	}

	IEnumerator StopSpawningBomb(float time)
    {
        yield return new WaitForSeconds(time);
        spawningActive = false;
    }
}
