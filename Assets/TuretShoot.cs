using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UIElements;
using Unity.Burst.CompilerServices;
using System;
using System.Linq;
using Unity.VisualScripting;


public class TuretShoot : MonoBehaviour
{
    [SerializeField]
    GameObject shake;
    List<Collider> colliders = new List<Collider>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetClosest());
        Invoke("KillYourself", 60f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GetClosest()
    {
        while (true)
        {
            int indexOfClosestBrain = 0;
            float distance = 10000;
            int i = 0;


            colliders = Physics.OverlapSphere(transform.position, 6f).ToList();
            foreach (var collider in colliders)
            {
                Debug.Log("damg");

                if (colliders[i].GetComponentInParent<EnemyStats>())
                {
                    float dist = Vector3.Distance(transform.position, collider.gameObject.transform.position);
                    if (dist < distance)
                    {
                        indexOfClosestBrain = i;
                        distance = dist;
                    }
                }



                i++;
            }
            var enemy = colliders[i - 1].GetComponentInParent<EnemyStats>(); if (enemy != null)
            {
                enemy.TakeDamage(10);
                GameManager.Instance.Body();
            }
            yield return new WaitForSeconds(2f);

        }
       
    }

    void KillYourself()
    {
        Instantiate(shake, Vector3.one, Quaternion.identity);
        Destroy(gameObject);
    }
}
