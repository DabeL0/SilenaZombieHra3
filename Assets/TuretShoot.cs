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
    [SerializeField]
    AudioSource ShootSound;
    [SerializeField]
    LineRenderer lineRenderer1;
    [SerializeField]
    LineRenderer lineRenderer2;
    [SerializeField]
    GameObject laserOrigin1;
    [SerializeField]
    GameObject laserOrigin2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetClosest());
        Invoke("KillYourself", 60);
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


            colliders = Physics.OverlapSphere(transform.position, 10f).ToList();
            
            
            foreach (var collider in colliders)
            {
                Debug.Log("damg");
                Debug.DrawLine(transform.position, collider.gameObject.transform.position, Color.red);
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
                lineRenderer2.enabled = true;
                lineRenderer1.enabled = true;
                
                enemy.TakeDamage(10);
                GameManager.Instance.Body();
                transform.LookAt(enemy.gameObject.transform.position);
                
                    lineRenderer1.SetPosition(0, laserOrigin1.transform.position);
                    
                    lineRenderer1.SetPosition(1, enemy.transform.position + new Vector3(0, enemy.transform.position.y/2,0));

                lineRenderer2.SetPosition(0, laserOrigin2.transform.position);

                lineRenderer2.SetPosition(1, enemy.transform.position + new Vector3(0, enemy.transform.localScale.y / 2, 0));

                ShootSound.Play();
                distance = 10000;
                yield return new WaitForSeconds(0.2f);
                lineRenderer1.enabled = false;
                lineRenderer2.enabled = false;

            }
            
            yield return new WaitForSeconds(0.8f);

        }
       
    }

    void KillYourself()
    {
        Instantiate(shake, Vector3.one, Quaternion.identity);
        Destroy(gameObject);
    }
}
