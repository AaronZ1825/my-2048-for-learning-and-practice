using System.Collections;
using System.Collections.Generic;   
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TileBoard board;
    public CanvasGroup gameOver;
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtBestScore;

    private int score = 0;
    private int bestScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (board == null || txtScore == null || txtBestScore == null || gameOver == null)
        {
            Debug.LogError("Inspector fields are not assigned!");
            return;
        }

        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
        txtBestScore.text = $"{bestScore}";

        gameOver.alpha = 0;
        gameOver.interactable = false;

        board.enabled = true;
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
    }

    private void SetScore(int score)
    {
        this.score = score;
        txtScore.text = score.ToString();

        if (this.score > bestScore)
        {
            bestScore = this.score;
            PlayerPrefs.SetInt("bestScore", this.score);
        }
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;
        PlayerPrefs.Save();
        StartCoroutine(FadeAnimate(gameOver, 1, 0.5f));
    }

    IEnumerator FadeAnimate(CanvasGroup canvasGroup, float fadeTo, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.1f;
        float fadeFrom = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(fadeFrom, fadeTo, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = fadeTo;
    }
}

