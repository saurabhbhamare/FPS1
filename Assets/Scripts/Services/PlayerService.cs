using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService
{
    private CharacterController characterController;
    private PlayerController playerController;
    private PlayerView playerView;
    private UIService uiService;
    private PlayerHUDManager playerHUDManager;
    private EventService eventService;
    private BulletService bulletService;
    public PlayerService(PlayerView playerView, CharacterController characterController,UIService uiService, PlayerHUDManager playerHUDManager, EventService eventService,BulletService bulletService)
    {
        this.bulletService = bulletService;
        this.playerHUDManager = playerHUDManager;
        this.uiService = uiService;
        this.playerView = playerView;
        this.eventService = eventService;
        this.characterController = characterController;
        playerController = new PlayerController(playerView, characterController,this, uiService, playerHUDManager, this.eventService,bulletService);
        playerView.SetPlayerController(playerController);
    }
}
