using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.IO;

[Serializable]
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public string activeUser;
    public List<ScoreDone> scoreList = new List<ScoreDone>();
    // Start is called before the first frame update
    void Start()
    {
       if(Instance != null)
        {
            Destroy(gameObject);
            return;
        } 
       Instance = this;
       DontDestroyOnLoad(gameObject);
       LoadHighscore();
    }
    public void User(string name)
    {
        Instance.activeUser = name;
    }
    public void UpdateLeaderboard(int scoreDone)
    {
        string newScoreMessage = $"{activeUser} : {scoreDone}";
        ScoreDone score = new ScoreDone(scoreDone, Instance.activeUser, newScoreMessage);
        Instance.scoreList.Add(score);
        Instance.scoreList.Sort((x, y) => y.score.CompareTo(x.score));
        if (Instance.scoreList.Count > 5)
        {
            Instance.scoreList.RemoveAt(scoreList.Count - 1);
        }

        SaveHighscore();
    }

    public string BestScore()
    {
        if (Instance.scoreList.Count > 0)
        {
           return "Highscore: " + Instance.scoreList[0].scoreMessage;
        }
        else
        {
            return "Highscore: N/A";
        }
    }
    public void SaveHighscore()
    {
        ScoreListWrapper wrapper = new ScoreListWrapper();
        wrapper.scores = Instance.scoreList;

        string jsonData = JsonUtility.ToJson(wrapper);

        string filePath = Application.persistentDataPath + "/highscore.json";
        File.WriteAllText(filePath, jsonData);
    }
    public void LoadHighscore()
    {
        string filePath = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            ScoreListWrapper wrapper = JsonUtility.FromJson<ScoreListWrapper>(jsonData);
            Instance.scoreList = wrapper.scores;
        }
        foreach (ScoreDone scoreDone in Instance.scoreList)
        {
            Debug.Log(scoreDone.scoreMessage);
        }
    }

    [Serializable]
    public class ScoreDone
    {
        public int score;
        public string username;
        public string scoreMessage;
        public ScoreDone(int score, string username, string scoreMessage)
        {
            this.score = score;
            this.username = username;
            this.scoreMessage = scoreMessage;
        }
    }
    [Serializable]
    public class ScoreListWrapper
    {
        public List<ScoreDone> scores;
    }
}
