using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber2 : MonoBehaviour
{
    private GameObject selectedObject;
    private float objectZ;
    public string destinationTag = "DropArea";

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null && hit.collider.CompareTag("drag"))
                {
                    selectedObject = hit.collider.gameObject;
                    objectZ = hit.point.y; // Store the y position of the hit point
                    Cursor.visible = false;
                }
            }
            else
            {
                // Handle object drop and snapping logic here
                RaycastHit hit = CastRay();

                if (hit.collider != null && hit.collider.CompareTag(destinationTag))
                {
                    selectedObject.transform.position = hit.collider.transform.position;
                }

                Cursor.visible = true;
                selectedObject = null;
            }
        }

        if (selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, objectZ, worldPosition.z); // Use the stored y position
        }
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}
