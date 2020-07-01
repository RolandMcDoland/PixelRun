using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed = 0.02f;
    public float attackSpeed = 0.001f;
    private TextMeshProUGUI score;

    void Start()
    {
        var obj = GameObject.FindObjectsOfType<TextMeshProUGUI>();
        foreach (var o in obj)
        {
            if (o.name.Equals("Score"))
            {
                score = o;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards player
        Vector3 newPosition = transform.position;
        newPosition.x -= speed;
        transform.position = newPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // When a bullet hit the enemy destroy the bullet
        if (collision.gameObject.name != "Player" && collision.gameObject.tag != "Enemy")
        {
            // Play audio from one of the circles (after being destroyed the audio won't play from enemy nor bullet)
            GameObject.Find("5").GetComponent<AudioSource>().Play();

            // If the bullet is the colour of the enemy destroy enemy
            if (gameObject.name.Contains(collision.gameObject.tag))
            {
                //Destroy(gameObject);
                var s = Convert.ToInt32(score.text);
                if (gameObject.tag.Equals("StrongEnemy"))
                    s += 20;
                else
                    s += 10;
                score.text = s.ToString();

                speed = 0f;
                gameObject.GetComponent<Animator>().SetTrigger("Death");
                Destroy(collision.gameObject);
            }
            else
            {
                collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                collision.gameObject.GetComponent<Animator>().SetTrigger("Wrong");
            }

            //Destroy(collision.gameObject);
        }
        // When its the player stop moving
        else
            speed = attackSpeed;
    }
}
