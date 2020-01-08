using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    [SerializeField]
    private LayerMask hitLayer=1;

    [SerializeField]
    private LayerMask groundLayer=1;

    private bool grabbing = false;

    private Ball currentBall;

    public static Dragger instance;

    Camera cam;


    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        else 
        {
            Destroy(this);
        }
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            CheckBallCollision();
        }
        if (Input.GetMouseButton(0)) 
        {
            if (grabbing) 
            {
                Drag();
            }
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            if (grabbing) 
            {
                Drop();
            }
            grabbing = false;
        }
    }

    private void Drag()
    {
        if(currentBall==null)
        {
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit,1000f, groundLayer)) 
        {
            currentBall.transform.position = new Vector3(hit.point.x,0f,hit.point.z);
        }
    }

    private void Drop()
    {
        currentBall?.Miss();
        currentBall = null;
    }

    private void CheckBallCollision()
    {
        RaycastHit hit;

        if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition),out hit, 1000f, hitLayer,QueryTriggerInteraction.Collide)) 
        {
            var ball = hit.collider.GetComponent<Ball>();
            if (ball != null) 
            {
                currentBall = ball;
                currentBall.Grab();
                currentBall.OnDestroyed += (_ball) => 
                {
                    if(currentBall == _ball) 
                    {
                        currentBall = null;
                    }
                };
                grabbing = true;
            }
        }
    }
}
