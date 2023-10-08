using UnityEngine;

public class EnemyHealth: MonoBehaviour, IDamageable
{
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;

    private UIController uiController;
    private bool isDead = false;

    private void Awake()
    {
        health = maxHealth;

    }
    private void Start()
    {
        uiController = UIController.Instance;
    }

    private void Update()
    {
        if (health <= 0 && !isDead)
        {
            uiController.DecreaseEnemiesToKill();
            gameObject.SetActive(false);
            isDead = true;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }
}
