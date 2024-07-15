using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDoorRC : MonoBehaviour
{
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;

    private DoorController1 raycastedObj1;
    private EnemyAI enemyAI;

    private bool doOnce;

    private const string interactableTag = "InteractiveObject";

    private void Start() {
        enemyAI = gameObject.GetComponent<EnemyAI>();
    }

    private void Update(){
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;
        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask)){
            if(hit.collider.CompareTag(interactableTag)){
                if(!doOnce){
                    raycastedObj1 = hit.collider.gameObject.GetComponent<DoorController1>();
                    enemyAI.currentDest = enemyAI.destinations[Random.Range(0, enemyAI.destinations.Count)];
                }
                doOnce = true;

                

                // if(Input.GetKeyDown(openDoorKey)){
                //     raycastedObj.PlayAnimation();
                // }
            }
        }
    }
}
