using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject VictoryPanel;

    private GameObject _player;
    private SpawnManager _spawnManager;
    private ScoreManager _scoreManager;
    private SaveManager _saveManager;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _spawnManager = GetComponentInChildren<SpawnManager>();
        _scoreManager = GetComponent<ScoreManager>();
        _saveManager = GetComponent<SaveManager>();
    }

    public void GameOver()
    {
        DisableGame();
        _spawnManager.DisableAllEnemies();
        ShowPanel(GameOverPanel, false);
    }

    public void Victory()
    {
        DisableGame();

        if (_scoreManager.GetScore() < _saveManager.LoadHighScore())
        {
            _saveManager.SaveHighScore(_scoreManager.GetScore());
        }
        ShowPanel(VictoryPanel, true);
    }

    private void ShowPanel(GameObject panel, bool didWin)
    {
        panel.GetComponent<Animator>().SetBool("IsGameOver", true);
        panel.GetComponent<GameOverUIManager>().SetHighScoreText(ScoreManager.GetScoreFormatting(_saveManager.LoadHighScore()), didWin);
    }

    private void DisableGame()
    {
        _player.GetComponent<PlayerController>().enabled = false;
        _player.GetComponentInChildren<MouseCameraContoller>().enabled = false;

        PlayerShootingController shootingController = _player.GetComponentInChildren<PlayerShootingController>();
        shootingController.GameOver();
        shootingController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        _scoreManager.GameOver();
    }
}
