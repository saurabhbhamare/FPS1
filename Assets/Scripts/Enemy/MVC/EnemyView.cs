using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Transform firePointTransform;
    public EnemyController enemyController { get; private set; }
    public NavMeshAgent navmeshAgent;
    private void Awake()
    {
        navmeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }
    public void SetEnemyController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }
    void Start()
    {
        if (enemyController is ScreecherController)
        {
            if (this.enemyController == null)
            {
                return;
            }
            enemyController.GetEnemyModel().nextAttackTime = Time.time + enemyController.GetEnemyModel().attackInterval;
        }
    }
    void Update()
    {
        enemyController.Movement();
        HandleShooting();
    }
    private void OnTriggerEnter(Collider other)
    {
        this.enemyController.HandleCollision(other);
    }
    public Image GetHealthBarUIImage()
    {
        return healthBarImage;
    }
    public Transform GetFirePointTransform()
    {
        return firePointTransform;
    }
    public void HandleShooting()
    {
        if(Time.time >= enemyController.GetEnemyModel().nextAttackTime)
        {
            this.enemyController.Attack();
            enemyController.GetEnemyModel().nextAttackTime = Time.time + enemyController.GetEnemyModel().attackInterval;
        }
    }
}
