using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCamController : MonoBehaviour
{
    public Transform target;

    public float minHeight, maxHeight;


    void Start()
    {
        
    }

    void Update()
    {
        /*transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);

        float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z); ;*/

        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);
    }
}
