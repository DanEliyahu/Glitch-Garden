using UnityEngine;

public class Ghost : MonoBehaviour
{
    private bool _canCollide = true;
    private static readonly int Vanish = Animator.StringToHash("Vanish");

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<Defender>())
        {
            GetComponent<Animator>().SetTrigger(Vanish);
            _canCollide = false;
        }
    }

    public bool IsVanished()
    {
        return !_canCollide;
    }

    public void SetCanCollide()
    {
        _canCollide = true;
    }
}
