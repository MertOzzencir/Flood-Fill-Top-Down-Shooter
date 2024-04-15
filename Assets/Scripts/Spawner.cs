using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Enemy enemy;
    Wave currentWave;

    int currentWaveNumber;
    int enemiesRemaningToSpawn;
    float nextSpawnTime;
    int enemiesRemaningAlive;
    MapGenerator map;

    private void Start()
    {
        map = FindObjectOfType<MapGenerator>();
        NextWave();
    }
    private void Update()
    {
        if (enemiesRemaningToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemaningToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        float spawnDelay = 1f;
        float tileFlashSpeed = 4f;
        Transform randomTile = map.getRandomOpenTile();
        Material tileMat = randomTile.GetComponent<Renderer>().material;
        Color initialColor = tileMat.color;
        Color flashColor = Color.red;
        float spawnTimer = 0f;
        while(spawnTimer < spawnDelay)
        {
            tileMat.color = Color.Lerp(initialColor, flashColor, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1));

            spawnTimer += Time.deltaTime;
            yield return null;
        }
        
        Enemy spawnedEnemy = Instantiate(enemy, randomTile.position + Vector3.up, Quaternion.identity) as Enemy;
        spawnedEnemy.OnDeath += EnemyOnDeath;
       

    }
    void NextWave()
    {
        currentWaveNumber++;
        if(currentWaveNumber <= waves.Length) { 

            currentWave = waves[currentWaveNumber - 1]; 
            enemiesRemaningToSpawn = currentWave.enemyCount;
            enemiesRemaningAlive = enemiesRemaningToSpawn;
        }

    }
    void EnemyOnDeath()
    {
        enemiesRemaningAlive--;
        if(enemiesRemaningAlive == 0)
        {
            NextWave();

        }

    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;

    }
   

}
