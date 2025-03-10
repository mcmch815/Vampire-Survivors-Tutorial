using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerMovement pm;

    //To disable far away chunks
    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    public GameObject latestChunk;
    public float maxOpDist; //Must be greater than the length and width of the tilemap
    float opDist; //Reference current distance of each chunk from our player
    float optimizerCooldown;
    public float optimizerCooldownDur;

    void Start()
    {
        pm = FindAnyObjectByType<PlayerMovement>();
    }
    
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }
    void ChunkChecker()
    {
        //If there is no currentchunk then do not check and spawn
        if(!currentChunk)
        {
            return;
        }

        //Otherwise 
        CheckAndSpawnChunk("Right", pm.moveDir.x > 0 && pm.moveDir.y == 0); // right
        CheckAndSpawnChunk("Left", pm.moveDir.x < 0 && pm.moveDir.y == 0); // left
        CheckAndSpawnChunk("Up", pm.moveDir.x == 0 && pm.moveDir.y > 0); // up
        CheckAndSpawnChunk("Down", pm.moveDir.x == 0 && pm.moveDir.y < 0); // down
        CheckAndSpawnChunk("Right Up", pm.moveDir.x > 0 && pm.moveDir.y > 0); // right up
        CheckAndSpawnChunk("Right Down", pm.moveDir.x > 0 && pm.moveDir.y < 0); // right down
        CheckAndSpawnChunk("Left Up", pm.moveDir.x < 0 && pm.moveDir.y > 0); // left up
        CheckAndSpawnChunk("Left Down", pm.moveDir.x < 0 && pm.moveDir.y < 0); // left down
        
    }

    void CheckAndSpawnChunk(string offset, bool condition)
    {
        // Checking what direction the player is moving  and if there is no terrain in that direction
        if (condition && !Physics2D.OverlapCircle(currentChunk.transform.Find(offset).position, checkerRadius, terrainMask))
        {
            // If there is no terrain, store the position where the chunk will be spawned
            noTerrainPosition = currentChunk.transform.Find(offset).position;
            // Call the method to spawn a new chunk at the determined position
            SpawnChunk();
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);

    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if(optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }
        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }

    }
}
