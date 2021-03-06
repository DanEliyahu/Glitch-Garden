using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Range(0f, 20f)] [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float spinSpeed = 1f;
    [SerializeField] private float damage = 50f;
    private bool _canCollide = true;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * (moveSpeed * Time.deltaTime),Space.World);
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var health = other.GetComponent<Health>();
        var attacker = other.GetComponent<Attacker>();
        var ghost = other.GetComponent<Ghost>();

        if (ghost && ghost.IsVanished()) // no collision
        {
            return;
        }
        if (attacker && health && _canCollide)
        {
            _canCollide = false;
            health.DealDamage(damage);
            Destroy(gameObject);
        }
    }
}