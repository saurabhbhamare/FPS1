
using UnityEngine;
public class BloaterFly : Enemy
{
    private float hoverHeight = 10.0f;

    private void Start()
    {
        enemyHealth = 50;
        currentHealth = enemyHealth;
        speed = 5.0f;
    }
    protected override void FollowPlayer()
    {
        //Calculate character position

        Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + hoverHeight, playerTransform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }
}
