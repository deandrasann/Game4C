using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompletionManager : MonoBehaviour
{
    public static CompletionManager Instance;

    public int score = 0;
    public int target = 0;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
