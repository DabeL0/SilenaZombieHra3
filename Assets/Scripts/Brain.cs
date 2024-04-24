using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision " + collision.gameObject);

        var enemy = collision.gameObject.GetComponent<EnemyStats>(); 
        if(enemy != null)
        {
            GameManager.Instance.EndGame();
        }
    }
}
