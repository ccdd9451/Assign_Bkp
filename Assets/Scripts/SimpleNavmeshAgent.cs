using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SimpleNavmeshAgent : MonoBehaviour {

    NavMeshAgent Agent;
    Rigidbody rb;
    public bool OnNavigating = false;


	// Use this for initialization
	void Start () {
        Agent = GetComponent<NavMeshAgent>();
        Agent.updatePosition = false;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (OnNavigating)
        {
            transform.position = Agent.nextPosition;
            /*if (Agent.pathStatus == NavMeshPathStatus.PathComplete && Agent.remainingDistance < 0.1)
            {
                Debug.Log("Nav succeed");
                Agent.isStopped = true;
                OnNavigating = false;
            }*/
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;
        if (target.tag == "Agent")
        {
            if (!target.GetComponent<SimpleNavmeshAgent>().OnNavigating)
            {
                Debug.Log("Nav stop by collide");
                Agent.isStopped = true;
                OnNavigating = false;
            }
        }
    }

    public void SetDestination(Vector3 dest)
    {
        Agent.SetDestination(dest);
        OnNavigating = true;
    }
}
