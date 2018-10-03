using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    public float doorOpenAngle = 90.0f;
    public float doorCloseAngle = 0.0f;
    public float doorAnimSpeed = 2.0f;
    public float doorRangeClose = 2.0f;
    private Quaternion doorOpen = Quaternion.identity;
    private Quaternion doorClose = Quaternion.identity;
    private Transform playerTrans = null;

    public bool doorStatus = false; //false is close, true is open
    private bool doorGo = false; //for Coroutine, when start only one
    void Start()
    {
        doorStatus = false; //door is open, maybe change
                            //Initialization your quaternions
        doorOpen = Quaternion.Euler(0, 0, doorOpenAngle);
        doorClose = Quaternion.Euler(0, 0, doorCloseAngle);
        //Find only one time your player and get him reference
        playerTrans = GameObject.Find("Chal_Rig_WHorse").transform;
    }
    void Update()
    {

        //Calculate distance between player and door
        if (Vector3.Distance(playerTrans.position, this.transform.position) > doorRangeClose)
        {
            if (doorStatus)
            { //close door
                StartCoroutine(this.moveDoor(doorClose));
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
                if (doorStatus){}
                else
                { //open door
                    StartCoroutine(this.moveDoor(doorOpen));
                }
        }
    }
    public IEnumerator moveDoor(Quaternion dest)
    {
        doorGo = true;
        //Check if close/open, if angle less 4 degree, or use another value more 0
        while (Quaternion.Angle(transform.localRotation, dest) > 0.001f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, dest, Time.deltaTime * doorAnimSpeed);
            //UPDATE 1: add yield
            yield return null;
        }
        //Change door status
        doorStatus = !doorStatus;
        doorGo = false;
        //UPDATE 1: add yield
        yield return null;
    }
}
