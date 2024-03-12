using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private throwManager throwManager;
    //[SerializeField] private UIController uiController;
    [SerializeField] private Serializing serializing;
    [SerializeField] private GoalHoopMovement goalHoopMov;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewLevel(WorldData worldData)
    {
        
        serializing.LoadWorldInfo(serializing.playerData.level);
        serializing.LoadPlayerInfo();
        serializing.worldData.target += serializing.playerData.score;
        serializing.playerData.score = 0;

        goalHoopMov.Init();
        throwManager.Initialized();

    }
}
