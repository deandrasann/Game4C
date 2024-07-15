using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StandInteract : MonoBehaviour
{

    public GameObject kertasUI;
    public GameObject plastikUI;
    public GameObject daunUI;

    public string kertasScene;
    public string plastikScene;
    public string daunScene;

    private bool isInKertas = false;
    private bool isInPlastik = false;
    private bool isInDaun = false;



    void Update()
    {

        if(isInKertas == true){
            if(Input.GetKeyDown(KeyCode.E)){
                MoveToAnotherScene(kertasScene);
            }
        }
        if(isInPlastik == true){
            if(Input.GetKeyDown(KeyCode.E)){
                MoveToAnotherScene(plastikScene);
            }
        }
        if (isInDaun == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MoveToAnotherScene(daunScene);
            }
        }
    }

    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("StandKertasLuar")){
            kertasUI.SetActive(true);
            isInKertas = true;
        }

        if (other.CompareTag("StandPlastikLuar"))
        {
            plastikUI.SetActive(true);
            isInPlastik = true;
        }

        if (other.CompareTag("StandDaunLuar"))
        {
            daunUI.SetActive(true);
            isInDaun = true;
        }
    }

   
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("StandKertasLuar")){
            kertasUI.SetActive(false);
            isInKertas = false;
        }
        if (other.CompareTag("StandPlastikLuar"))
        {
            plastikUI.SetActive(false);
            isInPlastik = false;
        }
        if (other.CompareTag("StandDaunLuar"))
        {
            daunUI.SetActive(false);
            isInDaun = false;
        }
    }

    void MoveToAnotherScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
