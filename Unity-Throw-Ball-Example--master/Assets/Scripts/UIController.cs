using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("MainMenuUI")]
    [SerializeField] public Canvas mainMenuCanvas;
    [SerializeField] private Button startGame;

    [Header("GameUI")]
    [SerializeField] public GameObject gameUI;
    //[SerializeField] private Button startGame;

    [Header("NextLevelUI")]
    [SerializeField] public Canvas nextCanvas;
    [SerializeField] private Button nextGame;

    [Header("RestartUI")]
    [SerializeField] public Canvas restartCanvas;
    [SerializeField] private Button restartGame;


    [Header("ScoreUI")]
    [SerializeField] public int score;
    [SerializeField] private TextMesh scoreText;

    [Header("BestScoreUI")]
    [SerializeField] public int bestScore;
    [SerializeField] private TextMesh bestScoreText;

    [Header("TimeUI")]
    [SerializeField] private float time;
    [SerializeField] private TextMesh timeText;

    [Header("LevelUI")]
    [SerializeField] private TextMesh levelText;

    [Header("TargetUI")]
    [SerializeField] private TextMesh targetText;

    [Header("Timer")]
    public float timerDuration = 20;



    [Header("SuperPos")]
    public PLayerMovement pLayerMovement;

    [SerializeField] public Serializing serializing;
    [SerializeField] private LevelSystem levelSystem;


    void Start()
    {
        gameUI.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
        startGame.onClick.RemoveAllListeners();
        startGame.onClick.AddListener(OnStartGameButtonClic);
    }

    void Initialized()
    {
       
        levelSystem.NewLevel(serializing.worldData);
        time = timerDuration;
        UpdateBestScoreUI();
        StartCoroutine(Timer());
    }

    void OnStartGameButtonClic()
    {
        gameUI.SetActive(true);
        mainMenuCanvas.gameObject.SetActive(false);
        Initialized();
    }

    void OnRestartGameButtonClic()
    {
        //levelSystem.NewLevel(serializing.worldData);
        serializing.SaveInfo();
        SceneManager.LoadScene(0);
        mainMenuCanvas.gameObject.SetActive(false);
        gameUI.SetActive(true);
        restartCanvas.gameObject.SetActive(false);
        gameUI.SetActive(true);
        Initialized();
    }

    void OnNextLevelGameButtonClic()
    {
        serializing.playerData.level++;
        serializing.worldData.level++;
        serializing.SaveInfo();
        SceneManager.LoadScene(0);
        mainMenuCanvas.gameObject.SetActive(false);
        gameUI.SetActive(true);
        nextCanvas.gameObject.SetActive(false);
        gameUI.SetActive(true);
        Initialized();
    }

    public void RestartLevel()
    {
        gameUI.SetActive(false);
        restartCanvas.gameObject.SetActive(true);
        restartGame.onClick.RemoveAllListeners();
        restartGame.onClick.AddListener(OnRestartGameButtonClic);
    }

    public void NextLevel()
    {
        gameUI.SetActive(false);
        nextCanvas.gameObject.SetActive(true);
        nextGame.onClick.RemoveAllListeners();
        nextGame.onClick.AddListener(OnNextLevelGameButtonClic);
    }

    public void UpdateScoreUI()
    {
        scoreText.text = serializing.playerData.score.ToString();
    }
    public void UpdateTargetScoreUI()
    {
        serializing.worldData.target += serializing.playerData.score;
        targetText.text = serializing.worldData.target.ToString();
    }

    public void UpdateBestScoreUI()
    {
        bestScore = serializing.playerData.bestScore;
        bestScoreText.text = serializing.playerData.bestScore.ToString();
    }
    public void UpdateLevelUI()
    {
        levelText.text = serializing.worldData.level.ToString();
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
