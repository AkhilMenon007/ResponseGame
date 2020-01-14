using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : LevelBehaviour
{
    [SerializeField]
    private CollectionBox box = null;
    [SerializeField]
    private SpawnData correctColor = null;

    [SerializeField]
    private SpawnData[] incorrectColors = null;

    public override CollectionBox GetBox(SpawnData other)
    {
        if (other == box.spawnData)
            return box;
        return null;
    }


    public override bool IsValidColor(SpawnData other)
    {
        return correctColor == other;
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

        if (spawnedBallCount >= maxBallCount)
        {
            return;
        }

        for (int i = 0; i < spawnLocations.Length; i++)
        {
            if (range.Contains(i))
            {
                spawnedBallCount++;
                SpawnBall(correctColor, spawnLocations[i]);
            }
            else
            {
                SpawnBall(incorrectColors[Random.Range(0, incorrectColors.Length)], spawnLocations[i]);
            }
        }
    }
}
