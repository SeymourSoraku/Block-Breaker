using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField NameField;

    public void StartGame()
    {
        if (!string.IsNullOrWhiteSpace(NameField.text))
        {
            GameData.Instance.PlayerName = NameField.text;
            SceneManager.LoadScene(1);
        }
    }

    public void GoToScores()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        GameData.Instance.Save();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
