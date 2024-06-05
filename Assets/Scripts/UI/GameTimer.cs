using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float startTimeInSeconds;
    [SerializeField] private TMP_Text timeText;
    private float timeRemaining = 10;
    private bool timerIsRunning = false;

    private void Start()
    {
        StartTimer();
    }

    private void StartTimer()
    {
        timeRemaining = startTimeInSeconds;
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                timeText.text = "Time is up!";
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = "Time left: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
