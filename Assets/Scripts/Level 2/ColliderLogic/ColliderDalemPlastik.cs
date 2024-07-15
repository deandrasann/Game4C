using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDalemPlastik : MonoBehaviour
{
    public string TrashTag;
    [SerializeField] private KeyCode trashKey = KeyCode.A;
    [SerializeField] private Light associatedLight;

    private bool isCollidingWithPlastik = false;
    private bool canPressButton = false;

    private void Start()
    {
        if (associatedLight != null)
            associatedLight.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(trashKey) && isCollidingWithPlastik && !canPressButton)
        {
            // Add points
            //score += 10;
            //scoreText.text = "Score: " + score;

            ScoreManager.Instance.AddScore(20);


            if (associatedLight != null)
                associatedLight.enabled = true;
            // Debug.Log("Points: " + score);

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

            if (associatedLight != null)
                associatedLight.enabled = false;
        }
    }
}
