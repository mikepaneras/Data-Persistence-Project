using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] scores = new TextMeshProUGUI[5];
    private void Awake()
    {
        for(int i = 0; i < ScoreManager.Instance.scoreList.Count; i++)
        {
            scores[i].text = (i+1) + ") " + ScoreManager.Instance.scoreList[i].scoreMessage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
