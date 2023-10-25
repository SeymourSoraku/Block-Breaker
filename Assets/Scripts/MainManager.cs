using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private GameData.GameScore m_gameScore;

    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        m_gameScore = new GameData.GameScore()
        {
            PlayerName = (GameData.Instance != null ? GameData.Instance.PlayerName : "PLAYER"),
            Score = 0
        };
        if (GameData.Instance != null )
            GameData.Instance.ScoreList.Add(m_gameScore);
        SetupBricks();
        UpdateBestScore();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (!m_GameOver)
        {
            int bricks = GameObject.FindGameObjectsWithTag("Brick").Length;

            if (bricks == 0)
                SetupBricks();
        }
        else if(m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene(0);
        }
    }

    void SetupBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    void AddPoint(int point)
    {
        m_gameScore.Score += point;
        ScoreText.text = $"Score: {m_gameScore.Score}";

        UpdateBestScore();
    }

    private void UpdateBestScore()
    {
        if (GameData.Instance != null)
        {
            GameData.Instance.ScoreList.Sort();
            BestScoreText.text = $"Best Score: {GameData.Instance.ScoreList[0].Score} by {GameData.Instance.ScoreList[0].PlayerName}";
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
