using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/EnemyScriptableObject")]
public class EnemySO : ScriptableObject
{
    public EnemyView EnemyPrefab;
    public EnemyType EnemyType;
    public Vector3 SpawnPosition;
    public Vector3 SpawnRotation;
    public float MovementSpeed;
    public int MaxHealth;
    public EnemyBulletView enemyBulletView;
    public int attackStrength;
}
