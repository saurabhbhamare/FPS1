using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 100f;
    private int bulletDamage = 10;
    public BulletType bulletType;
    private BulletService bulletService;
    public void SetBulletService(BulletService service)
    {
        bulletService = service;
    }
    void Update()
    {
        //Handle Movement
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle collision logic
        bulletService.ReturnBullet(this);
    }
    private void OnCollisionEnter(Collision collision)
    {
        bulletService.ReturnBullet(this);
    }
    public int GetBulletDamage()
    {
        return bulletDamage;
    }
}
