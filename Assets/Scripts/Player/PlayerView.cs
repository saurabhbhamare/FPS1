using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerController playerController;
    private Rigidbody rb;
    [SerializeField] private Transform groundCheck;
    public LayerMask groundMask;
    public Transform firePoint;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (playerController == null)
        {
            Debug.Log("PlayerController is null");
        }
        playerController.playerModel.isGrounded = Physics.CheckSphere(groundCheck.position, playerController.playerModel.groundDistance, groundMask);
        playerController.HandleInput();
    }
    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    private void FixedUpdate()
    {
        playerController.HandleMovement();
    }
    public void ReloadWeapon()
    {
        StartCoroutine(StartCoroutineForReloading());
    }
    public IEnumerator StartCoroutineForReloading()
    {
        playerController.playerModel.isReloading = true;
        yield return new WaitForSeconds(3f);
        playerController.playerModel.ammoStock = playerController.playerModel.maxAmmo;
        playerController.playerModel.isReloading = false;
        playerController.playerHUDManager.ResetAmmoBarUIAfterReloading();
    }
    public Rigidbody GetRigidbody()
    {
        return rb;
    }
    private void OnTriggerEnter(Collider other)
    {
        playerController.eventService.OnTakingDamage.Invoke(other);
    }
}
