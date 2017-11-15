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
    private Camera _camera;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _spawnManager = GetComponentInChildren<SpawnManager>();
        _scoreManager = GetComponent<ScoreManager>();
        _saveManager = GetComponent<SaveManager>();
        _camera = Camera.main;
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
        Vector3 frontOfPlayer = _camera.transform.position + _camera.transform.forward * 3;
        Quaternion playerRotation = _camera.transform.rotation;
        GameObject newPanel = Instantiate(panel, frontOfPlayer, playerRotation);
        newPanel.GetComponent<Animator>().SetBool("IsGameOver", true);
        newPanel.GetComponent<GameOverUIManager>().SetHighScoreText(ScoreManager.GetScoreFormatting(_saveManager.LoadHighScore()), didWin);
    }

    private void DisableGame()
    {

        PlayerShootingController shootingController = _player.GetComponentInChildren<PlayerShootingController>();
        shootingController.GameOver();
        shootingController.enabled = false;

        _scoreManager.GameOver();
    }
}
