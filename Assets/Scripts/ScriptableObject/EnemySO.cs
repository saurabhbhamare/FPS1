using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/EnemyScriptableObject")]
public class EnemySO : ScriptableObject
{
    [Header("Common Properties")]
    public float MovementSpeed;
    public EnemyView EnemyPrefab;
    public EnemyType EnemyType;
    public Vector3 SpawnPosition;
    public Vector3 SpawnRotation;
    public int MaxHealth;
    public int attackStrength;

    [Header("Screecher Properties")]
    public float ZigZagAmplitude;
    public float ZigZagFrequency;
    public float HoverHeight;
    public float AttackInterval;
}
