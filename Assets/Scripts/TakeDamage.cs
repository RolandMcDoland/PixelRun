using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    //public float damage = 1f;
    //public float health = 5f;

    //public float attackTimeout = 1;
    //private float attackTimer = 0;

    public int healthNumber = 3;
    public int health = 3;
    public GameObject[] hearth;

    private GameObject ui;
    private GameObject endScreen;

    private void Start()
    {
        ui = GameObject.Find("UI");
        endScreen = GameObject.Find("EndScreen");
        ui.SetActive(true);
        endScreen.SetActive(false);

        health = healthNumber;
        for (int i = 0; i < healthNumber; i++)
        {
            hearth[i].SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Enemy"))
        {
            health -= 1;

            for (int i = 0; i < healthNumber; i++)
            {
                if (i < health)
                {
                    hearth[i].SetActive(true);
                }
                else
                {
                    hearth[i].SetActive(false);
                }

            }

            Destroy(collision.gameObject);

            if (health <= 0)
            {
                ui.SetActive(true);
                endScreen.SetActive(true);

                var fobj = GameObject.Find("FinalScore");
                var fscore = fobj.GetComponent<TextMeshProUGUI>();
                var obj = GameObject.Find("Score");
                var score = obj.GetComponent<TextMeshProUGUI>();
                fscore.text = score.text;

                ui.SetActive(false);
                endScreen.SetActive(true);

                StopGame();
            }
        }
    }

    private void StopGame()
    {
        gameObject.GetComponent<DrawLine>().enabled = false;
        gameObject.GetComponent<GameFlow>().enabled = false;

        var enemies = GameObject.FindGameObjectsWithTag("WeakEnemy");
        foreach (var e in enemies)
        {
            e.GetComponent<EnemyBehaviour>().enabled = false;
        }
        var enemies2 = GameObject.FindGameObjectsWithTag("StrongEnemy");
        foreach (var e in enemies2)
        {
            e.GetComponent<EnemyBehaviour>().enabled = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        
        //// Once a second
        //if (Time.time > attackTimer)
        //{
        //    // When enemy is in collision deal damage
        //    if (collision.gameObject.tag == "Enemy")
        //    {
        //        health -= damage;

        //        // Player is dead
        //        if(health <= 0)
        //        {
        //            Destroy(gameObject);
        //        }

        //        attackTimer = Time.time + attackTimeout;
        //    }
        //    Debug.Log("kolizja");
        //}
    }
}
