using System.Collections;
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
    public AudioSource backgroundMusic;
    public float delay = 4f;
    
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
        backgroundMusic.Play();
    }

    void OnDestroy()
    {
        Enemy.OnEnemyDied -= Scoring;
        Enemy.OnEnemyDied -= Scoring;
    }

    void gameOver_Defeat()
    {
        //Time.timeScale = 0;
        
        if (score > highScore)
        {
            highScore = score;
            string fileToParse = $"{Application.dataPath}/Resources/{filename}.txt";
            StreamWriter writer = new StreamWriter(fileToParse);
            writer.WriteLine(highScore);
            writer.Close();
        }
        
        GameOverText.text = "GAME OVER, YOU LOSE!";
        gameOverCheck = true;
    }

    void gameOver_Victory()
    {
        //Time.timeScale = 0;
        
        if (score > highScore)
        {
            highScore = score;
            string fileToParse = $"{Application.dataPath}/Resources/{filename}.txt";
            StreamWriter writer = new StreamWriter(fileToParse);
            writer.WriteLine(highScore);
            writer.Close();
        }
        
        GameOverText.text = "GAME OVER, YOU WIN!";
        gameOverCheck = true;
        
    }

    void Scoring(int points)
    {
        score += points;
    }

    IEnumerator LoadCreditsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
            StartCoroutine(LoadCreditsAfterDelay(delay));
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    Time.timeScale = 1;
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            //}
        }
        
        ScoreText.text = $"SCORE\n{score.ToString("0000")}";
    }
}
