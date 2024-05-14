using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    Light spotLight;
    NavMeshAgent agent;
    Canvas hpCanvas;
    void Start()
    {
        spotLight = GetComponentInChildren<Light>();
        hpCanvas = GetComponentInChildren<Canvas>();
        if(SceneManager.GetActiveScene().name== "Level3")
        {
            hpCanvas.enabled = false;
            spotLight.enabled = false;
        }
        agent = GetComponent<NavMeshAgent>();
        Invoke("SetDestination", 0.5f);
    }

    private void SetDestination()
    {
        agent.SetDestination(GameManager.Instance.GetClosestBrain(transform.position).position);
    }
}
