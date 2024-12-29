using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool: GenericResourcePool<EnemyBulletController>
{
    private EnemyBulletView enemyBulletView;
    private Screecher screecher;
    public EnemyBulletPool(EnemyBulletView enemyBulletView,Screecher screecher)
    {
        this.screecher = screecher;
        this.enemyBulletView = enemyBulletView;
    }
    public EnemyBulletController GetEnemyBullet() => GetItem<EnemyBulletController>();
    protected override EnemyBulletController CreateItem<T>()
    {
        EnemyBulletController enemyBullet = new EnemyBulletController(enemyBulletView,screecher);
        return enemyBullet;
    }
}
