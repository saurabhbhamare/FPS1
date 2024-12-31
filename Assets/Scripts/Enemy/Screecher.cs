using UnityEngine;
using System.Collections;
public class Screecher : Enemy
{
    private EnemyBulletPool enemyBulletPool;
    [SerializeField] private EnemyBulletView enemyBulletView;
    public Transform firePoint;
    private float zigzagAmplitude = 500f;
    [SerializeField] private float zigzagFrequency = 1f;
    private float hoverHeight = 10.0f;

    private void Start()
    {
        enemyBulletPool = new EnemyBulletPool(enemyBulletView, this);
        StartCoroutine(ContinuousFire());
        enemyHealth = 150;
        currentHealth = enemyHealth;
        speed = 2f;
    }
    protected override void FollowPlayer()
    {
        // Calculate the zigzag offset 
        float zigzagOffset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;

        // Calculate the target position with zigzag offset
        Vector3 targetPosition = new Vector3(playerTransform.position.x + zigzagOffset, playerTransform.position.y + hoverHeight, playerTransform.position.z);

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Smoothly rotate towards the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }

    private void Attack()
    {
        EnemyBulletController enemyBullet = enemyBulletPool.GetEnemyBullet();
        enemyBullet.ConfigureBullet(firePoint, playerTransform);
    }
    public void ReturnBulletToPool(EnemyBulletController bulletToReturn) => enemyBulletPool.ReturnItem(bulletToReturn);
    public IEnumerator ContinuousFire()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(3f);
        }
    }
}
