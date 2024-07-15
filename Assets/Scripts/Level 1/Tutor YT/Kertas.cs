using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kertas : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        PickUpObj pickUpObj = other.GetComponent<PickUpObj>();
        if (pickUpObj.seeKertas == true){
            // playerInventory.KertasCollected();
            gameObject.SetActive(false);
        }
    }
}
