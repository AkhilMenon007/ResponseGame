using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelBehaviour
{
    [SerializeField]
    private CollectionBox box = null;
    [SerializeField]
    private SpawnData correctColor = null;

    public override bool IsValidColor(SpawnData other) 
    {
        return correctColor == other;
    }
    public override CollectionBox GetBox(SpawnData other)
    {
        if (other == box.spawnData)
            return box;
        return null;
    }

    private void Awake()
    {
        currentLevel = this;
    }
    public override void OnEnable()
    {
        base.OnEnable();
        box.SetColor(correctColor);
        OnLevelCompleted += UpgradeLevel;
    }

    public override void OnDisable()
    {
        OnLevelCompleted -= UpgradeLevel;
        base.OnDisable();
    }

    public override void Spawn(Transform[] spawnLocations)
    {
        List<int> range = GetSpawnIndices();

        if(spawnedBallCount >= maxBallCount) 
        {
            return;
        }

        foreach (var index in range)
        {
            spawnedBallCount++;
            SpawnBall(correctColor, spawnLocations[index]);
        }
    }

}
