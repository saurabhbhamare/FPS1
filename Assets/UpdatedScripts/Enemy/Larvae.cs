using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Larvae : MonoBehaviour
{
    public Transform playerTransform;
    private NavMeshAgent navmeshAgent;
    private int attackStrength = 5;
    private int enemyHealth = 100;
    [SerializeField] private Image healthBarImage;
    private void Awake()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        navmeshAgent.SetDestination(playerTransform.position);
        navmeshAgent.updateRotation = true;
    }
    private void HandleDamage(int damage)
    {
        enemyHealth -= damage;
        UpdateEnemeyHealthBarAfterTakingDamage();
        if (this.enemyHealth < 1)
        {
            UIService.Instance.EnemyKilledMessage("You killed the larvae, eliminating a dangerous threat. Great job, hero!");
            Destroy(gameObject);
        }
        UpdateEnemeyHealthBarAfterTakingDamage();
    }
    public NavMeshAgent GetNavMeshAgent()
    {
        return navmeshAgent;
    }
    private void UpdateEnemeyHealthBarAfterTakingDamage()
    {
        healthBarImage.fillAmount -= 0.1f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerBulletView>())
        {
            HandleDamage(10);
        }
    }
    public int GetAttackStrength()
    {
        return attackStrength;
    }
}
