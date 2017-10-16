using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    Animator anim;

    public Vector3 desiredVelo = Vector3.zero;
    Vector3 currentVelo = Vector3.zero;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        currentVelo = Vector3.Lerp(currentVelo, desiredVelo, Time.deltaTime);
        if (currentVelo.magnitude > 0.1)
        {
            anim.SetBool("move", true);
            anim.SetFloat("velx", currentVelo.x);
            anim.SetFloat("vely", currentVelo.z);
        }
        else
        {
            anim.SetBool("move", false);
        }
    }
}
