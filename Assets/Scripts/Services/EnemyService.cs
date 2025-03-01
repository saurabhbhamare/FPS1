using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyService
{
    private Transform playerTransform;
    private List<EnemySO> enemySOList;

    private List<EnemyController> levelEnemies;
    public EnemyService(List<EnemySO> enemySOList, Transform playerTransform)
    {
        this.enemySOList = enemySOList;
        this.playerTransform = playerTransform;
        InitializeEnemies();
        SpawnEnemies();
    }

    private void InitializeEnemies()
    {
        levelEnemies = new List<EnemyController>();
    }
    public void SpawnEnemies()
    {
        foreach (EnemySO enemySO in enemySOList)
        {
            EnemyController enemyController = CreateEnemy(enemySO);
            levelEnemies.Add(enemyController);
        }
    }
    public EnemyController CreateEnemy(EnemySO enemySO)
    {
        EnemyController enemy;
        switch (enemySO.EnemyType)
        {
            case EnemyType.LARVAE:
                enemy = new LarvaeController(enemySO, playerTransform);
                break;
            case EnemyType.SCREECHER:
                enemy = new ScreecherController(enemySO, playerTransform);
                break;
            default:
                enemy = new EnemyController(enemySO, playerTransform);
                break;
        }
        return enemy;
    }
}
