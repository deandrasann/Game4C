using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController1 : MonoBehaviour
{
    private Animator doorAnim;
    private bool doorOpen = false;

    private void Awake() {
        doorAnim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation(){
        if(!doorOpen){
            doorAnim.Play("DDKananOpen", 0, 0.0f);
            doorOpen = true;
        }
        else{
            doorAnim.Play("DDKananClose", 0, 0.0f);
            doorOpen = false;
        }
    }
}
