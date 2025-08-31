using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [Header("Required Objects")]
    [SerializeField] GameObject balloon;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameUIScript uIScript; 

    [Header("Settings")]
    [Range(0,10f)][SerializeField] float resetTime = 3f;
    [Range(10f, 120f)][SerializeField] float roundTime = 60f;

    private float totalScore = 0;
    private BalloonScript balloonScript;
    private float time;

    private void Awake()
    {
        Cursor.visible = false;
        balloonScript = balloon.GetComponent<BalloonScript>();
        time = roundTime;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            GameOver();
        }
    }

    public float GetTime() => time;

    private void GameOver()
    {

        uIScript.OnGameOver();
    }

    public void HandlePoppedBalloon()
    {
        Debug.Log("You lost your points this round!");
        NewBalloon();
    } 

    public void BankPoints()
    {
        float balloonSize = balloonScript.GetCurrentSize();

        //float gained = balloonSize * Mathf.Lerp(1, 10, balloonSize / 100);
        totalScore += balloonSize * (Mathf.Lerp(1, 10, (balloonSize / 100))); ;
        //Debug.Log("You Banked "+gained+" points. Total: "+ totalScore);
        balloonScript.OnBankBalloon();
        NewBalloon();
    }

    private void NewBalloon()
    {


        StartCoroutine(ResetBalloonAfterDelay());


    }

    public float GetTotalScore()
    {
        return totalScore;
    }

    private IEnumerator ResetBalloonAfterDelay()
    {
        yield return new WaitForSeconds(resetTime);
        balloonScript.ResetBalloon();
        balloon.transform.position = spawnPoint.position;
        balloon.transform.rotation = spawnPoint.rotation;
    }

}
