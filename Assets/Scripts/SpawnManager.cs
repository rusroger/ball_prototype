using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRange = 9f;
    private int enemyCount = 0;
    public int round = 1;

    void Start()
    {
        SpawnEnemy(round);
        Debug.Log("Ronda " + round);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
    }

    void SpawnEnemy(int enemyToSpawn)
    {
        for(int i = 0; i < enemyToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPosition = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPosition;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0)
        {
            round++;
            SpawnEnemy(round);
            Debug.Log("Ronda " + round);
        }
    }
}
