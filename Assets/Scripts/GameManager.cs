using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public string filename;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;
    
    private int highScore;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string fileToParse = $"{Application.dataPath}/Resources/{filename}.txt";
        StreamReader reader = new StreamReader(fileToParse);
        if (reader.ReadLine() is { } line)
        {
            highScore = int.Parse(line);
        }
        reader.Close();
        ScoreText.text = "SCORE\n0000";
        HighScoreText.text = $"HI-SCORE\n{highScore.ToString("0000")}";
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
