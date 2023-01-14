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
    private Flaregun flaregunScript;

    public int currentScore = 0;

    private void Awake()
    {
        playerCameraScript = GameObject.FindObjectOfType<PlayerCamera>();
        enemySpawnerScript = GameObject.FindObjectOfType<EnemySpawner>();
        crossHairScript = GameObject.FindObjectOfType<CrossHair>();
        flaregunScript = GameObject.FindObjectOfType<Flaregun>();        
    }

    private void Start()
    {
        Restart();
    }

    public void DisableAll()
    {        
        playerCameraScript.enabled = enemySpawnerScript.enabled = crossHairScript.enabled = false;
        flaregunScript.currentRound = 0;
        flaregunScript.autoReload = false;
        topPanel.SetActive(false);
    }

    public void EnableAll()
    {        
        playerCameraScript.enabled = enemySpawnerScript.enabled = crossHairScript.enabled = true;
        flaregunScript.currentRound = 1;
        flaregunScript.autoReload = true;
        topPanel.SetActive(true);
    }

    public void Restart()
    {
        DisableAll();
        endPanel.SetActive(false);
        startPanel.SetActive(true);
        //scoreText.text = "0";
        //timerText.text = TimeSpan.FromSeconds(maxGameTime).ToString("mm\\:ss\\:f"); ;
    }

    public void UpdateTopTimer(string value)
    {
        timerText.text = value;
    }

}
