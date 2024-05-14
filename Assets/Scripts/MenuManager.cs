using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] Text highscoreText;
    [SerializeField] Button startGame;
    string currentHighscore;
    bool scoreSet = false;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.Instance != null && !scoreSet)
        {
            currentHighscore = ScoreManager.Instance.BestScore();
            highscoreText.text = currentHighscore;
            scoreSet = true;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("main");
        Debug.Log("Loaded");
    }
    public void Username(string name)
    {
        ScoreManager.Instance.User(name);
    }
    public void ClearHighScore()
    {
        string filepath = Application.persistentDataPath + "/highscore.json";
        File.Delete(filepath);
        ScoreManager.Instance.scoreList.Clear();
        highscoreText.text = ScoreManager.Instance.BestScore();
    }
    public void GoToLeaderboard()
    {
        SceneManager.LoadScene("leaderboard");
    }
}
