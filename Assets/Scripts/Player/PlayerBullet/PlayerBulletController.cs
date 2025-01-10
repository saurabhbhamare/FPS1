using UnityEngine;

public class PlayerBulletController
{
    private PlayerBulletView playerBulletView;
    private PlayerService playerService;
    private PlayerBulletModel playerBulletModel;
    public PlayerBulletController(PlayerBulletView playerBulletView)
    {   
        this.playerBulletView = Object.Instantiate(playerBulletView);
        this.playerBulletModel = new PlayerBulletModel();
        this.playerBulletView.SetBulletController(this);
    }
    public void ConfigureBullet(Transform firePoint, PlayerService playerService)
    {
        this.playerService = playerService;
        playerBulletView.gameObject.SetActive(true);
        playerBulletView.transform.position = firePoint.transform.position;
        Rigidbody rb = playerBulletView.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * this.playerBulletModel.bulletSpeed;
    }
    public void HandleCollision()
    {
        playerBulletView.gameObject.SetActive(false);
        playerService.ReturnBulletToPool(this);
    }
    public PlayerBulletModel GetPlayerBulletModel()
    {
        return playerBulletModel;
    }
}
