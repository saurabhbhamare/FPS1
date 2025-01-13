public class EnemyModel 
{
    public int enemyHealth;
    public int attackStrength;
    public float nextAttackTime;
    public float attackInterval;
    public float zigzagAmplitude;
    public float zigzagFrequency;
    public float hoverHeight;

    public EnemyModel(EnemySO enemySO)
    {
        enemyHealth = enemySO.MaxHealth;
        attackStrength = enemySO.attackStrength;
        attackInterval = enemySO.AttackInterval;
        zigzagAmplitude = enemySO.ZigZagAmplitude;
        zigzagFrequency = enemySO.ZigZagFrequency;
        hoverHeight = enemySO.HoverHeight;
        nextAttackTime = 0f;
    }
}
