using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [SerializeField]
    private Dropdown dropdown = null;
    [SerializeField]
    private Text nameText = null;
    [SerializeField]
    private Text ageText = null;
    [SerializeField]
    private Text speedText = null;
    [SerializeField]
    private Text spawnSpeedText = null;


    [SerializeField]
    private GameObject gameStartMenu = null;
    [SerializeField]
    private GameObject firstLevelInfo = null;

    public void Next() 
    {
        var gender = dropdown.value;
        var name = nameText.text;

        if (name == "" || gender ==0) 
        {
            return;
        }

        int age;
        float spawnSpeed,speed;

        if(int.TryParse(ageText.text,out age) && float.TryParse(speedText.text,out speed) && float.TryParse(spawnSpeedText.text,out spawnSpeed)) 
        {
            DataSaver.playerName = name;
            DataSaver.playerGender = (DataSaver.Gender)(gender - 1);
            DataSaver.playerAge = age;
            BallSpawner.instance.spawnDelay = spawnSpeed;
            BallSpawner.instance.speed = speed;

            gameStartMenu.SetActive(false);
            firstLevelInfo.SetActive(true);
        }
        else 
        {
            return;
        }
    }
}
