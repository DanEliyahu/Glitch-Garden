using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnDelay = 1f;
    [SerializeField] private float maxSpawnDelay = 5f;
    [SerializeField] private Attacker attackerPrefab;

    private bool spawn = true;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnAttacker();
        }
    }

    private void SpawnAttacker()
    {
        if (attackerPrefab)
        {
            var newAttacker = Instantiate(attackerPrefab, transform.position, Quaternion.identity);
            newAttacker.transform.SetParent(transform);
        }
    }
}