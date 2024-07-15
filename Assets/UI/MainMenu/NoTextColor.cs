using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoTextColor : MonoBehaviour
{
    public TMP_Text text;
    public Color color;

    private void Start()
    {
        text.outlineColor = Color.red;
    }
}
