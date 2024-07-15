using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 60f; 
    public TextMeshProUGUI countdownText;
    public GameObject berhasil;
    public GameObject gagal;

    private void Start()
    {
        Time.timeScale = 1f;
        if (countdownText == null)
        {
            Debug.LogError("Countdown Text is not assigned.");
        }
    }

    private void Update()
    {
        int score = CompletionManager.Instance.score;
        int target = CompletionManager.Instance.target;
        if (score == target)
        {
            Time.timeScale = 0f;
            if(berhasil != null)
            {
                berhasil.SetActive(true);
            }
            
            return;
        }

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateCountdownDisplay();
        }
        else
        {
            timeRemaining = 0;
            // Handle what happens when the timer reaches zero
            TimerEnded();
        }
    }

    private void UpdateCountdownDisplay()
    {
        // Format the time as minutes and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        countdownText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimerEnded()
    {
        // This is where you handle what happens when the timer ends
        // For example, you could display a message, trigger an event, etc.
        Debug.Log("Timer has ended!");
        gagal.SetActive(true);

    }

}
