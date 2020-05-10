using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float damage = 1f;
    public float health = 5f;

    public float attackTimeout = 1;
    private float attackTimer = 0;

    void OnCollisionStay2D(Collision2D collision)
    {
        // Once a second
        if (Time.time > attackTimer)
        {
            // When enemy is in collision deal damage
            if (collision.gameObject.tag == "Enemy")
            {
                health -= damage;

                // Player is dead
                if(health <= 0)
                {
                    Destroy(gameObject);
                }

                attackTimer = Time.time + attackTimeout;
            }
        }
    }
}
