using UnityEngine;

[DisallowMultipleComponent]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int _Health;
    [SerializeField]
    private int _MaxHealth = 100;
    public int CurrentHealth { get => _Health; private set => _Health = value; }
    public int MaxHealth { get => _MaxHealth; private set => _MaxHealth = value; }

    public event IDamageable.TakeDamageEvent onTakeDamage;
    public event IDamageable.DeathEvent onDeath;

    private void OnEnable()
    {
        _Health = MaxHealth;
    }

    public void TakeDamage(int Damage)
    {
        int damageTaken = Mathf.Clamp(Damage, 0, CurrentHealth);

        CurrentHealth -= damageTaken;

        if (damageTaken != 0)
        {
            onTakeDamage?.Invoke(damageTaken);
        }

        if (CurrentHealth == 0 && damageTaken != 0)
        {
            onDeath?.Invoke(transform.position);
        }
    }
}