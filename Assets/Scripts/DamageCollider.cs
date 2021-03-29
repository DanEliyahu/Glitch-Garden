using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<HealthDisplay>().TakeDamage();
        Destroy(other.gameObject);
    }
}
