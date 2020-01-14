using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallMissPanel : MonoBehaviour
{

    [SerializeField]
    private Text missPanel = null;

    private void OnEnable()
    {
        LevelBehaviour.OnLevelCompleted += ResetCount;
        BallSpawner.instance.BallMissEvent += SetCountText;
    }
    private void OnDisable()
    {
        BallSpawner.instance.BallMissEvent -= SetCountText;
        LevelBehaviour.OnLevelCompleted -= ResetCount;
    }

    private void SetCountText(Ball obj)
    {
        missPanel.text = $"Miss Count : {LevelBehaviour.currentLevel.missCount}";
    }

    private void ResetCount() 
    {
        missPanel.text = $"Miss Count : 0";
    }

}
