using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    float currentHealth;
    float currentMight;
    float currentSpeed;
    float currentRecovery;
    float currentProjectileSpeed;

    //Exerperience and level of player
    [Header("Experience and Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    [Header("I-Frames")]
    public float invincibilityDuration;
    public float invincibilityTimer;
    bool isInvincible;


    public List<LevelRange> levelRanges;
    
    void Awake()
    {
        currentHealth = characterData.MaxHealth;
        currentMight = characterData.Might;
        currentSpeed = characterData.MoveSpeed;
        currentRecovery = characterData.Recovery;
        currentProjectileSpeed = characterData.ProjectileSpeed;
    }

    void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    void Update()
    {
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if(isInvincible)
        {
            isInvincible = false;
        }
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            int experienceCapIncrease = 0;
            foreach (LevelRange levelRange in levelRanges)
            {
                if (level >= levelRange.startLevel && level <= levelRange.endLevel)
                {
                    experienceCapIncrease = levelRange.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player has taken damage");
        if(!isInvincible)
        {   
            // Reset invincibility timer
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            
        
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }
  
    public void Kill()
    {
        Debug.Log("Player has died");
    }

     // Method to heal the player but cannot exceed the max health  
    public void Heal(float amount)
    {
        
        if(currentHealth < characterData.MaxHealth)
        {
            if(currentHealth + amount > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
            else
            {
                currentHealth += amount;
            }
          
        }
    }
  

}
