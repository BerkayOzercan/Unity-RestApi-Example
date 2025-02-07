using Assets.GameSystem.Scripts;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Target : MonoBehaviour
{
    [SerializeField]
    private float _point = 1f;
    [SerializeField]
    private Health _health = null;

    private void IsDie(bool isDie)
    {
        if (isDie)
        {
            ScoreManager.Instance.AddScore(_point);

            Destroy(gameObject);
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
}
