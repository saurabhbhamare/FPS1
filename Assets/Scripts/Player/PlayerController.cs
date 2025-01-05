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
        RegisterEventListeners();

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
    private void Fire()
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
    private void OnPlayerContact(Collider collider)
    {
        if (collider.gameObject.GetComponent<EnemyBulletView>())
        {
            AudioManager.Instance.PlayPlayerHurtSound();
            int damage = collider.gameObject.GetComponent<EnemyBulletView>().GetEnemyBulletController().GetBulletDamage();
            TakeDamage(damage);
            if (playerModel.playerHealth <= 0)
            {
                eventService.OnPlayerDeath.Invoke();
            }
            playerHUDManager.UpdateHealthBarAfterTakingDamage(damage);

        }
        if (collider.gameObject.GetComponent<EnemyView>())
        {
            AudioManager.Instance.PlayPlayerHurtSound();
            int damage = collider.gameObject.GetComponent<EnemyView>().enemyController.GetAttackStrength();
            TakeDamage(damage);
            if (playerModel.playerHealth <= 0)
            {
                eventService.OnPlayerDeath.Invoke();
            }
            playerHUDManager.UpdateHealthBarAfterTakingDamage(damage);
        }
        if (collider.gameObject.GetComponent<HealthKit>())
        {
            playerModel.playerHealth = playerModel.playerMaxHealth;
            playerHUDManager.ResetHealthValueAndHealtBar();
        }
    }
    private void OnPlayerDeath()
    {
        characterController.enabled = false;
        playerView.enabled = false;
    }
    public void RegisterEventListeners()
    {
        eventService.OnPlayerContactWithObject.AddListener(OnPlayerContact);
        eventService.OnPlayerDeath.AddListener(OnPlayerDeath);
    }
    private void UnRegisterEventListeners()
    {
        eventService.OnPlayerContactWithObject.RemoveListener(OnPlayerContact);
        eventService.OnPlayerDeath.RemoveListener(OnPlayerDeath);
    }
    ~PlayerController()
    {
        UnRegisterEventListeners();
    }
}