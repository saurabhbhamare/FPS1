using System.Collections.Generic;
using UnityEngine;

public class BulletService
{
    [SerializeField] private GameObject playerBulletPrefab;
    //[SerializeField] private GameObject enemyBulletPrefab;
    private int poolSize = 10;

    private Queue<BulletView> playerBulletPool = new Queue<BulletView>();
    private Queue<BulletView> enemyBulletPool = new Queue<BulletView>();

    public BulletService(GameObject playerBulletPrefab)
    {
        this.playerBulletPrefab = playerBulletPrefab;
        InitializePoolForPlayerAndEnemy();
    }
    private void InitializePoolForPlayerAndEnemy()
    {
        InitializePool(playerBulletPrefab,playerBulletPool,poolSize);
       // InitializePool();
    }
    public void InitializePool(GameObject bulletPrefab, Queue<BulletView> bulletPool, int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            BulletView bullet = Object.Instantiate(bulletPrefab).GetComponent<BulletView>();
            bullet.gameObject.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }
    public void FireBullet(Vector3 position, Quaternion rotation)
    {
        BulletView bullet = GetBullet();
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.SetBulletService(this);
        bullet.gameObject.SetActive(true);  
    }
    private BulletView GetBullet()
    {
        if (playerBulletPool.Count > 0)
        {
            return playerBulletPool.Dequeue();
        }
        else
        {
            BulletView newBullet = Object.Instantiate(playerBulletPrefab).GetComponent<BulletView>();
            return newBullet;
        }
    }
    public void ReturnBullet(BulletView bullet)
    {
        bullet.gameObject.SetActive(false);
        playerBulletPool.Enqueue(bullet);
    }
}
