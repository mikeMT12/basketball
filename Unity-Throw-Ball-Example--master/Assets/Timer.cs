using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float timerDuration = 20f;
    public Image image;
    float timer;
    public PLayerMovement pLayerMovement;

    void Start()
    {
        timer = timerDuration;
        StartCoroutine(time());
    }

    void Update()
    {

    }

    public IEnumerator time()
    {
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            // Animate timer from 1 to 0 
            //float normalizedTime = Mathf.Clamp01(timer / timerDuration);
            // Update slider
            //image.fillAmount = normalizedTime;
            yield return null;
        }
        Debug.Log("NO");
        pLayerMovement.StartCoroutine("MovePlayerToSuperPos");


    }
}