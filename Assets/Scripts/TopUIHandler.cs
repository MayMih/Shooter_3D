using System;

using TMPro;

using UnityEngine;

/// <summary>
/// Класс верхней панели статуса Игры - отвечает за отображение текущего счёта
/// </summary>
public class TopUIHandler : MonoBehaviour
{
    [Header("Макс. время игры в секундах")]
    [SerializeField] private int maxGameTime;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text totalScoreText;

    private UIHandler generalUIScript;
    private float currentTime;
    private AudioSource mainCamAudio;

    private void Start()
    {
        generalUIScript = GameObject.FindObjectOfType<UIHandler>();
        mainCamAudio = Camera.main.gameObject.GetComponent<AudioSource>();        
    }

    private void OnEnable()
    {
        currentTime = maxGameTime;
        mainCamAudio?.Stop();
    }

    // Update is called once per frame
    private void Update()
    {
        scoreText.text = generalUIScript.currentScore.ToString();
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            currentTime = 0;
        }
        generalUIScript?.UpdateTopTimer(TimeSpan.FromSeconds(currentTime).ToString("mm\\:ss\\:f"));
        if (currentTime == 0)
        {
            totalScoreText.text = generalUIScript?.currentScore.ToString();
            Cursor.lockState = CursorLockMode.None;
            generalUIScript?.DisableAll();
            generalUIScript.IsGameOver = true;
            generalUIScript.endPanel.SetActive(true);
            mainCamAudio?.Play();
        }
    }
}
