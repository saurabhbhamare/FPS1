using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] protected Image healthBarImage;
    protected int enemyHealth;
    protected int currentHealth;
    protected float speed;

    protected virtual void Update()
    {
        FollowPlayer();
    }
    protected abstract void FollowPlayer();
    protected virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth < 1)
        {
            UIService.Instance.EnemyKilledMessage($"You killed the {GetType().Name}, eliminating a dangerous threat. Great job, hero!");
            Destroy(gameObject);
        }
    }
    protected virtual void UpdateHealthBar()
    {
        healthBarImage.fillAmount = (float)currentHealth / enemyHealth;
    }
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerBulletView>())
        {
            TakeDamage(10);
        }
    }
}
