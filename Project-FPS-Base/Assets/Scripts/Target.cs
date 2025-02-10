using Assets.GameSystem.Scripts;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private TargetType _targetType = TargetType.None;
    [SerializeField]
    private Collectable _collectable = null;

    private Health _health = null;
    private ScoreManager _scoreManager = null;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        _scoreManager = ScoreManager.Instance;
    }

    private void IsDie(bool isDie)
    {
        if (isDie)
        {
            _scoreManager.AddBonus(_targetType);
            DestroyTarget();
            Destroy(gameObject);
        }
    }

    private void DestroyTarget()
    {
        //Spawn collectables
        for (int i = 0; i < 3; i++)
        {
            Instantiate(_collectable, transform.position, Quaternion.identity);
        }
    }

    private void OnEnable()
    {
        _health.IsDie += IsDie;
    }

    private void OnDisable()
    {
        _health.IsDie -= IsDie;
    }

    public enum TargetType
    {
        None,
        Green,
        Blue,
        Red,
        Yellow
    }
}
