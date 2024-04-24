using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Camera cam;
    List<Collider> colliders = new List<Collider>();
    [SerializeField]
    GameObject turret;
    [SerializeField]
    GameObject cursor;
    [SerializeField]
    CinemachineVirtualCamera cam1;
    [SerializeField]
    GameObject shake;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cursor.transform.position = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        setMouseLocation();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = VytvorRay();

            if (Physics.Raycast(ray, out RaycastHit hit, 150f))
            {
                if(hit.collider.gameObject.GetComponentInParent<EnemyStats>())
                {
                    var enemy = hit.collider.GetComponentInParent<EnemyStats>();

                    enemy.TakeDamage(10);
                    GameManager.Instance.Body();
                }
                
            }
        }
        else if(Input.GetMouseButtonDown(1))
        {
           
            Ray ray = VytvorRay();

            if (Physics.Raycast(ray, out RaycastHit hit, 150f))
            {
                Debug.Log("asdf");
                colliders = Physics.OverlapSphere(hit.point, 6f).ToList();
                Debug.Log(colliders.Count + " " + GameManager.Instance.bodiky);
                if (GameManager.Instance.bodiky >= 5)
                {
                    GameManager.Instance.bodiky -= 5;
                    GameManager.Instance.NactiBody();
                    Instantiate(shake, hit.point, Quaternion.identity);



                    foreach (var collider in colliders)
                    {
                        Debug.Log("damg");
                        var enemy = collider.GetComponentInParent<EnemyStats>(); if (enemy != null)
                        {
                            enemy.TakeDamage(10);
                        }
                    }



                }
            }
        }else if(Input.GetKeyDown(KeyCode.E)) 
        {
            Ray ray = VytvorRay();

            if (Physics.Raycast(ray, out RaycastHit hit, 150f))
            {
                if(hit.collider.gameObject.tag == "Ground")
                {
                    if (GameManager.Instance.bodiky >= 100)
                    {
                        GameManager.Instance.bodiky -= 100;
                        GameManager.Instance.NactiBody();

                        Instantiate(turret, hit.point, Quaternion.identity);
                        Instantiate(shake, hit.point, Quaternion.identity);
                    }
                }
            }

        }
    }

    private Ray VytvorRay()
    {
        
        return cam.ScreenPointToRay(Input.mousePosition);
        
    }

    private void setMouseLocation()
    {
        var asdf = cam.ScreenToWorldPoint(Input.mousePosition);
        cam.transform.LookAt(asdf);
            
    }
}

