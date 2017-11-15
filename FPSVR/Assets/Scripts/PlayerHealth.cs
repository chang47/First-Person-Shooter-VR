using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider HealthBar;
    public float Health = 100;
    public GameObject test;

    private float _currentHealth;
    private GameManager _gameManager;

    private bool first = false;

    void Start ()
    {
        _currentHealth = Health;
        _gameManager = Object.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!first)
        {
            first = !first;
            //GameObject gameobject = Instantiate(test, transform.position + transform.forward * 3, transform.rotation);
            //Animator animator = gameobject.GetComponent<Animator>();
            //animator.SetBool("IsGameOver", true);
        }
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
