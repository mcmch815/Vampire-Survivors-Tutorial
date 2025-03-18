using UnityEngine;

public class HealthPotion : MonoBehaviour, ICollectible
{
    public float healthAmount = 10;

    public void Collect()
    {
        PlayerStats player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        player.Heal(healthAmount);
        Destroy(gameObject);
    }
 
}


