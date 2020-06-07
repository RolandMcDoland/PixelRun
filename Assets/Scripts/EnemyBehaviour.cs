using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed = 0.02f;
    public float attackSpeed = 0.001f;

    // Update is called once per frame
    void Update()
    {
        // Move towards player
        Vector3 newPosition = transform.position;
        newPosition.x -= speed;
        transform.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When a bullet hit the enemy destroy both
        if (collision.gameObject.name != "Player" && collision.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        // When its the player stop moving
        else
            speed = attackSpeed;
    }
}
