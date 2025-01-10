using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel 
{
    public int enemyHealth;
    public int attackStrength;
    public float nextAttackTime;
    public float attackInterval;
    public EnemyModel(EnemySO enemySO)
    {
        enemyHealth = enemySO.MaxHealth;
        attackStrength = enemySO.attackStrength;
        nextAttackTime = 0f;
        attackInterval = 3f;
    }
}
