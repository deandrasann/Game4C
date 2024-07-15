using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PickUpObj : MonoBehaviour
{
    public float interactionRange = 3f; 
    public LayerMask kertasLayer; 
    public LayerMask plastikLayer; 
    public LayerMask daunLayer; 

    public TextMeshProUGUI KertasText;
    public TextMeshProUGUI PlastikText;
    public TextMeshProUGUI DaunText;

    private int kertasAmount = 0; 
    private int plastikAmount = 0; 
    private int daunAmount = 0;

    public int targetKertasAmount = 0;
    public int targetPlastikAmount = 0;
    public int targetDaunAmount = 0;

    AudioLevel1 audioLevel1;

    public GameObject finishGameUIPanel;
    public GameObject firstPersonController;

    private void Awake()
    {
        audioLevel1 = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioLevel1>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionRange, kertasLayer))
            {
                Kertas kertas = hit.collider.GetComponent<Kertas>();
                if (kertas != null)
                {
                    kertasAmount += kertas.Loot();
                    KertasText.text = ":    " + kertasAmount.ToString() + "  / 10  ";
                    // UpdateLootUI();

                    audioLevel1.PlaySFX(audioLevel1.takeKertas);
                    CheckForCompletion();
                }
            }

            if (Physics.Raycast(ray, out hit, interactionRange, plastikLayer))
            {
                Plastik plastik = hit.collider.GetComponent<Plastik>();
                if (plastik != null)
                {
                    plastikAmount += plastik.Loot();
                    PlastikText.text = ":    " + plastikAmount.ToString() + "  / 10  ";
                    // UpdateLootUI();

                    audioLevel1.PlaySFX(audioLevel1.takePlastik);
                    CheckForCompletion();
                }
            }

            if (Physics.Raycast(ray, out hit, interactionRange, daunLayer))
            {
                Daun daun = hit.collider.GetComponent<Daun>();
                if (daun != null)
                {
                    daunAmount += daun.Loot();
                    DaunText.text = ":    " + daunAmount.ToString() + "  / 10  ";
                    // UpdateLootUI();

                    audioLevel1.PlaySFX(audioLevel1.takeDaun);
                    CheckForCompletion();
                }
            }
        }
    }

    private void CheckForCompletion()
    {
        if (kertasAmount >= targetKertasAmount && plastikAmount >= targetPlastikAmount && daunAmount >= targetDaunAmount)
        {    
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            finishGameUIPanel.SetActive(true);
            firstPersonController.GetComponent<FirstPersonController>().enabled = false;
        }
    }

    public void MoveToLevel2()
    {
        SceneManager.LoadScene("Cutscene Lv1 End");
    }
}
