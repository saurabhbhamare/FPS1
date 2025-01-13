using UnityEngine;

public class PlayerController
{
    public PlayerView playerView;
    public PlayerModel playerModel;
    private CharacterController characterController;

    // private BulletPool playerBulletPool;
    //private PlayerBulletPool playerBulletPool;
    private PlayerService playerService;
    public UIService uiService;
    public PlayerHUDManager playerHUDManager;
    public EventService eventService;
    private BulletService bulletService;
    public PlayerController(PlayerView playerView, CharacterController characterController, PlayerService playerService, UIService uiService, PlayerHUDManager playerHUDManager, EventService eventService, BulletService bulletService)
    {
        this.bulletService = bulletService;
        //playerBulletPool = new BulletPool();
        this.playerHUDManager = playerHUDManager;
        this.playerService = playerService;
        this.playerModel = new PlayerModel();
        this.playerView = playerView;
        this.characterController = characterController;
        //this.playerBulletPool = playerBulletPool;
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
        if (bulletService == null)
        {
            Debug.Log("bulletservice is null");
        }
        bulletService.FireBullet(playerView.firePoint.position, playerView.firePoint.rotation, BulletType.PLAYER);
        playerModel.ammoStock--;
        if (playerModel.ammoStock < 1)
        {
            StartReloading();
        }
        playerHUDManager.UpdateAmmoBarUIAfterFiring();
    }
    public void TakeDamage(int damage)
    {
        playerModel.playerHealth -= damage;
    }
    private void OnPlayerContact(Collider collider)
    {
        if (collider.gameObject.GetComponent<Bullet>())
        {
            AudioManager.Instance.PlayPlayerHurtSound();
            int damage = collider.gameObject.GetComponent<Bullet>().GetBulletDamage();
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
    public void StartReloading()
    {
        playerModel.isReloading = true;
        playerModel.reloadStartTime = Time.time;
    }
    public void CheckReloading()
    {
        if (playerModel.isReloading && Time.time >= playerModel.reloadStartTime + playerModel.reloadDuration)
        {
            playerModel.ammoStock = playerModel.maxAmmo;
            playerModel.isReloading = false;
            playerHUDManager.ResetAmmoBarUIAfterReloading();
        }
    }
    public void RegisterEventListeners()
    {
        eventService.OnPlayerContactWithObject.AddListener(OnPlayerContact);
        eventService.OnPlayerDeath.AddListener(OnPlayerDeath);
    }
    public void UnRegisterEventListeners()
    {
        eventService.OnPlayerContactWithObject.RemoveListener(OnPlayerContact);
        eventService.OnPlayerDeath.RemoveListener(OnPlayerDeath);
    }

}