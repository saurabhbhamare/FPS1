using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainHandler : MonoBehaviour
{
    [SerializeField] private PlayerView playerView;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerBulletView playerBulletView;
    [SerializeField] private PlayerHUDManager playerHUDManager;
    [SerializeField] private Button homeButton;
    [SerializeField] private GameObject gameOverScreen;

    private PlayerController playerController;
    [SerializeField] private UIService uiService;
    private PlayerService playerService;
    private EventService eventService;

    void Start()
    {

        InitServices();
        RegisterEventListener();
        RegisterButtonListener();
    }
    private void RegisterEventListener()
    {
        eventService.OnPlayerDeath.AddListener(ShowGameOverScreen);
    }
    private void InitServices()
    {
        eventService = new EventService();
        playerService = new PlayerService(playerView, characterController, playerBulletView, uiService, playerHUDManager, eventService);
    }
    private void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
    private void UnRegisterEventListeners()
    {
        eventService.OnPlayerDeath.RemoveListener(ShowGameOverScreen);
    }
    ~MainHandler()
    {
        UnRegisterEventListeners();
    }
    public void RegisterButtonListener()
    {

        homeButton.onClick.AddListener(OnHomeButtonClicked);
    }
    public void OnHomeButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
