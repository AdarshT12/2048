using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public GameObject pauseMenuScreen;

    private int score;

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        highscoreText.text = LoadHighscore().ToString();            // displaying highest score.

        gameOver.alpha = 0f;                                
        gameOver.interactable = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;                                      // enabling the player to use the board.
        pauseMenuScreen.SetActive(false);
    }

    public void GameOver()
    {
        board.enabled = false;                                     
        gameOver.interactable = true;
        StartCoroutine(Fade(gameOver, 1f, 1f));
    }
   
   private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
   {
        yield return new WaitForSeconds(delay);                    // UI Desging for fade effect before merging

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while( elapsed < duration )
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
   }

   public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

   private void SetScore(int score)
   {
        this.score = score;
        scoreText.text = score.ToString();
        SaveHighscore();
   }

   private void SaveHighscore()
   {
        int hiscore = LoadHighscore();
        if (score > hiscore)
        {
            PlayerPrefs.SetInt("hiscore", score);
        }
   }

   private int LoadHighscore()
   {
        return PlayerPrefs.GetInt("hiscore", 0);
   }

   public void PauseGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(true);



    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }
}
