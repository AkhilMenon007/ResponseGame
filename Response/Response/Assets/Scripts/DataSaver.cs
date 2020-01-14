using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class DataSaver : MonoBehaviour
{
    public enum Gender 
    {
        Male,
        Female,
        Other
    }
    public static string playerName = "";
    public static int playerAge = 20;
    public static Gender playerGender = Gender.Female;

    public List<SaveData> saveDatas = new List<SaveData>();
    private void Awake()
    {
        BallSpawner.instance.BallAcceptEvent += OnBallAccepted;
        BallSpawner.instance.BallErrorEvent += OnBallError;
        BallSpawner.instance.BallMissEvent += OnBallMissed;
        LevelBehaviour.OnGameOver += SaveGameData;
        LevelBehaviour.OnLevelCompleted += WriteSaveData;
    }

    private void SaveGameData()
    {
        string filePath = GetPath();
        if(!Directory.Exists(Application.dataPath + "/Data")) 
        {
            Directory.CreateDirectory(Application.dataPath + "/Data");
        }
        using (StreamWriter writer = File.CreateText(filePath))
        {
            writer.WriteLine($"Name,{playerName}");
            writer.WriteLine($"Age,{playerAge}");
            writer.WriteLine($"Gender,{playerGender.ToString()}");
            writer.WriteLine();

            writer.WriteLine("BallColor,ActionTime,ReactionTime,Distance,DropError,MissingError");
            int prev = 0;
            for (int i = 0; i < saveDatas.Count; i++)
            {
                if (prev < saveDatas[i].level) 
                {
                    prev = saveDatas[i].level;
                    writer.WriteLine("");
                    writer.WriteLine($"LEVEL {prev}");
                    writer.WriteLine("");
                }
                writer.WriteLine(saveDatas[i]);
            }
        }
    }

    private string GetPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Data/" + playerName + ".csv";
#else
        return Application.dataPath + "/Data/" + playerName + ".csv";
#endif
    }
    private void WriteSaveData()
    {
        foreach (var saveData in saveDatas)
        {
            Debug.Log(saveData.ToString());
        }
    }

    private void OnBallAccepted(Ball obj)
    {
        var saveData = new SaveData();

        saveData.actionTime = (float)(obj.actionEndTime - obj.actionStartTime).TotalMilliseconds;
        saveData.reactionTime = (float)(obj.actionStartTime - LevelBehaviour.previousActionTimeCompleted).TotalMilliseconds;
        saveData.distance = (obj.pickedUpPosition - obj.droppedPosition).magnitude;
        saveData.ballColor = obj.spawnData.colorName;
        saveData.level = obj.level;
        saveDatas.Add(saveData);
    }

    private void OnBallError(Ball obj)
    {
        var saveData = new SaveData();


        saveData.actionTime = (float)(obj.actionEndTime - obj.actionStartTime).TotalMilliseconds;
        saveData.reactionTime = (float)(obj.actionStartTime - LevelBehaviour.previousActionTimeCompleted).TotalMilliseconds;
        saveData.distance = (obj.pickedUpPosition - obj.droppedPosition).magnitude;
        saveData.dropError = true;
        saveData.level = obj.level;
        saveData.ballColor = obj.spawnData.colorName;
        saveDatas.Add(saveData);
    }

    private void OnBallMissed(Ball obj)
    {
        if (obj.isValidBall) 
        {
            var saveData = new SaveData();
            saveData.ballColor = obj.spawnData.colorName;
            saveData.missingError = true;
            saveData.level = obj.level;
            saveDatas.Add(saveData);
        }
    }
}

[Serializable]
public class SaveData 
{
    public string ballColor = "";
    public float actionTime = -1f;
    public float reactionTime = -1f;
    public float distance = -1f;
    public int level = 0;

    public bool dropError = false;
    public bool missingError = false;


    public override string ToString()
    {
        int missing = missingError ? 1 : 0;
        int drop = dropError ? 1 : 0;
        return $"{ballColor},{actionTime},{reactionTime},{distance},{drop},{missing}";
    }
}