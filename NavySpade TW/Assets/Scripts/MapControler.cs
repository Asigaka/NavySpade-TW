using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControler : MonoBehaviour
{
    [SerializeField] private Transform ground;

    public static MapControler Instance;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    public Vector3 GetRandomPosOnGround()
    {
        return new Vector3(Random.Range(-ground.localScale.x * 5, ground.localScale.x * 5),
            0, Random.Range(-ground.localScale.z * 5, ground.localScale.z * 5));
    }

    public Vector3 GetRandomPosOnSpawnZone(List<SpawnZone> spawnZones)
    {
        int randIndex = Random.Range(0, spawnZones.Count);

        return spawnZones[randIndex].GetRandomPointInZone();
    }
}
