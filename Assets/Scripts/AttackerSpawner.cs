using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnDelay = 1f;
    [SerializeField] private float maxSpawnDelay = 5f;
    [SerializeField] private Attacker[] attackersPrefabs;

    private bool _spawn = true;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (_spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnAttacker();
        }
    }

    public void StopSpawning()
    {
        _spawn = false;
    }
    private void SpawnAttacker()
    {
        if (attackersPrefabs.Length > 0)
        {
            var index = Random.Range(0, attackersPrefabs.Length);
            var transform1 = transform;
            Instantiate(attackersPrefabs[index], transform1.position, Quaternion.identity,transform1);
        }
    }
}