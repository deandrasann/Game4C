using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    Vector3 offset;
    Vector3 originalPosition;
    public string destinationTag = "DropArea";
    private Collider objectCollider;
    public float raycastMaxDistance = 100f; // Adjust the max distance for raycasting if necessary

    public float heightOffset = 1f;

    AudioMainMenu audio;

    private void Start()
    {
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioMainMenu>();

        objectCollider = GetComponent<Collider>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnMouseDown()
    {
        originalPosition = transform.position;
        offset = transform.position - MouseWorldPosition();
        objectCollider.enabled = false;
    }

    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    private void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit[] hits = Physics.RaycastAll(rayOrigin, rayDirection, raycastMaxDistance);

        bool snappedToTarget = false;

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag(destinationTag))
            {
                transform.position = hit.transform.position + new Vector3(0, heightOffset, 0);
                CompletionManager.Instance.AddScore(1);
                snappedToTarget = true;
                audio.PlaySFX(audio.clickButton);
                break; // Exit the loop once we've found and snapped to the correct tag
            }
        }

        if (!snappedToTarget)
        {
            transform.position = originalPosition;
        }

        objectCollider.enabled = true;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }


}
