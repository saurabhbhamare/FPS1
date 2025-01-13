using UnityEngine;
public class ScreecherController : EnemyController
{
    private BulletService bulletService;
    private float zigzagAmplitude = 500f;
    private float zigzagFrequency = 1f;
    private float hoverHeight = 10.0f;
    private Transform playerTransform;
    private Transform fireTransform;
    public ScreecherController(EnemySO enemySO, Transform playerTransform, BulletService bulletService) : base(enemySO, playerTransform)
    {
        this.bulletService = bulletService;
        this.enemyView.SetEnemyController(this);
        enemyView.gameObject.transform.position = enemySO.SpawnPosition;
        fireTransform = enemyView.GetFirePointTransform();
        enemyView.transform.rotation = Quaternion.Euler(enemySO.SpawnRotation);
        this.playerTransform = playerTransform;
    }
    public override void Movement()
    {

        // Calculate the zigzag offset 
        float zigzagOffset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;

        // Calculate the target position with zigzag offset
        Vector3 targetPosition = new Vector3(this.targetTransform.position.x + zigzagOffset, targetTransform.position.y + hoverHeight, targetTransform.position.z);

        // Move towards the target position
        this.enemyView.transform.position = Vector3.MoveTowards(this.enemyView.transform.position, targetPosition, 5f * Time.deltaTime);

        // Smoothly rotate towards the player
        Vector3 direction = (targetTransform.position - this.enemyView.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        Quaternion adjustment = Quaternion.Euler(0, 90, 0);
        lookRotation *= adjustment;
        this.enemyView.transform.rotation = Quaternion.Slerp(this.enemyView.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    public override void Attack()
    {
        bulletService.FireBullet(fireTransform.position, fireTransform.rotation, BulletType.ENEMY);
    }
}
