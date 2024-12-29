using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloaterFly : MonoBehaviour
{
    public Transform playerTransform;

    private int enemyHealth = 20;
    [SerializeField] private Image healthBarImage;

    private float speed = 5.0f;
    private float hoverHeight = 10.0f; 

    private void Update()
    {
        Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + hoverHeight, playerTransform.position.z);

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerBulletView>())
        {
            TakeDamage(10);
        }
    }
    private void TakeDamage(int damage)
    {
        enemyHealth -= damage; 
        if (this.enemyHealth < 1)
        {
            UIService.Instance.EnemyKilledMessage("You killed the BoaterFLY, eliminating a dangerous threat. Great job, hero!");
            Destroy(gameObject);
        }
    }


}
