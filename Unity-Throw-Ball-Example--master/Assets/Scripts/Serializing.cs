using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Serializing : MonoBehaviour
{

    public WorldData worldData;
    public PlayerData playerData;
    public string worldFilePath;
    public string playerFilePath;

    private void Awake()
    {
        print("start");
        worldFilePath = Application.streamingAssetsPath + worldFilePath;
        playerFilePath = Application.streamingAssetsPath + playerFilePath;
        if (!File.Exists(playerFilePath))
        {
            var playerStartData = new PlayerData
            {
                bestScore = 0,
                score = 0,
                level = 1   
            };
            string json = JsonUtility.ToJson(playerStartData);
            File.WriteAllText(playerFilePath, json);
        }
        LoadWorldInfo(1);
        print(worldData.level);
    }

    void Start()
    {
        LoadPlayerInfo();
        print(playerData.level);
        LoadWorldInfo(playerData.level);
    }

    public void SaveInfo()
    {
        var playerSaveData = new PlayerData
        {
            bestScore = playerData.bestScore,
            score = 0,
            level = playerData.level
        };
        string json = JsonUtility.ToJson(playerSaveData);
        File.WriteAllText(playerFilePath, json);
        Debug.Log("Saving successful");
    }
    
    public void LoadPlayerInfo()
    {
        string json = File.ReadAllText(playerFilePath);
        playerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log($"LoadingPlayer - {playerData.bestScore}");
    }

    public void LoadWorldInfo(int level)
    {
        string json = File.ReadAllText(worldFilePath+$"{level}.json");
        worldData = JsonUtility.FromJson<WorldData>(json);
        Debug.Log($"Loading World - {worldData.level}");
    }

    private void OnApplicationQuit()
    {
        Debug.Log("exit");
        SaveInfo();
    }

}
