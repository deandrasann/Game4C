using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogLine
{
    public DialogCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialog
{
    public List<DialogLine> dialogueLines = new List<DialogLine>();
}

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialogue;

    public void TriggerDialogue()
    {
        DialogManager.Instance.StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TriggerDialogue();
        }
    }
}
