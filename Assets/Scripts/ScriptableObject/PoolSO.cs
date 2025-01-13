using UnityEngine;
[CreateAssetMenu(fileName = "PoolScriptableObject", menuName = "ScriptableObjects/PoolScriptableObject")]
public class PoolSO : ScriptableObject
{
    public GameObject PlayerBulletPrefab;
    public GameObject EnemyBulletPrefab;
    public int PlayerBulletPoolSize;
    public int EnemyBulletPoolSize;
}
