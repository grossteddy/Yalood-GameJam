using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIScript : MonoBehaviour
{
    [SerializeField] GameManagerScript gameManagerScript;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] GameObject[] gameOverScreen;
    [SerializeField] GameObject good;
    [SerializeField] GameObject bad;

    private int scoreNumber = 0;

    private void Update()
    {
        time.text = gameManagerScript.GetTime().ToString("F2");
    }

    public void Restart()
    {
        StartTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnGameOver()
    {
        StopTime();

        time.gameObject.SetActive(false);
        foreach (var game in gameOverScreen) 
        {
            game.SetActive(true);
        }

        scoreNumber = (int)gameManagerScript.GetTotalScore();
        if (scoreNumber > 1000)
        {
            bad.SetActive(false);
        }
        else
        {
            good.SetActive(false);
        }

        score.text = scoreNumber.ToString();
    }

    public void OnPauseGame()
    {

    }

    private void StopTime()
    {
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    private void StartTime()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
    }





}
