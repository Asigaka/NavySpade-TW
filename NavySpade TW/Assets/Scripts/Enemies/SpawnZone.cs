using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public Vector3 GetRandomPointInZone()
    {
        Vector3 randPos = transform.position +
            new Vector3(Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2),
            Random.Range(-transform.localScale.y / 2, transform.localScale.y / 2),
            Random.Range(-transform.localScale.z / 2, transform.localScale.z / 2));
        return randPos;
    }    
}
