using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger1 : MonoBehaviour
{
    public Dialog dialogue;

    private void Start()
    {
        Time.timeScale = 1f;
        DialogManager.Instance.StartDialogue(dialogue);
    }
}
