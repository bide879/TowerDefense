using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GameClear()
    {
        SceneManager.LoadScene("GameClear");
    }

}
