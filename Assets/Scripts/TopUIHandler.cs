using System;

using TMPro;

using UnityEngine;

public class TopUIHandler : MonoBehaviour
{
    [Header("Макс. время игры в секундах")]
    [SerializeField] private int maxGameTime;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text totalScoreText;

    private UIHandler generalUIScript;
    private float currentTime;

    private void Start()
    {
        generalUIScript = GameObject.FindObjectOfType<UIHandler>();
    }

    private void OnEnable()
    {
        currentTime = maxGameTime;
        Camera.main.gameObject.GetComponent<AudioSource>()?.Stop();
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
            generalUIScript.endPanel.SetActive(true);
            totalScoreText.text = generalUIScript?.currentScore.ToString();
            Cursor.lockState = CursorLockMode.None;
            generalUIScript?.DisableAll();
            generalUIScript.IsGameOver = true;
            Camera.main.gameObject.GetComponent<AudioSource>()?.Play();
        }
    }
}
