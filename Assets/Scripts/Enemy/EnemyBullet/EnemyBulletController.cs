using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController
{
    private EnemyBulletView enemyBulletView;
    private EnemyBulletModel enemyBulletModel;
    public EnemyController enemyController;

    //private float bulletSpeed = 80f;
    //public int bulletDamage = 10;
    public EnemyBulletController(EnemyBulletView enemyBulletView, EnemyController enemyController)
    {
        this.enemyController = enemyController;
        if (enemyBulletView == null)
        {
            Debug.Log("Enemy bullet view is null");
        }
        this.enemyBulletModel = new EnemyBulletModel();
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
        rb.velocity = direction * this.enemyBulletModel.bulletSpeed;
    }
    public void HandleCollision()
    {
        enemyBulletView.gameObject.SetActive(false);
        enemyController.ReturnBulletToPool(this);
    }
    public int GetBulletDamage()
    {
        return this.enemyBulletModel.bulletDamage;
    }
}
