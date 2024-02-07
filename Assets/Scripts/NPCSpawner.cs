using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab; // Reference to the NPC prefab
    public Transform[] spawnPoints; // Array of spawn points for NPCs
    public float spawnInterval = 5f; // Interval between NPC spawns
    public int maxNPCsToSpawn = 10; // Maximum number of NPCs to spawn

    public int spawnedNPCs = 0;

    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNPCs());


        // Find the PlayerController component in the scene
        //playerController = FindObjectOfType<PlayerController>();

        //if (playerController == null)
        //{
        //    Debug.LogError("PlayerController not found in the scene!");
        //}
    }

    private IEnumerator SpawnNPCs()
    {
        while (spawnedNPCs < maxNPCsToSpawn)
        {
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomSpawnIndex];

            Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
            spawnedNPCs++;

            playerController.NPCsAvailable();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
