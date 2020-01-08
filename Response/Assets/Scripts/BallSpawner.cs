using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public float spawnDelay=2f;
    public float speed = 1f;
    [SerializeField]
    private Transform[] spawnPoints = null;


    private Coroutine spawnCoroutine;

    public Action<Transform[]> BallSpawnEvent;

    public Action<Ball> BallMissEvent;
    public Action<Ball> BallAcceptEvent;
    public Action<Ball> BallErrorEvent;

    public static BallSpawner instance;

    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
        }
        else 
        {
            Destroy(this);
        }
    }

    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(spawnDelay);
            BallSpawnEvent?.Invoke(spawnPoints);
        }
    }

}
