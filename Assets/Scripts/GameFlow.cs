using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int numberOfEnemies = 1;

    // Update is called once per frame
    void Update()
    {
        // Check if there are enough enemies and spawn new ones if not
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < numberOfEnemies)
        {
            Instantiate(enemyPrefab, new Vector3(12f, -3f, 0f), Quaternion.identity);
        }
    }
}
