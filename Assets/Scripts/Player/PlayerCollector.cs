using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    //Collider used to dedect objexts within a certain range
    CircleCollider2D playerCollider;
    public float pullSpeed;

    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
        playerCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        playerCollider.radius = player.currentMagnet;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
  
        if(other.gameObject.TryGetComponent(out ICollectible collectible))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if(rb != null)
            {   
                //TODO: Make the pickups follow the player and not picked up until they reach the player
                Vector2 direction = (transform.position - other.transform.position).normalized;
                rb.AddForce(direction * pullSpeed);
            }
            collectible.Collect();
        }
    }



}
