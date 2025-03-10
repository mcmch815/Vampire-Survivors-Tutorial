using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    float currentHealth;
    float currentMight;
    float currentSpeed;
    float currentRecovery;
    float currentProjectileSpeed;

    //Experience and level of the player
    [Header("Experience and Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int experienceCapIncrease;


    void Awake()
    {
        currentHealth = characterData.MaxHealth;
        currentMight = characterData.Might;
        currentSpeed = characterData.MoveSpeed;
        currentRecovery = characterData.Recovery;
        currentProjectileSpeed = characterData.ProjectileSpeed;
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if(experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            experienceCap += experienceCapIncrease;
        }
    }
  
}
