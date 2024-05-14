using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DarkCursor : MonoBehaviour
{
    [SerializeField]
    Light Light;
    Camera cam;
    [SerializeField]
    GameObject camera_obj;
    // Start is called before the first frame update
    void Start()
    {
        cam = camera_obj.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = VytvorRay();

        if (Physics.Raycast(ray, out RaycastHit hit, 150f))
        {
            Light.transform.position = hit.point + new Vector3(0,1,0);
        }
    }

    private Ray VytvorRay()
    {

        return cam.ScreenPointToRay(Input.mousePosition);

    }
}
