using UnityEngine;

public class EnemyBulletView : MonoBehaviour
{
    private EnemyBulletController enemyBulletController;
    public void SetBulletController(EnemyBulletController enemyBulletController)
    {
        this.enemyBulletController = enemyBulletController;
    }
    private void OnCollisionEnter(Collision collision)
    {
        enemyBulletController.HandleCollision();
    }
    private void OnTriggerEnter(Collider other)
    {
        enemyBulletController.HandleCollision();
    }
    public EnemyBulletController GetEnemyBulletController()
    {
        return this.enemyBulletController;
    }
}
