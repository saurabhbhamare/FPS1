using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]
public class PlayerSO : ScriptableObject
{
    public int PlayerMaxHealth;
    public float MoveSpeed;
    public float JumpForce;
    public float GravityValue;
    public int MaxAmmo;
    public float ReloadDuration;
    public float GroundDistance;
}
