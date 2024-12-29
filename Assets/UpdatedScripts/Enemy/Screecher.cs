using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screecher : MonoBehaviour
{
    private EnemyBulletPool enemyBulletPool;
    [SerializeField] private EnemyBulletView enemyBulletView;
    public Transform playerTransform;
    [SerializeField] private int screecherHealth;
    [SerializeField] private Image healthBarImage;
    public Transform firePoint;
    public int attackStrength = 10;
    private float zigzagAmplitude = 10f;
    private float zigzagFrequency = 5f;
    int damage = 5;
    private void Start()
    {
        StartCoroutine(ContinuousFire());
    }
    private void Awake()
    {
        enemyBulletPool = new EnemyBulletPool(enemyBulletView, this);
    }
    private void Update()
    {
        FollowPlayer();
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }
    private void ZigZagMovement(ref Vector3 currentPosition)
    {
        float zigzagOffset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        currentPosition.x += zigzagOffset;
    }
    private void Attack()
    {
        EnemyBulletController enemyBullet = enemyBulletPool.GetEnemyBullet();
        enemyBullet.ConfigureBullet(firePoint, playerTransform);
    }
    public void TakeDamage(int damage)
    {
        screecherHealth -= damage;
        UpdateHealthBar();
        if (this.screecherHealth < 1)
        {
            UIService.Instance.EnemyKilledMessage("You killed the Screecher the flying demon, eliminating a dangerous threat. Great!");
            Destroy(gameObject);
        }
    }
    private void FollowPlayer()
    {
        Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + 10f, playerTransform.position.z);
        ZigZagMovement(ref targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.5f * Time.deltaTime);
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerBulletView>())
        {
            TakeDamage(damage);
        }
    }
    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = (float)screecherHealth / 150;
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
