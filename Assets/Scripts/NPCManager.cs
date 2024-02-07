using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject[] npcPrefabs; // Array of NPC prefabs to spawn
    public Transform[] spawnPoints; // Array of spawn points for NPCs
    public int numberOfNPCsToSpawn = 10; // Number of NPCs to spawn
    public float spawnInterval = 5f; // Interval between NPC spawns
    public int maxNPCsToSpawn = 10; // Maximum number of NPCs to spawn

    private int spawnedNPCs = 0;
    private float nextSpawnTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnNPCs();
        nextSpawnTime = Time.time + spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedNPCs < maxNPCsToSpawn && Time.time >= nextSpawnTime)
        {
            SpawnNPCs();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }
    private void SpawnNPCs()
    {

        if (spawnedNPCs < maxNPCsToSpawn)
        {
            int randomPrefabIndex = Random.Range(0, npcPrefabs.Length);
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            GameObject npcPrefab = npcPrefabs[randomPrefabIndex];
            Transform spawnPoint = spawnPoints[randomSpawnIndex];

            GameObject npc = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
            npc.transform.SetParent(transform);
            
            spawnedNPCs++;
        }
    }
}
