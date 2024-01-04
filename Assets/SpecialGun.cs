using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 1;

    public void FireBullet()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;

        Debug.Log("Bullet Fired!"); // Add a debug message to verify if the method is called
    }
}
