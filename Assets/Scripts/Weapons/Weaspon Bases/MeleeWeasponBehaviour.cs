using UnityEngine;

/// <summary>
/// Base script of all melee behaviours [To be placed on preab of a weaspon that is melee]
/// </summary>
public class MeleeWeasponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    public float destroyAfterSeconds;
    
    //Current Stats

    protected float currentDamage;
    protected float currenSpeed;
    protected float currentCooldownDuration;
    protected float currentPierce;
    
    protected virtual void Awake()
    {
        currentDamage = weaponData.Damage;
        currenSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }
        else if(col.CompareTag("Breakable"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
                
            }
            
        }
    }


}
