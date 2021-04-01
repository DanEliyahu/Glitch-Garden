using UnityEngine;

public class Attacker : MonoBehaviour
{
    private float _currentSpeed = 1f;
    private GameObject _currentTarget;
    private Animator _animator;
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

    private void Awake()
    {
        FindObjectOfType<LevelController>().AttackerSpawned();
    }

    private void OnDestroy()
    {
        var levelController = FindObjectOfType<LevelController>();
        if (levelController != null)
        {
            levelController.AttackerDestroyed();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * (_currentSpeed * Time.deltaTime));
        if (!_currentTarget)
        {
            _animator.SetBool(IsAttacking,false);
        }
    }

    public void SetMovementSpeed(float speed)
    {
        _currentSpeed = speed;
    }

    public void Attack(GameObject target)
    {
        _animator.SetBool(IsAttacking,true);
        _currentTarget = target;
    }

    public void StrikeCurrentTarget(float damage)
    {
        if (!_currentTarget)
        {
            return;
        }

        var health = _currentTarget.GetComponent<Health>();
        if (health)
        {
            health.DealDamage(damage);
        }
    }
}