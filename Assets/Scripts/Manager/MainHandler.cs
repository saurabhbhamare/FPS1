using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainHandler : MonoBehaviour
{
    
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerBulletView playerBulletView;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerHUDManager playerHUDManager;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private List<EnemySO> levelEnemiesSO;

    [SerializeField] private Button homeButton;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private UIService uiService;

    private PlayerController playerController;
    private PlayerService playerService;
    private EnemyService enemyService;
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
        enemyService = new EnemyService(levelEnemiesSO, playerTransform);
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
    public void RegisterButtonListener()
    {
        homeButton.onClick.AddListener(OnHomeButtonClicked);
    }
    public void OnHomeButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void OnDestroy()
    {
        UnRegisterEventListeners();
    }
}
