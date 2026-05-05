using UnityEngine;

public class NPCSpawnManager : MonoBehaviour
{
    [Header("Settings")]
    public GameObject npcPrefab;
    public Transform[] spawnPoints;
    public int totalSkins = 9;

    private void Start()
    {
        SpawnNPCs();
    }

    private void SpawnNPCs()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject npcObj = Instantiate(npcPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
            npcObj.name = $"NPC_{i + 1}";

            // Random skin (duplicates allowed)
            NPCInteractable npc = npcObj.GetComponent<NPCInteractable>();
            if (npc != null)
            {
                int randomSkin = Random.Range(0, totalSkins);
                npc.SetSkin(randomSkin);
            }
        }
    }
}