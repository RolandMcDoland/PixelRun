﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform firePoint;

    public float bulletForce = 20f;

    // Fires bullet in the direction of the enemy
    public void ShootBullet(GameObject bulletPrefab)
    {
        if (bulletPrefab.name.Contains("Green"))
            GetComponent<Animator>().SetTrigger("GreenAttack");
        else
            GetComponent<Animator>().SetTrigger("RedAttack");

        StartCoroutine(ExecuteAfterTime(bulletPrefab, 0.25f));
    }

    IEnumerator ExecuteAfterTime(GameObject bulletPrefab, float time)
    {
        yield return new WaitForSeconds(time);

        // Reset player position (necessary due to recoil)
        transform.position = new Vector3(-7.25f, -3.6f, -2.0f);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        bullet.GetComponent<AudioSource>().Play();
    }
}
