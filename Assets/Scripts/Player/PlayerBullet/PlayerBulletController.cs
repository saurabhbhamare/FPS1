using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController
{
    private PlayerBulletView playerBulletView;
    public int bulletDamage = 10;
    private float bulletSpeed = 250f;
    private PlayerService playerService;
    public PlayerBulletController(PlayerBulletView playerBulletView)
    {
        this.playerBulletView = Object.Instantiate(playerBulletView);
        this.playerBulletView.SetBulletController(this);
    }
    public void ConfigureBullet(Transform firePoint, PlayerService playerService)
    {
        this.playerService = playerService;
        playerBulletView.gameObject.SetActive(true);
        playerBulletView.transform.position = firePoint.transform.position;
        Rigidbody rb = playerBulletView.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;

    }
    public void HandleCollision()
    {
        playerBulletView.gameObject.SetActive(false);
        playerService.ReturnBulletToPool(this);
    }
}
