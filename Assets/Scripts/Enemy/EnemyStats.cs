using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentDamage = enemyData.Damage;
        currentHealth = enemyData.MaxHealth;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if(currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D col)
        {
            Debug.Log("Stay collision detected");
            if(col.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player is colliding with enemy");
                PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
                player.TakeDamage(currentDamage);
            }
        }
}

