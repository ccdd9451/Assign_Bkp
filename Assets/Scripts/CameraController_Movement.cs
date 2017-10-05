using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Movement : MonoBehaviour {

    #region Attributes

	float rotationX = 0F;
	float rotationY = 0F;
	Quaternion originalRotation;
	Rigidbody rb;
    GameObject activatedObstacle = null;


	public float sensitivityX = 5F;
	public float sensitivityY = 5F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -90F;
	public float maximumY = 90F;
	public float maxSpeed = 10;
    public GameObject NavGroup;

    #endregion

    #region Enums
    public enum KEYBOARD_INPUT : int {
		DOWN = KeyCode.LeftShift,
		UP = KeyCode.Space,
		LEFT = KeyCode.A,
		RIGHT = KeyCode.E,
		FORWARD = KeyCode.Comma,
		BACKWARD = KeyCode.O,
		DIRECTOR = KeyCode.Mouse1,
		ACTIVATOR = KeyCode.Mouse0,
        CANCELCMDS = KeyCode.Q,
        OBSDOWN = KeyCode.DownArrow,
        OBSUP = KeyCode.UpArrow,
        OBSLEFT = KeyCode.LeftArrow,
        OBSRIGHT = KeyCode.RightArrow,
    }
    #endregion


    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        originalRotation = transform.localRotation;
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Read the mouse input axis
		rotationX += Input.GetAxis("Mouse X") * sensitivityX;
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		// rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
		rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
		Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
		transform.localRotation = originalRotation * xQuaternion * yQuaternion;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
        CameraControl();
        ObsControl();


        if (Input.GetKeyDown ((KeyCode)KEYBOARD_INPUT.DIRECTOR)) {
			Directing ();
		}
		if (Input.GetKeyDown ((KeyCode)KEYBOARD_INPUT.ACTIVATOR)) {
			ActivateOrCancel ();
		}
        /*if (Input.GetKey((KeyCode)KEYBOARD_INPUT.ACTIVATOR))
        {
            Cancel();
        }*/
    }

    private void ObsControl()
    {
        if (activatedObstacle)
        {
            Vector3 movement = Vector3.zero;
            if (Input.GetKey((KeyCode)KEYBOARD_INPUT.OBSRIGHT))
            {
                movement.x += 1;
            }
            if (Input.GetKey((KeyCode)KEYBOARD_INPUT.OBSLEFT))
            {
                movement.x -= 1;
            }
            if (Input.GetKey((KeyCode)KEYBOARD_INPUT.OBSUP))
            {
                movement.z += 1;
            }
            if (Input.GetKey((KeyCode)KEYBOARD_INPUT.OBSDOWN))
            {
                movement.z -= 1;
            }
            movement = movement * maxSpeed;
            movement = transform.rotation * movement;
            movement.y = 0;
            activatedObstacle.GetComponent<Rigidbody>().velocity = movement;
        }
    }

    private void CameraControl()
    {
        Vector3 movement = Vector3.zero;
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
        movement = transform.rotation * movement;
        movement.y = 0;
        if (Input.GetKey((KeyCode)KEYBOARD_INPUT.UP))
        {
            movement.y += 1;
        }
        if (Input.GetKey((KeyCode)KEYBOARD_INPUT.DOWN))
        {
            movement.y -= 1;
        }
        movement = movement * maxSpeed;
        rb.velocity = movement;
    }
	private void Directing ()
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position,
			    transform.forward, out hit, 500)) {
			Debug.DrawLine (transform.position, hit.point);
            foreach (SimpleNavmeshAgent agent
                in NavGroup.GetComponentsInChildren<SimpleNavmeshAgent>())
            {
                Debug.Log("hitted at" + hit.point.ToString());
                agent.SetDestination(hit.point);
            }
		}
	}

    private float lastSucceedOperationTime = 0;
	private void ActivateOrCancel ()
	{
        RaycastHit hit;
		if (Physics.Raycast (transform.position,
			transform.forward, out hit, 500)) {

            GameObject hittedObstacle = hit.transform.gameObject;
            if (Time.time - lastSucceedOperationTime > 0.3) {
                if (hittedObstacle.tag == "Obs_M")
                {
                    
                    if (hittedObstacle.Equals(activatedObstacle))
                    {
                        DeactivateCurrent();
                    }
                    else
                    {
                        DeactivateCurrent();
                        ActivatedObstacle(hittedObstacle);
                    }
                }
                else
                {
                    DeactivateCurrent();
                }
                lastSucceedOperationTime = Time.time;
            }
		}
	}

    private void ActivatedObstacle(GameObject obstacle)
    {
        Debug.Log("activating");
        activatedObstacle = obstacle;
        activatedObstacle.GetComponent<ObstacleControl>().SetOnCommand(true);
    }
    private void DeactivateCurrent()
    {
        Debug.Log("deactivating");
        if (activatedObstacle)
        {
            activatedObstacle.GetComponent<ObstacleControl>().SetOnCommand(false);
            activatedObstacle = null;
        }
    }
}
