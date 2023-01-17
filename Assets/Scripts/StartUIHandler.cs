using System;

using TMPro;

using UnityEngine;

public class StartUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text startTimerText;
    public float secondsToStart = 3;

    private UIHandler generalUIScript;
    private float maxSecondsToStart;

    private void Awake()
    {
        maxSecondsToStart = secondsToStart;
        generalUIScript = GameObject.FindObjectOfType<UIHandler>();
    }

    private void OnEnable()
    {
        Debug.Log("StartPanel enabled!");
        Camera.main.gameObject.GetComponent<AudioSource>()?.Stop();
        var src = this.GetComponent<AudioSource>();
        src.Play();
        secondsToStart = maxSecondsToStart;
    }
 

    /// <summary>
    /// Метод обновления кадра самоотключается по достижении таймером 0
    /// </summary>
    private void Update()
    {
        //Debug.Log("in UI update()");
        secondsToStart -= Time.deltaTime;
        if (secondsToStart < 0)
        {
            secondsToStart = 0;
        }
        startTimerText.text = TimeSpan.FromSeconds(secondsToStart).ToString("ss\\:fff");
        if (secondsToStart <= 0)
        {
            secondsToStart = 0;
            generalUIScript.EnableAll();
            this.GetComponent<AudioSource>().Stop();
            Debug.Log($"{gameObject} disabled!");
            gameObject.SetActive(false);
        }
    }
}
