using UnityEngine;

public class BulletView : MonoBehaviour
{
     private float bulletSpeed = 100f;
    //[SerializeField] private float bulletSpeed = 100f;
    public BulletType bulletType;
    private BulletService bulletService;

    // Set BulletService reference
    public void SetBulletService(BulletService service)
    {
        bulletService = service;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        //if (Vector3.Distance(transform.position, Vector3.zero) > 100f) // Example distance check
        //{
        //    bulletService.ReturnBullet(this);
        //}
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
}
