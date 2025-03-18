using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using System.Security.Cryptography;
using UnityEngine.Accessibility;
using System.Linq;

public class UIController : MonoBehaviour
{
    PlayerScript player;
    TMP_Text scoreText;
    TMP_Text HighScore;
    TMP_Text CurrentScore;
    public static UIController Instance;
    public GameObject froggyIsDeathPanel;
    public bool isPlaying = false;
    public float currentScore = 0f;
    private int scoreIterator = 8;
    [SerializeField] public Data data;
    public int currentHighScoreDataIndex;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        HighScore = GameObject.Find("HighScore").GetComponent<TMP_Text>();
        CurrentScore = GameObject.Find("CurrentScore").GetComponent<TMP_Text>();

        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        string loadedData = saveSystem.Load("save");
        if (loadedData != null)
        {
            data = JsonUtility.FromJson<Data>(loadedData);
        }
        else { data = new Data();}
        isPlaying = true;
        currentScore = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if (currentScore <= 100)
            {
                if (Mathf.FloorToInt(currentScore) % 12 == 0 && scoreIterator <= 16)
                {
                    scoreIterator += 1;
                }
            }
            else if (currentScore <= 1000)
            {
                if (Mathf.FloorToInt(currentScore) % 125 == 0 && scoreIterator <= 24)
                {
                    scoreIterator += 1;
                }
            }
            else if (currentScore <= 10000)
            {
                scoreIterator = 36;
            }
     
            currentScore += Time.deltaTime * scoreIterator;
        }
        scoreText.text = "Score: " + prettyScore(Mathf.FloorToInt(currentScore));
        if (!FroggyColliderDeath.isFroggyAlive) { 
            isPlaying = false;
            FroggyIsDead();
            gameOverForSingle();
    
        }
        HighScore.text = "High Score: " + data.highScoreData.ToString();
        CurrentScore.text = "Current Score: " + prettyScore(Mathf.FloorToInt(currentScore));
    }

    public void gameOverForSingle() {
        if (currentScore > data.highScoreData) {
            data.highScoreData = Mathf.FloorToInt(currentScore);
            string saveData = JsonUtility.ToJson(data);
            saveSystem.Save("save", saveData);
        }
    }

    public void FroggyIsDead()
    {
        Time.timeScale = 0f;
        froggyIsDeathPanel.SetActive(true);
        FroggyColliderDeath.isFroggyAlive = true;
    }

    public string prettyScore(int score) {
        return Mathf.RoundToInt(score).ToString();
    }
}
