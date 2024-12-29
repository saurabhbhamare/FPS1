using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPool : GenericResourcePool<PlayerBulletController>
{

    private PlayerBulletView playerBulletView;
    public PlayerBulletPool(PlayerBulletView playerBulletView)
    {
        this.playerBulletView = playerBulletView;
    }
    public PlayerBulletController GetPlayerBullet() => GetItem<PlayerBulletController>();
    protected override PlayerBulletController CreateItem<T>()
    {
        PlayerBulletController playerBullet = new PlayerBulletController(playerBulletView);
        return playerBullet;
    }
}
