using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
public class CollectionBox : MonoBehaviour
{
    public SpawnData spawnData;

    public void SetColor(SpawnData spawnData) 
    {
        this.spawnData = spawnData;
        GetComponent<MeshRenderer>().material.color = spawnData.color;
    }
    private void OnTriggerEnter(Collider other)
    {
        var ball = other.GetComponent<Ball>();
        if (ball != null) 
        {
            if(ball.spawnData == spawnData)
            {
                ball.Accept();
            }
            else 
            {
                ball.Error();
            }
        }
    }
}
