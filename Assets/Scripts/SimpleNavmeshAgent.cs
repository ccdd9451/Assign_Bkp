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
            Vector3 lastpos = transform.position;
            transform.position = Agent.nextPosition;

            if (!Agent.pathPending && 
                Agent.pathStatus == NavMeshPathStatus.PathComplete &&
                Agent.remainingDistance < 0.3)
            {
                Agent.isStopped = true;
                OnNavigating = false;
                rb.velocity = Vector3.zero;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        GameObject target = collision.gameObject;
        if (OnNavigating && target.tag == "Agent")
        {
            if (!target.GetComponent<SimpleNavmeshAgent>().OnNavigating)
            {
                Agent.isStopped = true;
                OnNavigating = false;
            }
        }
    }

    public void SetDestination(Vector3 dest)
    {
        Agent.ResetPath();
        Agent.nextPosition = transform.position;
        Agent.SetDestination(dest);
        Debug.Log("destination setted at:" +
            dest.ToString());

        OnNavigating = true;
    }
}
