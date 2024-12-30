using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService
{
    private CharacterController characterController;
    private PlayerController playerController;
    private PlayerView playerView;
    private PlayerBulletPool playerBulletPool;
    private PlayerBulletView playerBulletView;
    private UIService uiService;
    private PlayerHUDManager playerHUDManager;
    private EventService eventService;
    public PlayerService(PlayerView playerView, CharacterController characterController, PlayerBulletView playerBulletView, UIService uiService, PlayerHUDManager playerHUDManager, EventService eventService)
    {
        this.playerHUDManager = playerHUDManager;
        this.uiService = uiService;
        this.playerView = playerView;
        this.playerBulletView = playerBulletView;
        this.eventService = eventService;
        this.characterController = characterController;
        playerBulletPool = new PlayerBulletPool(playerBulletView);
        playerController = new PlayerController(playerView, characterController, playerBulletPool, this, uiService, playerHUDManager, this.eventService);
        playerView.SetPlayerController(playerController);
    }
    public void ReturnBulletToPool(PlayerBulletController bulletToReturn) => playerBulletPool.ReturnItem(bulletToReturn);
}
