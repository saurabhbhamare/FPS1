using UnityEngine;
public class EnemyController
{
    protected EnemySO enemySO;
    protected EnemyView enemyView;
    public Transform targetTransform;
    private EnemyModel enemyModel;
    public EnemyController(EnemySO enemySO, Transform playerTransform)
    {
        this.enemySO = enemySO;
        this.targetTransform = playerTransform;
        this.enemyModel = new EnemyModel(enemySO);
        InitializeView();
    }
    private void InitializeView()
    {
        enemyView = Object.Instantiate(enemySO.EnemyPrefab);
    }
    public EnemyModel GetEnemeyModel()
    {
        return this.enemyModel;
    }
    public int GetAttackStrength()
    {
        return this.enemyModel.attackStrength;
    }
    protected void TakeDamage(int damage)
    {
        this.enemyModel.enemyHealth -= damage;
    }
    protected void UpdateHealthBarUI()
    {
        this.enemyView.GetHealthBarUIImage().fillAmount = (float)this.enemyModel.enemyHealth / 100;
    }
    public void HandleCollision(Collider collider)
    {
        if (collider.GetComponent<PlayerBulletView>())
        {
            int damage = collider.GetComponent<PlayerBulletView>().playerBulletController.GetPlayerBulletModel().bulletDamage;
            TakeDamage(damage);
            UpdateHealthBarUI();
            CheckIfDead();
        }
    }
    protected void CheckIfDead()
    {
        if(enemyModel.enemyHealth<=0)
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
