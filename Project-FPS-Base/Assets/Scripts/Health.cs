using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 10;
    public int CurrentHealth { get; set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= damage;
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);

    }
}
