using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    private Animator _animator;

    void Start () {
        _animator = GetComponent<Animator>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _animator.SetBool("IsDoorOpen", true);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
