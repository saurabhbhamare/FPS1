using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController
{
    protected EnemySO enemySO;
    protected EnemyView enemyView;
    public Transform targetTransform;
    public int enemyHealth;
    public int attackStrength;
    public EnemyController(EnemySO enemySO, Transform playerTransform)
    {
        this.enemySO = enemySO;
        this.targetTransform = playerTransform;
        this.enemyHealth = enemySO.MaxHealth;
        this.attackStrength = enemySO.attackStrength;
        InitializeView();
    }
    private void InitializeView()
    {
        enemyView = Object.Instantiate(enemySO.EnemyPrefab);
    }
    public int GetAttackStrength()
    {
        return attackStrength;
    }
    protected void TakeDamage(int damage)
    {
        this.enemyHealth -= damage;
    }
    protected void UpdateHealthBarUI()
    {
        this.enemyView.GetHealthBarUIImage().fillAmount = (float)enemyHealth / 100;
    }
    public void HandleCollision(Collider collider)
    {
        if (collider.GetComponent<PlayerBulletView>())
        {
            int damage = collider.GetComponent<PlayerBulletView>().playerBulletController.bulletDamage;
            TakeDamage(damage);
            UpdateHealthBarUI();
            CheckIfDead();
        }
    }
    protected void CheckIfDead()
    {
        if (this.enemyHealth <= 0)
        {
            EnemyDied();
        }
    }
    public void EnemyDied()
    {
        GameObject.Destroy(enemyView.gameObject);
    }

    public virtual void Movement() { }
    public virtual void Attack() { }
    public virtual void ReturnBulletToPool(EnemyBulletController bulletToReturn) { }
}
