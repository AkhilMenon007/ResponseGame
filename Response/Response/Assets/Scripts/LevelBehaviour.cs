using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class LevelBehaviour : MonoBehaviour
{

    public LevelInfo levelInfo = null;


    [SerializeField]
    protected Ball ballObject = null;
    [SerializeField]
    protected float speed 
    {
        get 
        {
            return BallSpawner.instance.speed;
        }
    }
    [SerializeField]
    protected int maxBallCount=36;
    protected int spawnedBallCount = 0;
    protected int finishedballCount = 0;

    int totalBallSpawnedCount = 0;
    int totalBallFinishedCount = 0;

    [HideInInspector]
    public int missCount = 0;
    [HideInInspector]
    public int errorCount = 0;

    public static DateTime previousActionTimeCompleted;

    public static LevelBehaviour currentLevel;

    public LevelBehaviour nextLevel;

    public static Action OnLevelCompleted;
    public static Action OnGameOver;

    public abstract bool IsValidColor(SpawnData other);
    public abstract CollectionBox GetBox(SpawnData other);
    public abstract void Spawn(Transform[] spawnLocations);

    public virtual void OnEnable()
    {
        if (currentLevel == null)
            currentLevel = this;
        BallSpawner.instance.BallSpawnEvent += Spawn;
        previousActionTimeCompleted = DateTime.Now;
    }

    public virtual void OnDisable()
    {
        BallSpawner.instance.BallSpawnEvent -= Spawn;
    }

    public void UpgradeLevel() 
    {
        if(nextLevel != null) 
        {
            ChangeLevel(nextLevel);
        }

    }


    protected Ball SpawnBall(SpawnData spawnData,Transform position) 
    {
        totalBallSpawnedCount++;

        var spawnedObj = Instantiate(ballObject, position.position, position.rotation);
        spawnedObj.transform.parent = position;
        spawnedObj.SetColor(spawnData);
        spawnedObj.level = levelInfo.levelNumber;
        spawnedObj.OnBallAccepted += AcceptBall;
        spawnedObj.OnBallError += ErrorBall;
        spawnedObj.OnBallMissed += MissBall;
        return spawnedObj;
    }


    public static void ChangeLevel(LevelBehaviour targetLevel) 
    {
        currentLevel = targetLevel;
    }


    public static List<int> GetSpawnIndices() 
    {
        List<int> result = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            result.Add(i);
        }

        for (int i = 0; i < 2; i++)
        {
            result.RemoveAt(Random.Range(0, result.Count));
        }

        return result;
    }
    public void AcceptBall(Ball ball)
    {
        totalBallFinishedCount++;
        finishedballCount++;
        CheckLevelOver();
    }

    public void ErrorBall(Ball ball)
    {
        totalBallFinishedCount++;
        errorCount++;
        finishedballCount++;
        CheckLevelOver();
    }
    public void MissBall(Ball ball)
    {
        totalBallFinishedCount++;
        if (IsValidColor(ball.spawnData)) 
        {
            missCount++;
        }
        finishedballCount++;
        CheckLevelOver();
    }
    private void CheckLevelOver()
    {
        if (totalBallFinishedCount >= totalBallSpawnedCount)
        {
            OnLevelCompleted?.Invoke();
            UpgradeLevel();
        }
    }
}
