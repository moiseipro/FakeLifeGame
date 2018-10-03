using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDistance : MonoBehaviour
{

    public float rayDistance;

    void Update()
    {
        Debug.DrawRay(transform.position + Vector3.up, transform.forward * rayDistance , Color.red);

        if (Physics.Raycast(transform.position + transform.up, transform.forward, rayDistance))
        {
            Debug.Log("test");
        }
    }
}