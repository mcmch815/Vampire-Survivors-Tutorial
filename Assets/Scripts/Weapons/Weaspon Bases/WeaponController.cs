using UnityEngine;
using UnityEngine.Timeline;

public class WeaponController : MonoBehaviour
{
    /// <summary>
    /// Sets standard functions for all weapon controllers.
    /// The children will be attached to the player through a controller gameobject.
    /// This makes sense because the cooldown mechanism requires persistance, i.e
    /// it should be destroyed like the actual weapons and projectiles might be. 
    /// </summary>
    // Start is called once before the first execution of Update after the MonoBehaviour is created

  
    public WeaponScriptableObject weaponData;

    float currentCooldown;

    protected PlayerMovement pm;


    protected virtual void Start()    
    {
        pm = FindAnyObjectByType<PlayerMovement>();
        currentCooldown = weaponData.CooldownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }

}
