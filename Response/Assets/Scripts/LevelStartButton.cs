using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStartButton : MonoBehaviour
{
    [SerializeField]
    private LevelBehaviour level1=null;

    [SerializeField]
    private GameObject levelStartCanvas = null;

    [SerializeField]
    private GameObject gameOverCanvas = null;

    [SerializeField]
    private Text levelInfoText = null;

    // Start is called before the first frame update
    void OnEnable()
    {
        LevelBehaviour.currentLevel = level1;
        levelInfoText.text = level1.levelInfo.infoText;
        LevelBehaviour.OnLevelCompleted += LevelCompleted;
        LevelBehaviour.OnGameOver += GameOver;
    }

    private void GameOver() 
    {
        gameOverCanvas.SetActive(true);
    }

    private void LevelCompleted()
    {
        if (LevelBehaviour.currentLevel.nextLevel == null) 
        {
            LevelBehaviour.OnGameOver?.Invoke();
            return;
        }
        levelInfoText.text = LevelBehaviour.currentLevel.nextLevel.levelInfo.infoText;
        LevelBehaviour.currentLevel.gameObject.SetActive(false);
        levelStartCanvas.SetActive(true);
    }

    public void StartLevel() 
    {
        levelStartCanvas.SetActive(false);
        LevelBehaviour.currentLevel.gameObject.SetActive(true);
    }
}
