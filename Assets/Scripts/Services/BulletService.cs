using System.Collections.Generic;
using UnityEngine;

public class BulletService
{
    private Transform playerTransform;
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private GameObject enemyBulletPrefab;
    private int playerPoolSize = 10;
    private int enemyPoolSize = 30;

    private Queue<Bullet> playerBulletPool = new Queue<Bullet>();
    private Queue<Bullet> enemyBulletPool = new Queue<Bullet>();

    public BulletService(GameObject playerBulletPrefab, GameObject enemyBulletPrefab, Transform playerTransform)
    {
        this.playerBulletPrefab = playerBulletPrefab;
        this.enemyBulletPrefab = enemyBulletPrefab;
        this.playerTransform = playerTransform;
        InitializePoolForPlayerAndEnemy();
    }
    private void InitializePoolForPlayerAndEnemy()
    {
        InitializePool(playerBulletPrefab, playerBulletPool, playerPoolSize);
        InitializePool(enemyBulletPrefab, enemyBulletPool, enemyPoolSize);
    }
    public void InitializePool(GameObject bulletPrefab, Queue<Bullet> bulletPool, int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            Bullet bullet = Object.Instantiate(bulletPrefab).GetComponent<Bullet>();
            bullet.gameObject.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }
    public void FireBullet(Vector3 position, Quaternion rotation, BulletType bulletType)
    {

        Bullet bullet = GetBullet(bulletType);
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.SetBulletService(this);
        if (bulletType == BulletType.ENEMY)
        {
            Vector3 directionToPlayer = (playerTransform.position - position).normalized;
            bullet.transform.rotation = Quaternion.LookRotation(directionToPlayer);
        }
        bullet.bulletType = bulletType;
        bullet.gameObject.SetActive(true);
    }
    private Bullet GetBullet(BulletType bulletType)
    {
        Queue<Bullet> selectedPool = null;
        if (bulletType == BulletType.PLAYER)
        {
            selectedPool = playerBulletPool;
        }
        else
        {
            selectedPool = enemyBulletPool;
        }
        if (selectedPool.Count > 0)
        {
            return selectedPool.Dequeue();
        }
        else
        {
            GameObject bulletPrefab = bulletType == BulletType.PLAYER ? playerBulletPrefab : enemyBulletPrefab;
            Bullet newBullet = Object.Instantiate(bulletPrefab).GetComponent<Bullet>();
            return newBullet;
        }
    }
    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        if (bullet.bulletType == BulletType.PLAYER)
        {
            playerBulletPool.Enqueue(bullet);
        }
        else
        {
            enemyBulletPool.Enqueue(bullet);
        }
    }
}
