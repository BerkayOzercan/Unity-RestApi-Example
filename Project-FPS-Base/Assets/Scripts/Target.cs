using UnityEngine;

[RequireComponent(typeof(Health))]
public class Target : MonoBehaviour
{
    [SerializeField]
    private float _point = 1f;

    private Health _health = null;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void IsDie(bool isDie)
    {
        if (isDie)
        {
            GameManager.Instance.AddScore(_point);

            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        _health.IsDie += IsDie;
    }
}
