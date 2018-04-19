using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Text score;
    public GameObject gameoverPanell;
    public Text scoreGameOverScreen;
    int scoreTemp;

    private void Start()
    {
        EventManager.GameOver += GameOver;
    }

    public void GameOver()
    {
        scoreGameOverScreen.text = "SCORE: " + scoreTemp.ToString();
        gameoverPanell.SetActive(true);
    }

    public void ScoreUpdate(int _score)
    {
        scoreTemp = _score;
        score.text = scoreTemp.ToString();      
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
