using UnityEngine;

public class PlayerModel
{
    private PlayerSO playerData;
    public Vector3 movement;
    public Vector3 velocity;

    public bool isGrounded;
    public bool isReloading;
    public float reloadStartTime;
    public int playerHealth;
    public int playerMaxHealth;
    public float jumpForce;
    public float moveSpeed;
    public float reloadDuration;
    public int maxAmmo;
    public float gravityValue;
    public float groundDistance;
    public int ammoStock;

    public PlayerModel(PlayerSO playerData)
    {
        this.playerData = playerData;
        playerMaxHealth = playerData.PlayerMaxHealth;
        moveSpeed = playerData.MoveSpeed;
        jumpForce = playerData.JumpForce;
        gravityValue = playerData.GravityValue;
        maxAmmo = playerData.MaxAmmo;
        reloadDuration = playerData.ReloadDuration;
        groundDistance = playerData.GroundDistance;
        ammoStock = maxAmmo;
        playerHealth = playerData.PlayerMaxHealth;
        reloadStartTime = 0f;
    }



}
