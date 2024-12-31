using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUDManager : MonoBehaviour
{
    [SerializeField] private Image playerHealthBarImage;
    [SerializeField] private Image playerBulletsBarImage;
    [SerializeField] private GameObject textMSG;
    [SerializeField] private TextMeshProUGUI killedEnemiesText;

    private int playerHealthAmount = 100;
    private float ammoAmount = 10;
    private float maxAmmo = 10;
    private int enemiesKilledCount;
    public void UpdateAmmoBarUIAfterFiring()
    {
        ammoAmount--;
        playerBulletsBarImage.fillAmount = ammoAmount / 10;
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
        playerHealthAmount = 100;
        playerHealthBarImage.fillAmount = 1f;
    }
}
