using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int KertasAmount { get; private set; }

    public void KertasCollected(){
        KertasAmount++;
    }
}
