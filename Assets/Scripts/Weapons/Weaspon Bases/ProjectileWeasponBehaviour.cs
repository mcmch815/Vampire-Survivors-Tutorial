using UnityEngine;

/// <summary>
/// Base Script of all projectile behaviours [To be placed on a prefab of a weaspon that is a projectile]
/// </summary>

public class ProjectileWeasponBehaviour : MonoBehaviour
{

    public WeaponScriptableObject weaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;

    //Current Stats
    protected float currentDamage, currentSpeed, currentCooldownDuration, currentPierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
        float dirx = direction.x;
        float diry = direction.y;
        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        //if firing left
        if (dirx < 0 && diry == 0)
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -45f;
        }
        //if firing right
        else if (dirx > 0 && diry == 0)
        {
            rotation.z = -45f;
        }
        //if firing up
        else if (dirx == 0 && diry > 0)
        {
            rotation.z = 45f;
        }
        //if firing down
        else if (dirx == 0 && diry < 0)
        {
            rotation.z = -135f;
        }
        //if firing diagonally up-right
        else if (dirx > 0 && diry > 0)
        {
            rotation.z = 0f;
        }
        //if firing diagonally up-left
        else if (dirx < 0 && diry > 0)
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }
        //if firing diagonally down-right
        else if (dirx > 0 && diry < 0)
        {
            rotation.z = -90f;
        }
        //if firing diagonally down-left
        else if (dirx < 0 && diry < 0)
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
        }

    transform.localScale = scale;
    transform.rotation = Quaternion.Euler(rotation);

    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            ReducePierce();
        }
    }

    void ReducePierce()
    {
        currentPierce--;
        if(currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
