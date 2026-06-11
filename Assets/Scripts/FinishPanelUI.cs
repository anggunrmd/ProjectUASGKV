using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishPanelUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public PlayerMovement player;

    private void OnEnable()
    {
        int totalScore = player.score + (player.GetLives() * 100);

        scoreText.text = "Total Score : " + totalScore;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}