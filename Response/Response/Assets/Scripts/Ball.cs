﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Ball : MonoBehaviour
{
    public SpawnData spawnData { get; private set; }

    public Action<Ball> OnBallAccepted { get; set; }
    public Action<Ball> OnBallMissed { get; set; }
    public Action<Ball> OnBallError { get; set; }
    public Action<Ball> OnDestroyed { get; set; }


    public int level;

    public bool isMoving = true;

    [HideInInspector]
    public Vector3 pickedUpPosition;
    [HideInInspector]
    public Vector3 droppedPosition;

    [HideInInspector]
    public DateTime actionStartTime;

    [HideInInspector]
    public DateTime actionEndTime;

    public bool isValidBall { get; set; }
    private void Update()
    {
        if (isMoving) 
        {
            transform.position += transform.forward * BallSpawner.instance.speed * Time.deltaTime;
        }
    }

    public void Grab() 
    {
        isMoving = false;
        actionStartTime = DateTime.Now;
        pickedUpPosition = transform.position;
    }

    public void SetColor(SpawnData spawnData)
    {
        this.spawnData = spawnData;
        GetComponent<MeshRenderer>().material.color = spawnData.color;
        if (LevelBehaviour.currentLevel.IsValidColor(spawnData)) 
        {
            isValidBall = true;
        }
        else 
        {
            isValidBall = false;
        }
    }


    public void Accept() 
    {
        droppedPosition = LevelBehaviour.currentLevel.GetBox(spawnData).transform.position;
        actionEndTime = DateTime.Now;
        OnBallAccepted?.Invoke(this);
        BallSpawner.instance.BallAcceptEvent?.Invoke(this);
        LevelBehaviour.previousActionTimeCompleted = DateTime.Now;
        Destroy(gameObject);
    }

    public void Error() 
    {
        droppedPosition = transform.position;
        actionEndTime = DateTime.Now;
        OnBallError?.Invoke(this);
        BallSpawner.instance.BallErrorEvent?.Invoke(this);
        LevelBehaviour.previousActionTimeCompleted = DateTime.Now;
        Destroy(gameObject);
    }

    public void Miss() 
    {
        OnBallMissed?.Invoke(this);
        BallSpawner.instance.BallMissEvent?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

}
