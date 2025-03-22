using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
       public string waveName;
       public List<EnemyGroup> enemyGroups; // A list of all the enemy groups in this wave
       public int waveQuota; // The total number of enemies to spawn in this wave
       public float spwanInterval;
       public int spawnCount; // The number of enemies that have been spawned in this wave

    }

    [System.Serializable]
    public class EnemyGroup
    {
        public GameObject enemyPrefab;
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
    }

    public List<Wave> waves; // A list of all the waves in the game
    public int currentWaveCount; //The index of the current wave


    [Header("Spawner Attributes")]
    float spawnTimer; // The time between enemy spawns
    public float waveInterval; // The time between waves
    public int enemiesAlive; // The number of enemies currently alive in the game
    public int maxEnemiesAllowed; // The maximum number of enemies that can be alive at once
    public bool maxEnemiesReached; // A flag to check if the maximum number of enemies has been reached
    Transform player; // The player object

    [Header("Spawn Positioning")]
    public List<Transform> relativeSpawnPoints; // A list of all the spawn points relative to the player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>().transform;
        CalculateWaveQuota();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there are still more waves, and if the curreent wave has ended, the next wave should begin 
        if(waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota)
        {
            StartCoroutine(BeginNextWave());
        } 
        

        
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= waves[currentWaveCount].spwanInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
            
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);
        if(currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
        else
        {
            Debug.LogWarning("No more waves");
        }
    }

    void CalculateWaveQuota()
    {
        // Calculate the total number of enemies to spawn in the current wave
        int currentWaveQuota = 0;
        foreach (EnemyGroup enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning("Wave Quota: " + currentWaveQuota);
    }

    void SpawnEnemies()
    {
       if(waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
       {
           foreach (EnemyGroup enemyGroup in waves[currentWaveCount].enemyGroups)
           {
               if(enemyGroup.spawnCount < enemyGroup.enemyCount)
               {
                    if(enemiesAlive>= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);
                  
                 
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
               }
           }
       }

       if(enemiesAlive < maxEnemiesAllowed)
       {
           maxEnemiesReached = false;
       }       
       
    }

    public void onEnemyKilled()
    {
        enemiesAlive--;
    }

}
