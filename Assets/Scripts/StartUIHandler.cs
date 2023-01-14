using System;

using TMPro;

using UnityEngine;

public class StartUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text startTimerText;
    [SerializeField] private float secondsToStart = 3;

    private UIHandler generalUIScript;

    private void Start()
    {
        generalUIScript = GameObject.FindObjectOfType<UIHandler>();
    }

    private void OnEnable()
    {
        this.GetComponent<AudioSource>().loop = true;
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
            this.GetComponent<AudioSource>().loop = false;
            gameObject.SetActive(false);
        }
    }
}
