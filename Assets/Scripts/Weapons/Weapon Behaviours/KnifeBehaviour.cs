using UnityEngine;

/// <summary>
/// Attached to weapon prefab, not the player. 
/// </summary>

public class KnifeBehaviour :ProjectileWeasponBehaviour
{
    
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * weaponData.Speed * Time.deltaTime;
    }
}
