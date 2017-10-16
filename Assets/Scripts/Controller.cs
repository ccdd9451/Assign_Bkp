#define DVORAK

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float desiredSpeed = 1.0F;
    public GameObject skeleton;
#if DVORAK
    enum KEYBOARD_INPUT : int {
		LEFT = KeyCode.A,
		RIGHT = KeyCode.E,
		FORWARD = KeyCode.Comma,
		BACKWARD = KeyCode.O,
        ACCEL = KeyCode.LeftShift,
    }
#else
    enum KEYBOARD_INPUT : int
    {
        LEFT = KeyCode.A,
        RIGHT = KeyCode.D,
        FORWARD = KeyCode.W,
        BACKWARD = KeyCode.S,
        ACCEL = KeyCode.LeftShift,
    }
#endif

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = Vector3.zero;
        float mvMulIndex = 1;
        if (Input.GetKey((KeyCode)KEYBOARD_INPUT.RIGHT))
        {
            movement.x += 1;
        }
        if (Input.GetKey((KeyCode)KEYBOARD_INPUT.LEFT))
        {
            movement.x -= 1;
        }
        if (Input.GetKey((KeyCode)KEYBOARD_INPUT.FORWARD))
        {
            movement.z += 1;
        }
        if (Input.GetKey((KeyCode)KEYBOARD_INPUT.BACKWARD))
        {
            movement.z -= 1;
        }
        if (Input.GetKey((KeyCode)KEYBOARD_INPUT.ACCEL))
        {
            mvMulIndex = 2;
        }
        movement = movement.normalized * desiredSpeed * mvMulIndex;
        skeleton.GetComponent<Agent>().desiredVelo = movement;
    }
}
