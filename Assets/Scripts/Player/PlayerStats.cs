using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    float currentHealth;
    float currentMight;
    float currentSpeed;
    float currentRecovery;
    float currentProjectileSpeed;

    void Awake()
    {
        currentHealth = characterData.MaxHealth;
        currentMight = characterData.Might;
        currentSpeed = characterData.MoveSpeed;
        currentRecovery = characterData.Recovery;
        currentProjectileSpeed = characterData.ProjectileSpeed;
    }
  
}
