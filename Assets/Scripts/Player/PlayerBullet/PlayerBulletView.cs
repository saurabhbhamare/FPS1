using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletView : MonoBehaviour
{
    private PlayerBulletController playerBulletController;
    public void SetBulletController(PlayerBulletController playerBulletController)
    {
        this.playerBulletController = playerBulletController;
    }
    private void OnCollisionEnter(Collision collision)
    {
        playerBulletController.HandleCollision();
    }
}
