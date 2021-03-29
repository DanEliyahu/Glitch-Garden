using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private int _numOfAttackers = 0;

    private bool _levelTimerFinished = false;
    public void AttackerSpawned()
    {
        _numOfAttackers++;
    }

    public void AttackerDestroyed()
    {
        _numOfAttackers--;
        if (_numOfAttackers <= 0 && _levelTimerFinished)
        {
            print("End level now!");
        }
    }

    public void LevelTimerFinished()
    {
        _levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        var spawners = FindObjectsOfType<AttackerSpawner>();
        foreach (var spawner in spawners)
        {
            spawner.StopSpawning();
        }
    }
}