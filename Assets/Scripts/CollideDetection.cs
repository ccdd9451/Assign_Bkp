using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollideDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnCollisionStay(Collision collision)
    {
        GameObject target = collision.gameObject;
        if (//OnNavigating &&
            target.tag == "Agent")
        {
            if (!target.GetComponent<SimpleNavmeshAgent>().OnNavigating)
            {
                Debug.Log("agent collision detected");
                GetComponent<NavMeshAgent>().isStopped = true;
                GetComponent<SimpleNavmeshAgent>().OnNavigating = false;
            }
        }
    }
}
