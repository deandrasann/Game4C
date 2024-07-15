using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColliderLogicDaun : MonoBehaviour
{
    
    public string TrashTag;
    [SerializeField] private KeyCode trashKey = KeyCode.D;

    private bool isCollidingWithPlastik = false;
    private bool canPressButton = false;

   
    void Update()
    {
        if (Input.GetKeyDown(trashKey) && isCollidingWithPlastik && !canPressButton)
        {
            ScoreManager.Instance.AddScore(10);

            canPressButton = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TrashTag))
        {
            isCollidingWithPlastik = true;
            canPressButton = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TrashTag))
        {
            isCollidingWithPlastik = false;
            canPressButton = false;
        }
    }
}
