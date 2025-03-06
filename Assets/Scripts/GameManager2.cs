using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using UnityEditor;

public class GameManager2 : MonoBehaviour
{
    public string filename;
    public TextMeshProUGUI ScoreText;
    public Player playerPrefab;
    public TextMeshProUGUI GameOverText;
    public Invaders invaders;
    
    private int score = 0;
    private int highScore;
    private bool gameOverCheck = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScoreText.text = $"SCORE\n{score.ToString("0000")}";
        
        string fileToParse = $"{Application.dataPath}/Resources/{filename}.txt";
        StreamReader reader = new StreamReader(fileToParse);
        if (reader.ReadLine() is { } line)
        {
            highScore = int.Parse(line);
        }
        reader.Close();
        
        Enemy.OnEnemyDied += Scoring;
        Enemy.AlienShipDied += Scoring;
    }

    void OnDestroy()
    {
        Enemy.OnEnemyDied -= Scoring;
        Enemy.OnEnemyDied -= Scoring;
    }

    void gameOver_Defeat()
    {
        Time.timeScale = 0;
        
        if (score > highScore)
        {
            highScore = score;
            string fileToParse = $"{Application.dataPath}/Resources/{filename}.txt";
            StreamWriter writer = new StreamWriter(fileToParse);
            writer.WriteLine(highScore);
            writer.Close();
        }
        
        GameOverText.text = "GAME OVER, YOU LOSE! Would you like to play again?\n Press 'R' to play again.";
        gameOverCheck = true;
    }

    void gameOver_Victory()
    {
        Time.timeScale = 0;
        
        if (score > highScore)
        {
            highScore = score;
            string fileToParse = $"{Application.dataPath}/Resources/{filename}.txt";
            StreamWriter writer = new StreamWriter(fileToParse);
            writer.WriteLine(highScore);
            writer.Close();
        }
        
        GameOverText.text = "GAME OVER, YOU WIN! Would you like to play again?\n Press 'R' to play again.";
        gameOverCheck = true;
        
    }

    void Scoring(int points)
    {
        score += points;
    }

    // Update is called once per frame
    void Update()
    {
        playerPrefab.died += gameOver_Defeat;

        if (invaders.totalEnemies <= 0)
        {
            gameOver_Victory();
        }

        if (gameOverCheck)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
        
        ScoreText.text = $"SCORE\n{score.ToString("0000")}";
    }
}
