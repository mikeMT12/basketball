using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    [Header("ScoreUI")]
    [SerializeField] public int score;
    [SerializeField] private TextMesh scoreText;

    [Header("TimeUI")]
    [SerializeField] private float time;
    [SerializeField] private TextMesh timeText;

    [Header("Timer")]
    public float timerDuration = 20;


    [Header("SuperPos")]
    public PLayerMovement pLayerMovement;


    void Start()
    {
        time = timerDuration;
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }

    public void UpdateTmeUI()
    {
        int timeT = (int)time;
        timeText.text = timeT.ToString();
    }

    public IEnumerator Timer()
    {
        while (time >= 0)
        {
            //Debug.Log((int)Time.deltaTime);
            time -= Time.deltaTime;
            UpdateTmeUI();
            // Animate timer from 1 to 0 
            //float normalizedTime = Mathf.Clamp01(timer / timerDuration);
            //image.fillAmount = normalizedTime;
            yield return null;
        }
        pLayerMovement.StartCoroutine("MovePlayerToSuperPos");


    }
}
