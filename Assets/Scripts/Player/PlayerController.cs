using UnityEngine;

public class PlayerController
{
    public PlayerView playerView;
    public PlayerModel playerModel;
    private CharacterController characterController;
    private PlayerBulletPool playerBulletPool;
    private PlayerService playerService;
    public UIService uiService;
    public PlayerHUDManager playerHUDManager;
    public EventService eventService;
    public PlayerController(PlayerView playerView, CharacterController characterController, PlayerBulletPool playerBulletPool, PlayerService playerService, UIService uiService, PlayerHUDManager playerHUDManager, EventService eventService)
    {
        this.playerHUDManager = playerHUDManager;
        this.playerService = playerService;
        this.playerModel = new PlayerModel();
        this.playerView = playerView;
        this.characterController = characterController;
        this.playerBulletPool = playerBulletPool;
        this.uiService = uiService;
        this.eventService = eventService;
        eventService.OnTakingDamage.AddListener(OnTakingDamage);
        eventService.OnPlayerDeath.AddListener(OnPlayerDeath);
    }
    public void HandleMovement()
    {
        characterController.Move(playerModel.movement * playerModel.moveSpeed * Time.deltaTime);
        playerModel.velocity.y += playerModel.gravityVal * Time.deltaTime;
        characterController.Move(playerModel.velocity * Time.deltaTime);
    }
    public void HandleInput()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        playerModel.movement = playerView.transform.right * inputX + playerView.transform.forward * inputZ;
        if (Input.GetKeyDown(KeyCode.Space) && playerModel.isGrounded)
        {
            playerModel.velocity.y = Mathf.Sqrt(playerModel.jumpForce * -2f * playerModel.gravityVal);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (playerModel.isReloading)
            {
                return;
            }
            else
            {
                Fire();
            }

        }
    }
    public void Fire()
    {
        AudioManager.Instance.PlayFireSound();
        PlayerBulletController playerBullet = playerBulletPool.GetPlayerBullet();

        playerBullet.ConfigureBullet(playerView.firePoint, playerService);
        playerModel.ammoStock--;
        if (playerModel.ammoStock < 1)
        {
            playerView.ReloadWeapon();
        }
        playerHUDManager.UpdateAmmoBarUIAfterFiring();
    }
    public void TakeDamage(int damage)
    {
        playerModel.playerHealth -= damage;
    }
    public void OnTakingDamage(Collider collider)
    {
        if (collider.gameObject.GetComponent<EnemyBulletView>())
        {
            int damage = collider.gameObject.GetComponent<EnemyBulletView>().GetEnemyBulletController().bulletDamage;
            TakeDamage(damage);
            if (playerModel.playerHealth <= 0)
            {
                eventService.OnPlayerDeath.Invoke();
            }
            playerHUDManager.UpdateHealthBarAfterTakingDamage(damage);
        }
        if (collider.gameObject.GetComponent<Larvae>())
        {
            int damage = collider.gameObject.GetComponent<Larvae>().GetAttackStrength();
            TakeDamage(damage);
            if (playerModel.playerHealth <= 0)
            {
                eventService.OnPlayerDeath.Invoke();
            }
            playerHUDManager.UpdateHealthBarAfterTakingDamage(damage);
        }
    }
    private void OnPlayerDeath()
    {
        characterController.enabled = false;
        playerView.enabled = false;
    }
}