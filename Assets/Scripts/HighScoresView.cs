using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoresView : MonoBehaviour
{
    [SerializeField] TMP_Text FirstScore;
    [SerializeField] TMP_Text FirstName;
    [SerializeField] TMP_Text SecondScore;
    [SerializeField] TMP_Text SecondName;
    [SerializeField] TMP_Text ThirdScore;
    [SerializeField] TMP_Text ThirdName;
    [SerializeField] TMP_Text OtherScores;
    [SerializeField] TMP_Text OtherNames;

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.Instance == null)
            return;

        if (GameData.Instance.ScoreList.Count >= 1)
        {
            FirstScore.text = GameData.Instance.ScoreList[0].Score.ToString();
            FirstName.text = GameData.Instance.ScoreList[0].PlayerName;
        }
        if (GameData.Instance.ScoreList.Count >= 2)
        {
            SecondScore.text = GameData.Instance.ScoreList[1].Score.ToString();
            SecondName.text = GameData.Instance.ScoreList[1].PlayerName;
        }
        if (GameData.Instance.ScoreList.Count >= 3)
        {
            ThirdScore.text = GameData.Instance.ScoreList[2].Score.ToString();
            ThirdName.text = GameData.Instance.ScoreList[2].PlayerName;
        }
        for (int i = 3; i < GameData.Instance.ScoreList.Count && i < 10; i++)
        {
            OtherScores.text += GameData.Instance.ScoreList[i].Score.ToString() + "\n";
            OtherNames.text += GameData.Instance.ScoreList[i].PlayerName + "\n";
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
