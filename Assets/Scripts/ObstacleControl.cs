using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControl : MonoBehaviour {

    Rigidbody rb;
    public Vector3 movement = Vector3.zero;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
	}

    // Update is called once per frame
    void FixedUpdate() {
        Debug.Log("velo: " + rb.velocity.ToString());

        movement = Vector3.Lerp(movement, Vector3.zero, Time.deltaTime*5);
        movement.y = rb.velocity.y-100;
        rb.velocity = movement;
        //rb.AddForce((float)980*Vector3.down);
	}

    public void SetOnCommand(bool b)
    {
        if (b)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color = Color.yellow;
        }
        else
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color = Color.red;
        }
    }
}
