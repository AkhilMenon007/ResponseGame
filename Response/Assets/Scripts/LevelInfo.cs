using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="LevelInfo")]
public class LevelInfo : ScriptableObject
{

    public int levelNumber;
    [TextArea(0,100)]
    public string infoText;
}
