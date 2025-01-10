using UnityEngine;

public class PlayerBulletView : MonoBehaviour
{
    public PlayerBulletController playerBulletController;   
    public void SetBulletController(PlayerBulletController playerBulletController)
    {
        this.playerBulletController = playerBulletController;
    }
    private void OnCollisionEnter(Collision collision)
    {
        playerBulletController.HandleCollision();
    }
    
}
