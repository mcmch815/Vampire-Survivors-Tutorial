using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : MeleeWeasponBehaviour
{   
    List<GameObject> markedEnemies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        // not calling base, because we need completely different behaviour
        if(col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);

            markedEnemies.Add(col.gameObject);
        }
          else if(col.CompareTag("Breakable"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable)  && !markedEnemies.Contains(col.gameObject))
            {
                breakable.TakeDamage(currentDamage);
                markedEnemies.Add(col.gameObject);
                
            }
            
        }
    }



}
