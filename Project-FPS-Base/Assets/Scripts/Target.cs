using Assets.GameSystem.Scripts;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float _point = 1f;
    [SerializeField]
    private TargetType _targetType = TargetType.None;

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

            Destroy(gameObject);
        }
    }

    private void DestroyTarget()
    {

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
