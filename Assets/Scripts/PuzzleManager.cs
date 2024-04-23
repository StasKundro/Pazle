using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    // Number of puzzle pieces in the correct position
    private int piecesInPlace = 0;
    public GameObject eff;
    // Total number of puzzle pieces
    public int totalPieces = 9;

    public int score;
    public TMP_Text scoreText;

    private void Awake()
    {
        instance = this;
        score = PlayerPrefs.GetInt("Score", score);
        scoreText.text = "Score: " + score.ToString();
    }


    // Restart the level when all puzzle pieces are in the correct position
    public void PiecePlaced()
    {
        piecesInPlace++;

        if (piecesInPlace == totalPieces)
        {
            RestartLevel();
        }
    }

    // Restart the current level
    private void RestartLevel()
    {
        StartCoroutine(startGame(5f));
        score = PlayerPrefs.GetInt("Score", score);
        score += 1;
        PlayerPrefs.SetInt("Score", score);
        scoreText.text = "Score: " + score.ToString();
        eff.SetActive(true);
    }
    IEnumerator startGame(float Secs)
    {
        yield return new WaitForSeconds(Secs);
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
