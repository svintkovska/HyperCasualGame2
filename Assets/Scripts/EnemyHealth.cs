using UnityEngine;

public class EnemyHealth: MonoBehaviour, IDamageable
{
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;

    private bool isDead = false;

    private void Awake()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0 && !isDead)
        {
            gameObject.SetActive(false);
            isDead = true;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }
}
