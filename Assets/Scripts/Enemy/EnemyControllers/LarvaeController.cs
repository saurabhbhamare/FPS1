using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LarvaeController : EnemyController
{
    public LarvaeController(EnemySO enemySO, Transform playerTransform) : base(enemySO, playerTransform)
    {
        enemyView.SetEnemyController(this);
        NavMeshAgent navMeshAgent = enemyView.GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;  // Disable the agent to set the initial position
        }

        enemyView.gameObject.transform.position = enemySO.SpawnPosition;
        enemyView.transform.rotation = Quaternion.Euler(enemySO.SpawnRotation);

        // Re-enable NavMeshAgent and set its position explicitly
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = true;
            navMeshAgent.Warp(enemySO.SpawnPosition);  // Use Warp to set the position accurately
        }
    }
    public override void Movement()
    {
        this.enemyView.navmeshAgent.SetDestination(targetTransform.position);
        this.enemyView.navmeshAgent.updateRotation = true;
    }
}
