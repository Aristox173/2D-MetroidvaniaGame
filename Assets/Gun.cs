using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 1;
    public float fireRate = 0.1f; // Time interval between bullet spawns

    private void Start()
    {
        InvokeRepeating(nameof(FireBullet), 0, fireRate); // Call FireBullet() method repeatedly with the specified fireRate
    }

    private void FireBullet()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;
    }
}