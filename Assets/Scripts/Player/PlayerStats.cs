using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    CharacterScriptableObject characterData;

    //Current stats of player    
    public float currentHealth;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentMagnet;
   

    //Spawn weapon
    public List<GameObject> spawnedWeapons;

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
        characterData = CharacterSelector.GetData();

        // // No longer need the singleton
        CharacterSelector.instance.DestroySingleton();
       

        // Assign thhe variables
        currentHealth = characterData.MaxHealth;
        currentMight = characterData.Might;
        currentMoveSpeed = characterData.MoveSpeed;
        currentRecovery = characterData.Recovery;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;
       


        // Spawn starting weapon
        SpawnWeapon(characterData.StartingWeapon);
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

        Recover();
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
  
    void Recover()
    {
        if(currentHealth < characterData.MaxHealth)
        {
            if(currentHealth + currentRecovery * Time.deltaTime > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
            else
            {
            currentHealth += currentRecovery * Time.deltaTime;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        //Make it a child of the object that spawned it
        spawnedWeapon.transform.SetParent(transform);
        spawnedWeapons.Add(spawnedWeapon);
    }
    

}

