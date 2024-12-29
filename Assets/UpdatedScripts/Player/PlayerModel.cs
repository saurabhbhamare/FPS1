using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public int playerHealth = 100;
    public bool isGrounded;
    public float moveSpeed = 10f;
    public float lookSpeed;
    public float jumpForce = 5f;
    public float gravityVal = -9.81f;
    public Vector3 movement;
    public Vector3 velocity;
    public float groundDistance = 0.1f;
    public int ammoStock = 10;
    public float reloadingTime;
    public bool isReloading;
    public int maxAmmo = 10;
}
