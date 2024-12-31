using UnityEngine.AI;

public class Larvae : Enemy
{
    private NavMeshAgent navmeshAgent;
    private int attackStrength = 5;
    private void Awake()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = 100;
        currentHealth = enemyHealth;
        speed = navmeshAgent.speed;
    }
    protected override void FollowPlayer()
    {
        navmeshAgent.SetDestination(playerTransform.position);
        navmeshAgent.updateRotation = true;
    }
    public int GetAttackStrength()
    {
        return attackStrength;
    }
}
