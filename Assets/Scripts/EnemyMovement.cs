using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Invoke("SetDestination", 0.5f);
    }

    private void SetDestination()
    {
        agent.SetDestination(GameManager.Instance.GetClosestBrain(transform.position).position);
    }
}
