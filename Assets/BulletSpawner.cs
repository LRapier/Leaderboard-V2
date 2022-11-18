using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public PlayerController player;
    public float fireRate = 3f;
    public GameObject bulletPrefab;
    public Transform[] bulletSpawns;
    public float bulletDelayTime = 0f;
    public float fireRateDelayTime = 0f;
    public static BulletSpawner instance;

    void Awake() { instance = this; }

    void Update()
    {
        if (!player.isPlaying)
            return;

        bulletDelayTime += Time.deltaTime;
        while (bulletDelayTime >= fireRate)
        {
            SpawnBullet();
            bulletDelayTime -= fireRate;
        }

        fireRateDelayTime += Time.deltaTime;
        while (fireRateDelayTime >= 10)
        {
            FireRateUp();
            fireRateDelayTime -= 10;
        }
    }

    void FireRateUp()
    {
        fireRate = fireRate / 2;
    }

    void SpawnBullet()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawns[Random.Range(0, bulletSpawns.Length)].position, Quaternion.identity);
    }
}
