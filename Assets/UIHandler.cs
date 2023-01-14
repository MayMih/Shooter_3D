using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private TMP_Text startTimerText;
    [SerializeField] private float secondsToStart = 3;

    private PlayerCamera playerCameraScript;
    private EnemySpawner enemySpawnerScript;
    private CrossHair crossHairScript;
    private Flaregun flaregunScript;

    // Start is called before the first frame update
    void Start()
    {
        playerCameraScript = GameObject.FindObjectOfType<PlayerCamera>();
        enemySpawnerScript = GameObject.FindObjectOfType<EnemySpawner>();
        crossHairScript = GameObject.FindObjectOfType<CrossHair>();
        flaregunScript = GameObject.FindObjectOfType<Flaregun>();
        playerCameraScript.enabled = enemySpawnerScript.enabled = crossHairScript.enabled = false;
        flaregunScript.currentRound = 0;
        flaregunScript.autoReload = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("in UI update()");
        secondsToStart -= Time.deltaTime;
        if (secondsToStart < 0)
        {
            secondsToStart = 0;
        }
        startTimerText.text = TimeSpan.FromSeconds(secondsToStart).ToString("mm\\:ss\\:fff");
        if (secondsToStart <= 0)
        {            
            secondsToStart = 0;
            startPanel.SetActive(false);
            playerCameraScript.enabled = enemySpawnerScript.enabled = crossHairScript.enabled = true;
            flaregunScript.currentRound = 1;
            flaregunScript.autoReload = true;
            this.GetComponent<AudioSource>().loop = false;
            this.enabled = false;
        }        
    }
}
