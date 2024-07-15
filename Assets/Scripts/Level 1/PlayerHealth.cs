using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public TMP_Text healthText; // Reference to the TextMeshProUGUI element
    public GameObject gameOverUI; // Reference to the Game Over UI

    void Start()
    {
        // Load or initialize player health
        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            currentHealth = PlayerPrefs.GetInt("PlayerHealth");
        }
        else
        {
            currentHealth = maxHealth;
            PlayerPrefs.SetInt("PlayerHealth", currentHealth);
            PlayerPrefs.Save();
        }

        UpdateHealthUI(); // Update the health UI at the start
        gameOverUI.SetActive(false); // Ensure Game Over UI is hidden at the start
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthUI(); // Update the health UI when health changes

        if (currentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            // Show Game Over UI
            currentHealth = 0;
            PlayerPrefs.SetInt("PlayerHealth", currentHealth);
            PlayerPrefs.Save();
            gameOverUI.SetActive(true); // Show Game Over UI
        }
        else
        {
            // Save the current reduced health to PlayerPrefs
            PlayerPrefs.SetInt("PlayerHealth", currentHealth);
            PlayerPrefs.Save();
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void UpdateHealthUI()
    {
        // Update the health text display
        if (healthText != null)
        {
            healthText.text = "x " + currentHealth.ToString();
        }
    }

    public void ResetHealth()
    {
        // Set health to maxHealth and save to PlayerPrefs
        currentHealth = maxHealth;
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
        PlayerPrefs.Save();
    }
}