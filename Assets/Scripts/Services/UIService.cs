using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIService : MonoBehaviour
{
    public static UIService Instance { get; private set; }

    [SerializeField] private Button newGameButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject messageBox;
    [SerializeField] private TextMeshProUGUI messageText;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void EnemyKilledMessage(string msg)
    {
        messageBox.SetActive(true);
        messageText.text = (msg);
        StartCoroutine(HideNotificationAfterDelay(4f));
    }
    private IEnumerator HideNotificationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); messageBox.SetActive(false);
    }
    public void OnNewGameButtonClicked()
    {
        SceneManager.LoadScene("Level");
    }
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
