using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
   public float timeLeft = 180.0f; // 3 minutes in seconds
    private Text timerText;

    void Start()
    {
        timerText = GetComponent<Text>();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(timeLeft <=0 && GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            GameManager.Instance.GameOver();
            Time.timeScale = 0f;
            timeLeft = 0f;
        }
    }
}
