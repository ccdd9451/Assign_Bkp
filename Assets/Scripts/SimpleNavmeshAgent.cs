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
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ToggleDelegateOnNavigating(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (OnNavigating)
        {
            Vector3 lastpos = transform.position;

            if (!Agent.pathPending &&
                Agent.pathStatus == NavMeshPathStatus.PathComplete &&
                Agent.remainingDistance < 0.3)
            {
                Agent.isStopped = true;
                OnNavigating = false;
                rb.velocity = Vector3.zero;
                ToggleDelegateOnNavigating(false);
            }
            transform.position = Agent.nextPosition;

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
                ToggleDelegateOnNavigating(false);
            }
            transform.position = Agent.nextPosition;

        }
    }

    public void SetDestination(Vector3 dest)
    {
        Agent.ResetPath();
        Agent.nextPosition = transform.position;
        Agent.SetDestination(dest);
        ToggleDelegateOnNavigating(true);
        
    }

    private void ToggleDelegateOnNavigating(bool b)
    {
        OnNavigating = b;
        rb.isKinematic = !b;
        Agent.updatePosition = !b;
        Agent.updateRotation = !b;
        Agent.updateUpAxis = !b;
    }
}
