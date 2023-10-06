using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] Image foreground;

    private void Update()
    {
        foreground.fillAmount = enemyHealth.health / enemyHealth.maxHealth;
    }
}
