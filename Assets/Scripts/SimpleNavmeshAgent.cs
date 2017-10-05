using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SimpleNavmeshAgent : MonoBehaviour {

    NavMeshAgent Agent;
    public GameObject InitTarget;
    public Vector3 target;
    public bool OnNavigating = false;


	// Use this for initialization
	void Start () {
        Agent = GetComponent<NavMeshAgent>();
        target = InitTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (OnNavigating)
        {
            Agent.SetDestination(target);
        }
    }
}
