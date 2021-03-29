using UnityEngine;

public class Fox : MonoBehaviour
{
    private static readonly int Jump = Animator.StringToHash("Jump");

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var otherObject = otherCollider.gameObject;
        if (otherCollider.GetComponent<Gravestone>())
        {
            GetComponent<Animator>().SetTrigger(Jump);
            return;
        }

        if (otherCollider.GetComponent<Defender>())
        {
            GetComponent<Attacker>().Attack(otherObject);
        }
    }
}
