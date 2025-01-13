using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    public PlayerController playerController;
    private Rigidbody rb;
    public LayerMask groundMask;
    public Transform firePoint;
    private void Start()
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
        playerController.CheckReloading();
    }
    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    private void FixedUpdate()
    {
        playerController.HandleMovement();
    }
    public Rigidbody GetRigidbody()
    {
        return rb;
    }
    private void OnTriggerEnter(Collider other)
    {
        playerController.eventService.OnPlayerContactWithObject.Invoke(other);
    }
    private void OnDestroy()
    {
        playerController.UnRegisterEventListeners();
    }

}
