using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private GameObject gunPoint;

    public void Fire()
    {
        Instantiate(projectile, gunPoint.transform.position, Quaternion.identity);
    }
}