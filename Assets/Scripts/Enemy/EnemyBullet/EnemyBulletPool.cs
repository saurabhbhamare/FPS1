public class EnemyBulletPool : GenericResourcePool<EnemyBulletController>
{
    private EnemyBulletView enemyBulletView;
    private EnemyController enemyController;
    public EnemyBulletPool(EnemyBulletView enemyBulletView, EnemyController enemyController)
    {
        this.enemyController = enemyController;
        this.enemyBulletView = enemyBulletView;
    }
    public EnemyBulletController GetEnemyBullet() => GetItem<EnemyBulletController>();
    protected override EnemyBulletController CreateItem<T>()
    {
        EnemyBulletController enemyBullet = new EnemyBulletController(enemyBulletView, enemyController);
        return enemyBullet;
    }
}
