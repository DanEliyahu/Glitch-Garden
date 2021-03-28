﻿using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private GameObject gunPoint;

    private Animator _animator;
    private AttackerSpawner _myLaneSpawner;
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");


    private void Start()
    {
        _animator = GetComponent<Animator>();
        SetLaneSpawner();
    }

    private void Update()
    {
        _animator.SetBool(IsAttacking, IsAttackerInLane());
    }

    private bool IsAttackerInLane()
    {
        return _myLaneSpawner.transform.childCount > 0;
    }

    private void SetLaneSpawner()
    {
        var spawners = FindObjectsOfType<AttackerSpawner>();
        foreach (var spawner in spawners)
        {
            var isCloseEnough = Mathf.Approximately(Mathf.Floor(spawner.transform.position.y), transform.position.y);
            if (isCloseEnough)
            {
                _myLaneSpawner = spawner;
                break;
            }
        }
    }

    public void Fire()
    {
        Instantiate(projectile, gunPoint.transform.position, Quaternion.identity);
    }
}