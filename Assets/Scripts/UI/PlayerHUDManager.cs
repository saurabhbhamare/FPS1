using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUDManager : MonoBehaviour
{
    [SerializeField] private Image playerHealthBarImage;
    [SerializeField] private Image playerBulletsBarImage;
    [SerializeField] private GameObject textMSG;
    [SerializeField] private TextMeshProUGUI killedEnemiesText;
    [SerializeField] private PlayerSO playerData;

    private int playerHealthAmount;
    private float ammoAmount;
    private float maxAmmo;
    private void Awake()
    {
        playerHealthAmount = playerData.PlayerMaxHealth;
        maxAmmo = playerData.MaxAmmo;
        ammoAmount = maxAmmo;
    }
    public void UpdateAmmoBarUIAfterFiring()
    {
        ammoAmount--;
        playerBulletsBarImage.fillAmount = ammoAmount / maxAmmo;
    }
    public void ResetAmmoAmount()
    {
        ammoAmount = maxAmmo;
    }
    public void ResetAmmoBarUIAfterReloading()
    {
        ResetAmmoAmount();
        playerBulletsBarImage.fillAmount = 1;
    }
    public void UpdateHealthBarAfterTakingDamage(int damage)
    {
        PlayerHealthAfterTakingDamage(damage);
        playerHealthBarImage.fillAmount = playerHealthAmount / 100f;
    }
    public void PlayerHealthAfterTakingDamage(int damage)
    {
        playerHealthAmount -= damage;
    }
    public void ResetHealthValueAndHealtBar()
    {
        playerHealthAmount = playerData.PlayerMaxHealth;
        playerHealthBarImage.fillAmount = 1f;
    }
}
