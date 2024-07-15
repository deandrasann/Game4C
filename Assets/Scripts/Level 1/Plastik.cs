using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastik : MonoBehaviour
{
    public int lootAmount = 1; 
    public int Loot()
    {
        gameObject.SetActive(false);
        return lootAmount;
    }
}
