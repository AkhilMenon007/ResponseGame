using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level3 : LevelBehaviour
{
    [SerializeField]
    private CollectionBox[] boxes = null;
    [SerializeField]
    private SpawnData[] correctColors = null;

    [SerializeField]
    private SpawnData[] incorrectColors = null;

    int[] colorCount;

    public override CollectionBox GetBox(SpawnData other)
    {
        return boxes.Where(e => e.spawnData == other).FirstOrDefault();
    }

    public override bool IsValidColor(SpawnData other)
    {
        return correctColors.Contains(other);
    }

    private void Awake()
    {
        colorCount = new int[correctColors.Length];
    }

    public override void OnEnable()
    {
        base.OnEnable();

        for (int i = 0; i < correctColors.Length; i++)
        {
            boxes[i].SetColor(correctColors[i]);
        }

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

                while (true)
                {
                    int selection = Random.Range(0, colorCount.Length);
                    if (colorCount[selection] < maxBallCount / colorCount.Length)
                    {
                        SpawnBall(correctColors[selection], spawnLocations[i]);
                        colorCount[selection]++;
                        break;
                    }
                }

            }
            else
            {
                SpawnBall(incorrectColors[Random.Range(0, incorrectColors.Length)], spawnLocations[i]);
            }
        }
    }

}
