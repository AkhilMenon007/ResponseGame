using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorMessagePanel : MonoBehaviour
{
    [SerializeField]
    private float displayDuration = 1.414f;

    [SerializeField]
    private GameObject errorPanel = null;

    private Coroutine displayRoutine;

    private void OnEnable()
    {
        BallSpawner.instance.BallErrorEvent += PanelTrigger;
    }
    private void OnDisable()
    {
        BallSpawner.instance.BallErrorEvent -= PanelTrigger;
    }

    private void PanelTrigger(Ball ball) 
    {
        if(displayRoutine != null) 
        {
            StopCoroutine(displayRoutine);
        }
        displayRoutine = StartCoroutine(DisplayCoroutine());
    }


    private IEnumerator DisplayCoroutine() 
    {
        errorPanel.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        errorPanel.SetActive(false);
    }

}
