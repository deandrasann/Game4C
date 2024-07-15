using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ColliderLogicPlastik : MonoBehaviour
{
    public string TrashTag;
    public string TrashTagSalah1;
    public string TrashTagSalah2;

    [SerializeField] private KeyCode trashKey = KeyCode.A;
    [SerializeField] private Light trueLight;
    [SerializeField] private Light falseLight;

    private bool isCollidingWithPlastik = false;
    private bool isCollidingWithSalah1 = false;
    private bool isCollidingWithSalah2 = false;

    private bool canPressButton = false;

    public GameObject addPoint;
    public GameObject minPoint;

    private void Start()
    {
        Time.timeScale = 1f;
        if (trueLight != null)
            trueLight.enabled = false;

        if (falseLight != null)
            falseLight.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(trashKey) && isCollidingWithPlastik && !canPressButton)
        {

            ScoreManager.Instance.AddScore(10);
            addPoint.SetActive(true);

            if (trueLight != null)
                trueLight.enabled = true;
            // Debug.Log("Points: " + score);

            canPressButton = true;
        }

        if(Input.GetKeyDown(trashKey) && !isCollidingWithPlastik && !canPressButton)
        {
            if(isCollidingWithSalah1 || isCollidingWithSalah2)
            {
                ScoreManager.Instance.MinScore(10);
                minPoint.SetActive(true);
                if (falseLight != null)
                    falseLight.enabled = true;
                canPressButton = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TrashTag))
        {
            isCollidingWithPlastik = true;
            canPressButton = false;
        }

        if (other.CompareTag(TrashTagSalah1))
        {
            isCollidingWithSalah1 = true;
            canPressButton = false;
        }

        if (other.CompareTag(TrashTagSalah2))
        {
            isCollidingWithSalah2 = true;
            canPressButton = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TrashTag))
        {
            isCollidingWithPlastik = false;
            canPressButton = false;
            addPoint.SetActive(false);

            if (trueLight != null)
                trueLight.enabled = false;

        }

        if (other.CompareTag(TrashTagSalah1))
        {
            isCollidingWithSalah1 = false;
            canPressButton = false;
            minPoint.SetActive(false);

            if (falseLight != null)
                falseLight.enabled = false;
        }

        if (other.CompareTag(TrashTagSalah2))
        {
            isCollidingWithSalah2 = false;
            canPressButton = false;
            minPoint.SetActive(false);

            if (falseLight != null)
                falseLight.enabled = false;
        }
    }
}
