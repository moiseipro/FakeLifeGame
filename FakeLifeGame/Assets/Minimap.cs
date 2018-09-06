using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    public Transform Player;

    public int[] zoomFactor;
    public int currentZoomLevel;
    void Start()
    {
        currentZoomLevel = 1;
        this.GetComponent<Camera>().orthographicSize = zoomFactor[currentZoomLevel];
    }
        void LateUpdate () {
        Vector3 newPosition = Player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        if(Input.GetKeyDown("j") && currentZoomLevel > 0)
            this.GetComponent<Camera>().orthographicSize = zoomFactor[--currentZoomLevel];
        if (Input.GetKeyDown("k") && currentZoomLevel <= zoomFactor.Length - 1)
            this.GetComponent<Camera>().orthographicSize = zoomFactor[++currentZoomLevel];
    }  
}
