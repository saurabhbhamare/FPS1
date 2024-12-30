using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController
{
    private EnemyBulletView enemyBulletView;
    public Screecher screecher;
    private float bulletSpeed = 80f;
    public int bulletDamage = 10;
    public EnemyBulletController(EnemyBulletView enemyBulletView, Screecher screecher)
    {
        this.screecher = screecher;
        if (enemyBulletView == null)
        {
            Debug.Log("Enemy bullet view is null");
        }
        this.enemyBulletView = Object.Instantiate(enemyBulletView);
        this.enemyBulletView.SetBulletController(this);
    }
    public void ConfigureBullet(Transform firePoint, Transform playerPosition)
    {
        enemyBulletView.gameObject.SetActive(true);
        enemyBulletView.transform.position = firePoint.transform.position;
        Vector3 direction = (playerPosition.position - firePoint.position).normalized;
        enemyBulletView.transform.rotation = Quaternion.LookRotation(direction);
        Rigidbody rb = enemyBulletView.GetComponent<Rigidbody>();
        rb.velocity = direction * bulletSpeed;
    }
    public void HandleCollision()
    {
        enemyBulletView.gameObject.SetActive(false);
        screecher.ReturnBulletToPool(this);
    }
}
