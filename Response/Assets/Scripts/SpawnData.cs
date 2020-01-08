using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="BallSpawnData")]
public class SpawnData : ScriptableObject
{
    public int type;
    public float speed;
    public Color color;
    public string colorName;
}
