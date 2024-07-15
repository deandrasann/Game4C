using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    private bool isPaused = false;
    public GameObject firstPersonController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Continue();
                
            }
            else
            {
                Pause();
                
            }
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        firstPersonController.GetComponent<FirstPersonController>().enabled = false;
    }

    public void Continue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        firstPersonController.GetComponent<FirstPersonController>().enabled = true;
    }

    public void ResetLevel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        firstPersonController.GetComponent<FirstPersonController>().enabled = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
