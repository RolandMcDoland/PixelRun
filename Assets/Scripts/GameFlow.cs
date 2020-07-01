using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public float timeBetweenEnemySpawns = 10.0f;
    private float timerBetweenEnemySpawns;

    public int enemiesToKill = 1;
    private int enemyCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        instantiateRandomEnemy();

        timerBetweenEnemySpawns = timeBetweenEnemySpawns;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the time between enemy spawns is up and spawn new ones if not
        timerBetweenEnemySpawns -= Time.deltaTime;
        if (timerBetweenEnemySpawns < 0)
        {
            GameObject enemy = instantiateRandomEnemy();

            // Check if enough enemies have been killed to up the difficulty
            enemyCounter++;
            if (enemyCounter == enemiesToKill)
            {
                // Reduce time between spawns until you reach minimum then up the enemy's speed
                if (timeBetweenEnemySpawns > 5.0f)
                {
                    timeBetweenEnemySpawns -= 1.0f;
                    enemiesToKill *= 2;
                }
                else
                {
                    enemy.GetComponent<EnemyBehaviour>().speed += 0.01f;
                }
            }

            timerBetweenEnemySpawns = timeBetweenEnemySpawns;
        }
    }

    // Chooses a random enemy and spawns it
    GameObject instantiateRandomEnemy()
    {
        int index = Random.Range(0, 2);

        return Instantiate(enemyPrefabs[index], new Vector3(12f, -3.6f, -2f), Quaternion.identity);
    }
}
