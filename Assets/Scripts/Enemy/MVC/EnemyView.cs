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
                Debug.Log("Enemy controller is null");
                return;
            }
            StartCoroutine(HandleShooting());
        }
    }
    void Update()
    {
        enemyController.Movement();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerView>())
        {
            Debug.Log("Collision happened with player");
            Time.timeScale = 0.5f;
        }
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
    public IEnumerator HandleShooting()
    {
        while (true)
        {
            this.enemyController.Attack();
            yield return new WaitForSeconds(3f);
        }
    }
}
