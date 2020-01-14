using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TotalError : MonoBehaviour
{
    [SerializeField]
    private Text text = null;
    void Start()
    {
        LevelBehaviour.OnLevelCompleted += () => {text.text = $"Total error : {LevelBehaviour.currentLevel.missCount + LevelBehaviour.currentLevel.errorCount }"; };
    }
}
