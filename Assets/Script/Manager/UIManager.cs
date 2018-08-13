using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour {

    public TextMeshProUGUI  score;
    public GameObject gameoverPanell;
    public TextMeshProUGUI scoreGameOverScreen;
    public TextMeshProUGUI wrongKeyText;


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
        score.text = "SCORE: "+scoreTemp.ToString();      
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    bool isRunning;
    public void WrongKeyText()
    {
        if (!isRunning)
        {
            isRunning = true;
            StartCoroutine(WrongKeyCoroutine());
        }
    }

    IEnumerator WrongKeyCoroutine()
    {
        wrongKeyText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        wrongKeyText.gameObject.SetActive(false);
        isRunning = false;
    }
}
