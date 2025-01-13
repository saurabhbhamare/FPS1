using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainHandler : MonoBehaviour
{
    
    [SerializeField] private PlayerView playerView;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerHUDManager playerHUDManager;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private List<EnemySO> levelEnemiesSO;

    [SerializeField] private Button homeButton;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private UIService uiService;
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private GameObject enemyBulletPrefab;

    private PlayerController playerController;
    private PlayerService playerService;
    private EnemyService enemyService;
    private EventService eventService;
    private BulletService bulletService;
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
        bulletService = new BulletService(playerBulletPrefab,enemyBulletPrefab,playerTransform);
        eventService = new EventService();
        playerService = new PlayerService(playerView, characterController, uiService, playerHUDManager, eventService,bulletService);
        enemyService = new EnemyService(levelEnemiesSO, playerTransform,bulletService);
      
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
