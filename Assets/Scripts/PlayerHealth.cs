using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider HealthBar;
    public float Health = 100;

    private float _currentHealth;
    private GameManager _gameManager;

    void Start ()
    {
        _currentHealth = Health;
        _gameManager = Object.FindObjectOfType<GameManager>();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        //_currentHealth = 0;
        HealthBar.value = _currentHealth;
        if (_currentHealth <= 0)
        {
            _gameManager.GameOver();
        } 
    }
}
