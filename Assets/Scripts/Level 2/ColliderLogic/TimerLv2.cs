using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerLv2 : MonoBehaviour
{
    public float timeRemaining = 180f;
    public TextMeshProUGUI countdownText;

    private ScoreManager scoreManager;  // Ensure ScoreManager is properly referenced

    public GameObject resultUI;
    public GameObject scoreUI;
    public GameObject timerUI;

    public TextMeshProUGUI scoreValue;

    private void Start()
    {
        // Initialize ScoreManager
        scoreManager = GetComponent<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager component is not assigned or found.");
        }

        if (countdownText == null)
        {
            Debug.LogError("Countdown Text is not assigned.");
        }
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                timeRemaining = 0; // Set timeRemaining to 0 if it goes below 0
            }
            UpdateCountdownDisplay();
        }
        else
        {
            // Handle what happens when the timer reaches zero
            if (Time.timeScale != 0f)
            {
                TimerEnded();
            }
        }
    }

    private void UpdateCountdownDisplay()
    {
        // Format the time as minutes and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        countdownText.text = ": " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimerEnded()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;

        resultUI.SetActive(true);
        timerUI.SetActive(false);
        scoreUI.SetActive(false);

        // Ensure the scoreValue is set correctly
        if (scoreManager != null)
        {
            scoreValue.text = scoreManager.score.ToString();
        }
        else
        {
            Debug.LogError("ScoreManager is not assigned or found.");
        }
    }
}