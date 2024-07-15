using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 5f;
    public float destroyRange = -10f;

    void Update()
    {
        // Move the object downwards
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);

        // Optionally, you can check if the object is out of bounds and destroy it
        if (transform.position.y < destroyRange)
        {
            Destroy(gameObject);
        }
    }
}
