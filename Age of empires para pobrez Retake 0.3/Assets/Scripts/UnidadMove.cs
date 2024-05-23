using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnidadMove : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    NavMeshAgent agent;
    public LayerMask ground;
    void Start()
    {
        cam=Camera.main;
        agent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray=cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit,Mathf.Infinity, ground))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
