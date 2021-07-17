using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WaypointPatrol : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    [SerializeField()]
    private int currentWaypointIndex;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance) {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        };
    }
}
