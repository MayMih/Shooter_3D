using System.Linq;

using Assets.Scripts;

using TMPro;

using UnityEngine;

/// <summary>
/// Класс-обработчик игрового меню
/// </summary>
public class UIHandler : MonoBehaviour
{
    public GameObject endPanel;

    [SerializeField] private GameObject startPanel;    
    [SerializeField] private GameObject topPanel;
    [SerializeField] private TMP_Text timerText;    

    private PlayerCamera playerCameraScript;
    private EnemySpawner enemySpawnerScript;
    private CrossHair crossHairScript;
    private FlareGun flaregunScript;

    private bool isGameOver = false;

    public int currentScore = 0;

    /// <summary>
    /// Признак окончания игры
    /// </summary>
    /// <remarks>
    /// При установке в True уничтожает всех врагов
    /// </remarks>
    public bool IsGameOver 
    { 
        get => isGameOver; 
        set
        {
            isGameOver = value;
            GameObject.FindObjectsOfType<TargetController>()?.ToList().ForEach(obj => Destroy(obj));
        }
    }

    private void Awake()
    {
        playerCameraScript = GameObject.FindObjectOfType<PlayerCamera>();
        enemySpawnerScript = GameObject.FindObjectOfType<EnemySpawner>();
        crossHairScript = GameObject.FindObjectOfType<CrossHair>();
        flaregunScript = GameObject.FindObjectOfType<FlareGun>();
        // в отладке не показываем стартовый экран
        if (Debug.isDebugBuild)
        {
            startPanel.GetComponent<StartUIHandler>().secondsToStart = 0;
        }
    }

    private void Start()
    {
        Restart();
    }

    public void DisableAll()
    {
        playerCameraScript.enabled = crossHairScript.enabled = false;
        flaregunScript.currentRound = 0;
        flaregunScript.autoReload = false;
        topPanel.SetActive(false);        
    }

    public void EnableAll()
    {        
        playerCameraScript.enabled = crossHairScript.enabled = true;
        flaregunScript.currentRound = 1;
        flaregunScript.autoReload = true;
        topPanel.SetActive(true);
        enemySpawnerScript.Begin();
    }

    public void Restart()
    {
        DisableAll();
        currentScore = 0;
        endPanel.SetActive(false);
        startPanel.SetActive(true);
        isGameOver = false;
    }

    public void UpdateTopTimer(string value)
    {
        timerText.text = value;
    }

    internal void AddOnePoint()
    {
        currentScore++;
    }

    internal void RemoveOnePoint()
    {
        if (currentScore > 0)
        {
            currentScore--;
        }
    }
}
